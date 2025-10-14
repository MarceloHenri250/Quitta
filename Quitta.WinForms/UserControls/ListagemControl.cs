using Quitta.Forms;
using Quitta.Models;
using Quitta.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitta.UserControls
{
    public partial class ListagemControl : UserControl
    {
        // helper display row used as DataSource to allow editing via DataGridView
        private class DisplayRow
        {
            public string Id { get; set; } = string.Empty;
            public TipoItem Tipo { get; set; }
            public string Numero { get; set; } = string.Empty;
            public string Fornecedor { get; set; } = string.Empty;
            public decimal Valor { get; set; }
            public DateTime Vencimento { get; set; }
            public StatusItem Status { get; set; }
            public string AnexoDisplay { get; set; } = "Sem anexo";
        }

        // keep a binding list so DataGridView can interact with rows
        private BindingList<DisplayRow> displayRows = new();
        // Lista completa carregada neste controle (cache local)
        private List<Item> items = new();
        private List<Item> filteredItems = new();
        private int currentPage = 1;
        private int pageSize = 20;

        // Guarda valor anterior do status para permitir reverter caso necessário
        private StatusItem? prevStatusValue = null;
        private int prevStatusRowIndex = -1;

        // Serviço de dados usado para persistir alterações diretamente a partir deste controle
        private readonly DataService dataService;

        // Eventos que o MainForm pode assinar para reagir a updates/deletes
        public event Action<int>? PageChanged;
        public event Action<Item>? ItemUpdated;
        public event Action<Item>? ItemDeleted;

        public ListagemControl()
        {
            InitializeComponent();

            dataService = new DataService();

            // configurar eventos do DataGrid e controles
            dgvListagem.KeyDown += DgvListagem_KeyDown;
            dgvListagem.CellContentClick += DgvListagem_CellContentClick;
            dgvListagem.CellBeginEdit += DgvListagem_CellBeginEdit;
            dgvListagem.EditingControlShowing += DgvListagem_EditingControlShowing;
            dgvListagem.CellValueChanged += DgvListagem_CellValueChanged;

            // Commit imediato para que CellValueChanged dispare assim que usuário escolher uma opção
            dgvListagem.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgvListagem.CurrentCell != null && dgvListagem.CurrentCell.OwningColumn == colStatus && dgvListagem.IsCurrentCellDirty)
                {
                    dgvListagem.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            };

            btnFiltrar.Click += (s, e) => { currentPage = 1; RefreshGrid(); };
            btnLimparFiltros.Click += (s, e) => { ClearFilters(); };

            btnPrimeira.Click += (s, e) => { FirstPage(); };
            btnAnterior.Click += (s, e) => { PreviousPage(); };
            btnProxima.Click += (s, e) => { NextPage(); };
            btnUltima.Click += (s, e) => { LastPage(); };

            txtBusca.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { currentPage = 1; RefreshGrid(); } };
        }

        /// <summary>
        /// Define os dados exibidos no controle.
        /// </summary>
        /// <param name="items">Lista completa de itens (vinda do DataService / MainForm)</param>
        public void SetData(List<Item> items)
        {
            this.items = items ?? new List<Item>();
            currentPage = 1;
            ApplyFilters();
            RefreshGrid();
        }

        /// <summary>
        /// Aplica os filtros definidos na UI sobre a coleção completa `items` e atualiza `filteredItems`.
        /// </summary>
        private void ApplyFilters()
        {
            // Start from full list
            IEnumerable<Item> q = items;

            // Busca por texto (Número ou Fornecedor)
            var term = txtBusca?.Text?.Trim();
            if (!string.IsNullOrEmpty(term))
            {
                q = q.Where(i => (i.Numero != null && i.Numero.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0)
                                || (i.Fornecedor != null && i.Fornecedor.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0));
            }

            // Filtro por tipo (Boleto/Nota)
            if (cmbFiltroTipo != null && cmbFiltroTipo.SelectedItem != null && cmbFiltroTipo.SelectedItem.ToString() != "Todos")
            {
                var sel = cmbFiltroTipo.SelectedItem.ToString();
                if (sel == "Boleto") q = q.Where(i => i.Tipo == TipoItem.Boleto);
                else if (sel == "Nota") q = q.Where(i => i.Tipo == TipoItem.Nota);
            }

            // Filtro por status
            if (cmbFiltroStatus != null && cmbFiltroStatus.SelectedItem != null && cmbFiltroStatus.SelectedItem.ToString() != "Todos")
            {
                var sel = cmbFiltroStatus.SelectedItem.ToString();
                if (sel == "Pendente") q = q.Where(i => i.Status == StatusItem.Pendente);
                else if (sel == "Pago") q = q.Where(i => i.Status == StatusItem.Pago);
                else if (sel == "Vencido") q = q.Where(i => i.Status == StatusItem.Vencido);
            }

            // Filtro por período: permite start-only, end-only ou intervalo
            if (dtpFiltroInicio != null && dtpFiltroFim != null)
            {
                var hasStart = dtpFiltroInicio.ShowCheckBox ? dtpFiltroInicio.Checked : true;
                var hasEnd = dtpFiltroFim.ShowCheckBox ? dtpFiltroFim.Checked : true;

                if (hasStart && !hasEnd)
                {
                    var start = dtpFiltroInicio.Value.Date;
                    q = q.Where(i => i.Vencimento.Date >= start);
                }
                else if (!hasStart && hasEnd)
                {
                    var end = dtpFiltroFim.Value.Date;
                    q = q.Where(i => i.Vencimento.Date <= end);
                }
                else if (hasStart && hasEnd)
                {
                    var start = dtpFiltroInicio.Value.Date;
                    var end = dtpFiltroFim.Value.Date;
                    if (start <= end)
                    {
                        q = q.Where(i => i.Vencimento.Date >= start && i.Vencimento.Date <= end);
                    }
                }
            }

            filteredItems = q.ToList();
        }

        /// <summary>
        /// Atualiza a grade com a página atual de `filteredItems`.
        /// </summary>
        private void RefreshGrid()
        {
            ApplyFilters();
            var totalPages = Math.Max(1, (int)Math.Ceiling((double)filteredItems.Count / pageSize));
            if (currentPage > totalPages) currentPage = totalPages;

            var page = filteredItems.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            // Project to a display model to include AnexoDisplay and Id and bind as a BindingList so editing works
            displayRows.Clear();
            foreach (var i in page)
            {
                displayRows.Add(new DisplayRow
                {
                    Id = i.Id,
                    Tipo = i.Tipo,
                    Numero = i.Numero,
                    Fornecedor = i.Fornecedor,
                    Valor = i.Valor,
                    Vencimento = i.Vencimento,
                    Status = i.Status,
                    AnexoDisplay = (i.Attachments != null && i.Attachments.Count > 0) ? i.Attachments[0].FileName : "Sem anexo"
                });
            }

            dgvListagem.DataSource = null;
            dgvListagem.DataSource = displayRows;

            UpdatePaginationLabel();
        }

        private void UpdatePaginationLabel()
        {
            var totalPages = Math.Max(1, (int)Math.Ceiling((double)filteredItems.Count / pageSize));
            lblPagina.Text = $"Página {currentPage} de {totalPages}";
            PageChanged?.Invoke(currentPage);
        }

        public void NextPage()
        {
            if ((currentPage * pageSize) < filteredItems.Count)
            {
                currentPage++;
                RefreshGrid();
            }
        }

        public void PreviousPage()
        {
            if (currentPage > 1)
            {
                currentPage--;
                RefreshGrid();
            }
        }

        public void FirstPage()
        {
            currentPage = 1;
            RefreshGrid();
        }

        public void LastPage()
        {
            currentPage = Math.Max(1, (int)Math.Ceiling((double)filteredItems.Count / pageSize));
            RefreshGrid();
        }

        /// <summary>
        /// Antes de iniciar edição de status, guardamos o valor atual para eventual revert.
        /// Trabalha com o objeto anônimo ligado à grade (pega Id e acha Item real em `items`).
        /// </summary>
        private void DgvListagem_CellBeginEdit(object? sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var col = dgvListagem.Columns[e.ColumnIndex];
            if (col == colStatus)
            {
                var rowObj = dgvListagem.Rows[e.RowIndex].DataBoundItem;
                if (rowObj == null) return;
                var idProp = rowObj.GetType().GetProperty("Id");
                if (idProp == null) return;
                var id = idProp.GetValue(rowObj)?.ToString();
                var it = items.FirstOrDefault(x => x.Id == id);
                if (it != null)
                {
                    prevStatusValue = it.Status;
                    prevStatusRowIndex = e.RowIndex;
                }
            }
        }

        /// <summary>
        /// Quando o editor (ComboBox) aparece, ajustamos a lista de opções permitidas.
        /// </summary>
        private void DgvListagem_EditingControlShowing(object? sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvListagem.CurrentCell == null) return;
            var col = dgvListagem.CurrentCell.OwningColumn;
            if (col == colStatus && e.Control is ComboBox cb)
            {
                // ajustar opções: remover 'Pendente' quando o vencimento já passou
                var rowObj = dgvListagem.CurrentRow?.DataBoundItem;
                if (rowObj != null)
                {
                    var idProp = rowObj.GetType().GetProperty("Id");
                    if (idProp != null)
                    {
                        var id = idProp.GetValue(rowObj)?.ToString();
                        var it = items.FirstOrDefault(x => x.Id == id);
                        if (it != null)
                        {
                            var today = DateTime.Now.Date;
                            var allowed = Enum.GetValues(typeof(StatusItem)).Cast<StatusItem>().Where(s => !(s == StatusItem.Pendente && it.Vencimento.Date <= today)).ToList();

                            // definir DataSource do ComboBox do editor para a lista filtrada
                            try
                            {
                                cb.DataSource = null;
                                cb.DataSource = allowed;
                            }
                            catch
                            {
                                // se falhar, não interrompe a edição
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Trata alteração de valor da célula (aplica e salva automaticamente).
        /// </summary>
        private void DgvListagem_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var col = dgvListagem.Columns[e.ColumnIndex];
            if (col == colStatus)
            {
                var rowObj = dgvListagem.Rows[e.RowIndex].DataBoundItem;
                if (rowObj == null) return;
                var idProp = rowObj.GetType().GetProperty("Id");
                if (idProp == null) return;
                var id = idProp.GetValue(rowObj)?.ToString();
                var item = items.FirstOrDefault(i => i.Id == id);
                if (item == null) return;

                var cell = dgvListagem.Rows[e.RowIndex].Cells[e.ColumnIndex];
                StatusItem? newStatus = null;
                if (cell.Value is StatusItem sVal) newStatus = sVal;
                else if (cell.Value != null && Enum.TryParse<StatusItem>(cell.Value.ToString(), out var parsed)) newStatus = parsed;

                if (newStatus == null) return;

                // ignore if unchanged
                if (newStatus.Value == item.Status)
                {
                    prevStatusValue = null;
                    prevStatusRowIndex = -1;
                    return;
                }

                // if item is overdue and newStatus is Pendente, block it (shouldn't be available in editor but double-check)
                if (newStatus.Value == StatusItem.Pendente && item.Vencimento.Date <= DateTime.Now.Date)
                {
                    // force to Vencido
                    newStatus = StatusItem.Vencido;
                }

                var appliedStatus = newStatus.Value;

                // apply and persist on UI thread after current edit finishes
                this.BeginInvoke((Action)(() =>
                {
                    item.Status = appliedStatus;
                    var idx = items.FindIndex(i => i.Id == item.Id);
                    if (idx >= 0) items[idx].Status = item.Status;
                    try { dataService.SaveItems(items); } catch { }
                    ItemUpdated?.Invoke(item);
                    RefreshGrid();
                }));

                prevStatusValue = null;
                prevStatusRowIndex = -1;
            }
        }

        /// <summary>
        /// Exclusão via tecla Delete com confirmação e persistência imediata.
        /// </summary>
        private void DgvListagem_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dgvListagem.CurrentRow == null) return;
                var rowObj = dgvListagem.CurrentRow.DataBoundItem;
                if (rowObj == null) return;
                var idProp = rowObj.GetType().GetProperty("Id");
                if (idProp == null) return;
                var id = idProp.GetValue(rowObj)?.ToString();
                var selected = items.FirstOrDefault(i => i.Id == id);
                if (selected == null) return;

                var res = MessageBox.Show($"Deseja remover o item {selected.Numero}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    items.RemoveAll(i => i.Id == selected.Id);
                    try { dataService.SaveItems(items); } catch { }
                    RefreshGrid();
                    ItemDeleted?.Invoke(selected);
                }
            }
        }

        /// <summary>
        /// Clique nos botões de ação (Anexo/Editar/Excluir) na grade.
        /// </summary>
        private void DgvListagem_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var col = dgvListagem.Columns[e.ColumnIndex];
            var itemRow = dgvListagem.Rows[e.RowIndex].DataBoundItem;
            if (itemRow == null) return;

            // itemRow is an anonymous type used for display, find underlying item by Id
            var idProp = itemRow.GetType().GetProperty("Id");
            if (idProp == null) return;
            var id = idProp.GetValue(itemRow)?.ToString();
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item == null) return;

            if (col == colAnexo)
            {
                // if has attachment, open the first one; otherwise allow adding
                if (item.Attachments != null && item.Attachments.Count > 0)
                {
                    var attach = item.Attachments[0];
                    var path = dataService.GetAttachmentPath(attach.FileName);
                    if (File.Exists(path))
                    {
                        try { System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(path) { UseShellExecute = true }); }
                        catch { MessageBox.Show("Não foi possível abrir o anexo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                    else
                    {
                        MessageBox.Show("Arquivo de anexo não encontrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    // adicionar anexo (apenas um permitido)
                    using var ofd = new OpenFileDialog();
                    ofd.Title = "Selecionar anexo";
                    ofd.Filter = "Todos os arquivos (*.*)|*.*";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        var dest = dataService.GetAttachmentPath(Path.GetFileName(ofd.FileName));
                        dest = dataService.GetUniqueAttachmentPath(dest);
                        try
                        {
                            File.Copy(ofd.FileName, dest);
                            var attach = new Attachment { Id = Guid.NewGuid().ToString(), FileName = Path.GetFileName(dest), RelativePath = dest, CreatedAt = DateTime.Now };
                            // only one attachment allowed: replace existing
                            if (item.Attachments == null) item.Attachments = new List<Attachment>();
                            else item.Attachments.Clear();
                            item.Attachments.Add(attach);
                            dataService.SaveItems(items);
                            RefreshGrid();
                        }
                        catch
                        {
                            MessageBox.Show("Falha ao adicionar anexo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                return;
            }

            if (col == colEditar)
            {
                // abrir diálogo de edição (o diálogo já pede confirmação ao salvar)
                using var dlg = new EditItemDialog(item);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var edited = dlg.EditedItem;
                    var idx = items.FindIndex(i => i.Id == edited.Id);
                    if (idx >= 0) items[idx] = edited;

                    try { dataService.SaveItems(items); } catch { }

                    RefreshGrid();
                    ItemUpdated?.Invoke(edited);
                }
            }
            else if (col == colExcluir)
            {
                var res = MessageBox.Show($"Deseja remover o item {item.Numero}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    items.RemoveAll(i => i.Id == item.Id);
                    try { dataService.SaveItems(items); } catch { }
                    RefreshGrid();
                    ItemDeleted?.Invoke(item);
                }
            }
        }

        /// <summary>
        /// Restaura os filtros para o estado inicial.
        /// </summary>
        private void ClearFilters()
        {
            if (txtBusca != null) txtBusca.Clear();
            if (cmbFiltroTipo != null && cmbFiltroTipo.Items.Count > 0) cmbFiltroTipo.SelectedIndex = 0;
            if (cmbFiltroStatus != null && cmbFiltroStatus.Items.Count > 0) cmbFiltroStatus.SelectedIndex = 0;
            if (dtpFiltroInicio != null) dtpFiltroInicio.Value = DateTime.Now.Date;
            if (dtpFiltroFim != null) dtpFiltroFim.Value = DateTime.Now.Date;
            currentPage = 1;
            RefreshGrid();
        }

    }
}
