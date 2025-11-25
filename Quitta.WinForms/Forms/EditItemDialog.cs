using Quitta.Models;
using Quitta.Services;
using System.Data;

//TODO: colocar os componentes no designer ao inves de estarem todos no codigo

namespace Quitta.Forms
{
    public partial class EditItemDialog : Form
    {
        public Item EditedItem { get; private set; }

        private DataService dataService;

        public EditItemDialog(Item item)
        {
            InitializeComponent();

            // initialize DataService for file ops
            dataService = new DataService();

            // deep copy item to EditedItem
            EditedItem = new Item
            {
                Id = item.Id,
                Tipo = item.Tipo,
                Numero = item.Numero,
                Fornecedor = item.Fornecedor,
                Valor = item.Valor,
                Vencimento = item.Vencimento,
                Status = item.Status,
                Attachments = item.Attachments != null ? new List<Attachment>(item.Attachments.Select(a => new Attachment { Id = a.Id, FileName = a.FileName, RelativePath = a.RelativePath, CreatedAt = a.CreatedAt })) : new List<Attachment>()
            };

            // Preencher campos
            if (item.Tipo == TipoItem.Boleto)
                rbBoleto.Checked = true;
            else
                rbNota.Checked = true;

            txtNumero.Text = item.Numero;
            txtFornecedor.Text = item.Fornecedor;

            // Ensure the numeric control can accept the item's value: clamp to Min/Max to avoid ArgumentOutOfRangeException
            try
            {
                decimal valueToSet = item.Valor;
                if (valueToSet < numValor.Minimum) valueToSet = numValor.Minimum;
                if (valueToSet > numValor.Maximum) valueToSet = numValor.Maximum;
                numValor.Value = valueToSet;
            }
            catch
            {
                // fallback: set to minimum if anything unexpected happens
                try { numValor.Value = numValor.Minimum; } catch { }
            }

            dtpVencimento.Value = item.Vencimento;

            // populate status combobox with enum values (designer had strings)
            try
            {
                // set up options based on vencimento
                UpdateStatusOptions();
                // ensure current status is selected when allowed
                if (cmbStatus.DataSource is List<StatusItem> list && list.Contains(item.Status))
                    cmbStatus.SelectedItem = item.Status;
            }
            catch
            {
                // fallback: leave as-is
                try
                {
                    cmbStatus.DataSource = Enum.GetValues(typeof(StatusItem));
                    cmbStatus.SelectedItem = item.Status;
                }
                catch
                {
                    if (!string.IsNullOrEmpty(item.Status.ToString()))
                    {
                        cmbStatus.Items.Clear();
                        cmbStatus.Items.Add(item.Status.ToString());
                        cmbStatus.SelectedIndex = 0;
                    }
                }
            }

            // subscribe to vencimento changes to update allowed statuses
            dtpVencimento.ValueChanged += DtpVencimento_ValueChanged;

            // populate attachment UI
            UpdateAttachmentUI();
        }

        private void UpdateStatusOptions()
        {
            // remove 'Pendente' option when vencimento is <= today
            var today = DateTime.Now.Date;
            var allowed = Enum.GetValues(typeof(StatusItem)).Cast<StatusItem>()
                .Where(s => !(s == StatusItem.Pendente && dtpVencimento.Value.Date <= today))
                .ToList();

            // bind to combobox
            cmbStatus.DataSource = null;
            cmbStatus.DataSource = allowed;

            // if current edited status is not allowed, pick a sensible fallback (Vencido if present, else first)
            if (!allowed.Contains(EditedItem.Status))
            {
                if (allowed.Contains(StatusItem.Vencido))
                    cmbStatus.SelectedItem = StatusItem.Vencido;
                else if (allowed.Count > 0)
                    cmbStatus.SelectedItem = allowed[0];
            }
            else
            {
                cmbStatus.SelectedItem = EditedItem.Status;
            }
        }

        private void DtpVencimento_ValueChanged(object? sender, EventArgs e)
        {
            try
            {
                UpdateStatusOptions();
            }
            catch
            {
                // swallow
            }
        }

        private void UpdateAttachmentUI()
        {
            if (EditedItem.Attachments != null && EditedItem.Attachments.Count > 0)
            {
                var a = EditedItem.Attachments[0];
                lblAnexoName.Text = a.FileName;
                btnOpenAnexo.Enabled = true;
                btnReplaceAnexo.Text = "Substituir";
                btnRemoveAnexo.Enabled = true;
            }
            else
            {
                lblAnexoName.Text = "Sem anexo";
                btnOpenAnexo.Enabled = false;
                btnReplaceAnexo.Text = "Adicionar";
                btnRemoveAnexo.Enabled = false;
            }
        }

        private void BtnOpenAnexo_Click(object? sender, EventArgs e)
        {
            if (EditedItem.Attachments == null || EditedItem.Attachments.Count == 0) return;
            var a = EditedItem.Attachments[0];
            var path = dataService.GetAttachmentPath(a.FileName);
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

        private void BtnReplaceAnexo_Click(object? sender, EventArgs e)
        {
            // allowed extensions same as cadastro/ListagemControl
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
                    return;
                }

                var dest = dataService.GetAttachmentPath(Path.GetFileName(ofd.FileName));
                dest = dataService.GetUniqueAttachmentPath(dest);
                try
                {
                    File.Copy(ofd.FileName, dest);
                    var attach = new Attachment { Id = Guid.NewGuid().ToString(), FileName = Path.GetFileName(dest), RelativePath = dest, CreatedAt = DateTime.Now };

                    // if there was an existing attachment, delete its file
                    if (EditedItem.Attachments != null && EditedItem.Attachments.Count > 0)
                    {
                        var old = EditedItem.Attachments[0];
                        try { var oldPath = dataService.GetAttachmentPath(old.FileName); if (File.Exists(oldPath)) File.Delete(oldPath); } catch { }
                        EditedItem.Attachments.Clear();
                    }

                    EditedItem.Attachments.Add(attach);
                    UpdateAttachmentUI();
                }
                catch
                {
                    MessageBox.Show("Falha ao adicionar/substituir anexo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnRemoveAnexo_Click(object? sender, EventArgs e)
        {
            if (EditedItem.Attachments == null || EditedItem.Attachments.Count == 0) return;
            var res = MessageBox.Show("Deseja remover o anexo?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                var old = EditedItem.Attachments[0];
                try { var oldPath = dataService.GetAttachmentPath(old.FileName); if (File.Exists(oldPath)) File.Delete(oldPath); } catch { }
                EditedItem.Attachments.Clear();
                UpdateAttachmentUI();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // Validações (igual ao cadastro)
            if (string.IsNullOrWhiteSpace(txtNumero.Text))
            {
                MessageBox.Show("O campo Número é obrigatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumero.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtFornecedor.Text))
            {
                MessageBox.Show("O campo Fornecedor é obrigatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFornecedor.Focus();
                return;
            }
            if (numValor.Value <= 0)
            {
                MessageBox.Show("O valor deve ser maior que zero.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numValor.Focus();
                return;
            }
            if (cmbStatus.SelectedIndex < 0)
            {
                MessageBox.Show("Selecione um status.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbStatus.Focus();
                return;
            }

            var result = MessageBox.Show(
                "Deseja salvar as alterações?",
                "Confirmar Alterações",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                EditedItem.Tipo = rbBoleto.Checked ? TipoItem.Boleto : TipoItem.Nota;
                EditedItem.Numero = txtNumero.Text;
                EditedItem.Fornecedor = txtFornecedor.Text;
                EditedItem.Valor = numValor.Value;
                EditedItem.Vencimento = dtpVencimento.Value.Date;

                // robust parsing of selected status (combo is bound to enum)
                StatusItem selectedStatus;
                if (cmbStatus.SelectedItem is StatusItem s)
                    selectedStatus = s;
                else if (cmbStatus.SelectedItem != null && Enum.TryParse<StatusItem>(cmbStatus.SelectedItem.ToString(), out var parsed))
                    selectedStatus = parsed;
                else
                    selectedStatus = EditedItem.Status;

                // if item is overdue and selected status is Pendente, force to Vencido (safety)
                if (selectedStatus == StatusItem.Pendente && EditedItem.Vencimento.Date <= DateTime.Now.Date)
                {
                    selectedStatus = StatusItem.Vencido;
                }

                EditedItem.Status = selectedStatus;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
