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
        #region Tipos internos
        // Modelo usado apenas para exibição/edição na DataGridView
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
        #endregion

        #region Campos privados
        // BindingList para permitir edição via DataGridView
        private BindingList<DisplayRow> displayRows = new();

        // Cache local dos itens (lista completa e filtrada)
        private List<Item> items = new();
        private List<Item> filteredItems = new();

        // paginação
        private int currentPage = 1;
        private int pageSize = 20;

        // controle de edição de status (para permitir revert em caso de erro)
        private StatusItem? prevStatusValue = null;
        private int prevStatusRowIndex = -1;

        // serviço de dados para persistência de anexos e outros
        private readonly DataService dataService;

        // eventos que MainForm pode assinar
        public event Action<int>? PageChanged;
        public event Action<Item>? ItemUpdated;
        public event Action<Item>? ItemDeleted;
        #endregion

        #region Construtor e inicialização
        public ListagemControl()
        {
            InitializeComponent();

            dataService = new DataService();

            // recalcula página efetiva quando tamanho muda
            dgvListagem.SizeChanged += (s, e) => RefreshGrid();
            this.Resize += (s, e) => RefreshGrid();

            // eventos do DataGridView
            dgvListagem.KeyDown += DgvListagem_KeyDown;
            dgvListagem.CellContentClick += DgvListagem_CellContentClick;
            dgvListagem.CellBeginEdit += DgvListagem_CellBeginEdit;
            dgvListagem.EditingControlShowing += DgvListagem_EditingControlShowing;
            dgvListagem.CellValueChanged += DgvListagem_CellValueChanged;

            // commit imediato para editor de status em célula
            dgvListagem.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgvListagem.CurrentCell != null && dgvListagem.CurrentCell.OwningColumn == colStatus && dgvListagem.IsCurrentCellDirty)
                {
                    dgvListagem.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            };

            // filtros e paginação
            btnFiltrar.Click += (s, e) => { currentPage = 1; RefreshGrid(); };
            btnLimparFiltros.Click += (s, e) => { ClearFilters(); };

            btnPrimeira.Click += (s, e) => { FirstPage(); };
            btnAnterior.Click += (s, e) => { PreviousPage(); };
            btnProxima.Click += (s, e) => { NextPage(); };
            btnUltima.Click += (s, e) => { LastPage(); };

            txtBusca.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { currentPage = 1; RefreshGrid(); } };
        }
        #endregion

        #region Utilitários de layout/paginação
        // Calcula quantas linhas cabem no DataGridView (tamanho dinâmico de página)
        private int GetEffectivePageSize()
        {
            try
            {
                if (dgvListagem == null || dgvListagem.RowTemplate.Height <= 0)
                    return Math.Max(1, pageSize);

                var available = dgvListagem.ClientSize.Height - dgvListagem.ColumnHeadersHeight;
                var rows = available / dgvListagem.RowTemplate.Height;
                return Math.Max(1, rows);
            }
            catch
            {
                return Math.Max(1, pageSize);
            }
        }

        private void UpdatePaginationLabel()
        {
            var effectivePageSize = GetEffectivePageSize();
            var totalPages = Math.Max(1, (int)Math.Ceiling((double)filteredItems.Count / effectivePageSize));
            lblPagina.Text = $"Página {currentPage} de {totalPages}";
            PageChanged?.Invoke(currentPage);
        }

        public void NextPage()
        {
            var effectivePageSize = GetEffectivePageSize();
            if ((currentPage * effectivePageSize) < filteredItems.Count)
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
            var effectivePageSize = GetEffectivePageSize();
            currentPage = Math.Max(1, (int)Math.Ceiling((double)filteredItems.Count / effectivePageSize));
            RefreshGrid();
        }
        #endregion

        #region Carregamento e aplicação de filtros
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
        /// Aplica filtros definidos na UI sobre a coleção completa `items` e atualiza `filteredItems`.
        /// </summary>
        private void ApplyFilters()
        {
            // começar pela lista completa
            IEnumerable<Item> q = items;

            // busca por texto (Número ou Fornecedor)
            var term = txtBusca?.Text?.Trim();
            if (!string.IsNullOrEmpty(term))
            {
                q = q.Where(i => (i.Numero != null && i.Numero.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0)
                                || (i.Fornecedor != null && i.Fornecedor.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0));
            }

            // filtro por tipo (Boleto/Nota)
            if (cmbFiltroTipo != null && cmbFiltroTipo.SelectedItem != null && cmbFiltroTipo.SelectedItem.ToString() != "Todos")
            {
                var sel = cmbFiltroTipo.SelectedItem.ToString();
                if (sel == "Boleto") q = q.Where(i => i.Tipo == TipoItem.Boleto);
                else if (sel == "Nota") q = q.Where(i => i.Tipo == TipoItem.Nota);
            }

            // filtro por status
            if (cmbFiltroStatus != null && cmbFiltroStatus.SelectedItem != null && cmbFiltroStatus.SelectedItem.ToString() != "Todos")
            {
                var sel = cmbFiltroStatus.SelectedItem.ToString();
                if (sel == "Pendente") q = q.Where(i => i.Status == StatusItem.Pendente);
                else if (sel == "Pago") q = q.Where(i => i.Status == StatusItem.Pago);
                else if (sel == "Vencido") q = q.Where(i => i.Status == StatusItem.Vencido);
            }

            // filtro por período: permite start-only, end-only ou intervalo
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
        #endregion

        #region Atualização da grade e projeção para exibição
        /// <summary>
        /// Atualiza a grade com a página atual de `filteredItems`.
        /// </summary>
        private void RefreshGrid()
        {
            ApplyFilters();

            var effectivePageSize = GetEffectivePageSize();

            // atualização automática de itens vencidos (se Pendente e vencido, marca Vencido)
            var today = DateTime.Now.Date;
            var changed = new List<Item>();

            // iterar sobre snapshot para evitar CollectionModified
            foreach (var it in items.ToList())
            {
                if (it.Status == StatusItem.Pendente && it.Vencimento.Date <= today)
                {
                    it.Status = StatusItem.Vencido;
                    changed.Add(it);
                }
            }

            if (changed.Count > 0)
            {
                ApplyFilters();
                // notificar ouvintes para persistência
                foreach (var it in changed)
                {
                    try { ItemUpdated?.Invoke(it); } catch { }
                }
            }

            var totalPages = Math.Max(1, (int)Math.Ceiling((double)filteredItems.Count / effectivePageSize));
            if (currentPage > totalPages) currentPage = totalPages;

            var page = filteredItems.Skip((currentPage - 1) * effectivePageSize).Take(effectivePageSize).ToList();

            // projetar para DisplayRow (inclui AnexoDisplay e Id) para binding
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
                    AnexoDisplay = (i.Attachments != null && i.Attachments.Count > 0) ? "Abrir anexo" : "Sem anexo"
                });
            }

            dgvListagem.DataSource = null;
            dgvListagem.DataSource = displayRows;

            // garantir texto do botão de anexo por linha quando não está ligado diretamente ao binding
            for (int r = 0; r < dgvListagem.Rows.Count; r++)
            {
                var cell = dgvListagem.Rows[r].Cells["colAnexo"] as DataGridViewButtonCell;
                if (cell != null)
                {
                    var val = cell.Value?.ToString();
                    if (string.IsNullOrEmpty(val))
                    {
                        var dr = dgvListagem.Rows[r].DataBoundItem;
                        var prop = dr?.GetType().GetProperty("AnexoDisplay");
                        var txt = prop?.GetValue(dr)?.ToString() ?? "Sem anexo";
                        cell.Value = txt;
                    }
                    else
                    {
                        cell.Value = val;
                    }
                }
            }

            UpdatePaginationLabel();
        }
        #endregion

        #region Edição inline (Status) e handlers relacionados
        /// <summary>
        /// Antes de iniciar edição de status, guardamos o valor atual para eventual revert.
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
        /// Quando o editor (ComboBox) aparece, ajustamos opções permitidas (ex: impedir Pendente se vencido).
        /// </summary>
        private void DgvListagem_EditingControlShowing(object? sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvListagem.CurrentCell == null) return;
            var col = dgvListagem.CurrentCell.OwningColumn;
            if (col == colStatus && e.Control is ComboBox cb)
            {
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

                            try
                            {
                                cb.DataSource = null;
                                cb.DataSource = allowed;
                                cb.SelectedIndexChanged -= StatusEditor_SelectedIndexChanged;
                                cb.SelectedIndexChanged += StatusEditor_SelectedIndexChanged;
                            }
                            catch
                            {
                                // não interromper edição em caso de erro aqui
                            }
                        }
                    }
                }
            }
        }

        // handler para cometer edição imediatamente quando usuário altera o ComboBox
        private void StatusEditor_SelectedIndexChanged(object? sender, EventArgs e)
        {
            try
            {
                if (dgvListagem.CurrentCell != null && dgvListagem.CurrentCell.OwningColumn == colStatus)
                {
                    dgvListagem.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Trata alteração de valor da célula (aplica e delega persistência via evento ItemUpdated).
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

                // se não mudou, limpar estado anterior
                if (newStatus.Value == item.Status)
                {
                    prevStatusValue = null;
                    prevStatusRowIndex = -1;
                    return;
                }

                // garantir que não volte para Pendente se já estiver vencido
                if (newStatus.Value == StatusItem.Pendente && item.Vencimento.Date <= DateTime.Now.Date)
                {
                    newStatus = StatusItem.Vencido;
                }

                var appliedStatus = newStatus.Value;

                // aplicar e notificar na UI thread
                this.BeginInvoke((Action)(() =>
                {
                    item.Status = appliedStatus;
                    var idx = items.FindIndex(i => i.Id == item.Id);
                    if (idx >= 0) items[idx].Status = item.Status;
                    ItemUpdated?.Invoke(item);
                    RefreshGrid();
                }));

                prevStatusValue = null;
                prevStatusRowIndex = -1;
            }
        }
        #endregion

        #region Ações por clique nas colunas (Anexo / Editar / Excluir)
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
                    ItemDeleted?.Invoke(selected);
                    RefreshGrid();
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

            var idProp = itemRow.GetType().GetProperty("Id");
            if (idProp == null) return;
            var id = idProp.GetValue(itemRow)?.ToString();
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item == null) return;

            if (col == colAnexo)
            {
                // se existe anexo, tenta abrir o primeiro; caso contrário permite adicionar
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
                    var allowedExts = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".pdf", ".jpg", ".jpeg", ".png" };
                    using var ofd = new OpenFileDialog();
                    ofd.Title = "Selecionar anexo";
                    ofd.Filter = "PDF (*.pdf)|*.pdf|Imagens (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|Todos os arquivos (*.*)|*.*";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        var ext = Path.GetExtension(ofd.FileName) ?? string.Empty;
                        if (!allowedExts.Contains(ext.ToLowerInvariant()))
                        {
                            MessageBox.Show("Formato de arquivo inválido. São permitidos: PDF, JPG e PNG.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            var dest = dataService.GetAttachmentPath(Path.GetFileName(ofd.FileName));
                            dest = dataService.GetUniqueAttachmentPath(dest);
                            try
                            {
                                File.Copy(ofd.FileName, dest);
                                var attach = new Attachment { Id = Guid.NewGuid().ToString(), FileName = Path.GetFileName(dest), RelativePath = dest, CreatedAt = DateTime.Now };
                                if (item.Attachments == null) item.Attachments = new List<Attachment>();
                                else item.Attachments.Clear();
                                item.Attachments.Add(attach);
                                ItemUpdated?.Invoke(item);
                                RefreshGrid();
                            }
                            catch
                            {
                                MessageBox.Show("Falha ao adicionar anexo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }

                return;
            }

            if (col == colEditar)
            {
                using var dlg = new EditItemDialog(item);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    var edited = dlg.EditedItem;
                    var idx = items.FindIndex(i => i.Id == edited.Id);
                    if (idx >= 0) items[idx] = edited;

                    ItemUpdated?.Invoke(edited);
                    RefreshGrid();
                }
            }
            else if (col == colExcluir)
            {
                var res = MessageBox.Show($"Deseja remover o item {item.Numero}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    items.RemoveAll(i => i.Id == item.Id);
                    ItemDeleted?.Invoke(item);
                    RefreshGrid();
                }
            }
        }
        #endregion

        #region Helpers públicos/privados
        /// <summary>
        /// Restaura os filtros para o estado inicial.
        /// </summary>
        private void ClearFilters()
        {
            if (txtBusca != null) txtBusca.Clear();
            if (cmbFiltroTipo != null && cmbFiltroTipo.Items.Count > 0) cmbFiltroTipo.SelectedIndex = 0;
            if (cmbFiltroStatus != null && cmbFiltroStatus.Items.Count > 0) cmbFiltroStatus.SelectedIndex = 0;
            if (dtpFiltroInicio != null && dtpFiltroInicio.ShowCheckBox) dtpFiltroInicio.Checked = false;
            if (dtpFiltroFim != null && dtpFiltroFim.ShowCheckBox) dtpFiltroFim.Checked = false;
            currentPage = 1;
            RefreshGrid();
        }
        #endregion
    }
}
