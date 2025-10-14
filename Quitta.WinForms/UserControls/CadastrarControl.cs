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
    public partial class CadastrarControl : UserControl
    {
        public event Action<Item>? ItemCreated;
        public event Action<Attachment>? AttachmentAdded;

        private readonly DataService dataService = new DataService();

        // Mantém o caminho do arquivo selecionado antes do salvamento
        private string? pendingAttachmentSourcePath;

        public CadastrarControl()
        {
            InitializeComponent();
            // wire internal button clicks
            btnCadastrar.Click += BtnCadastrar_Click;
            btnLimpar.Click += (s, e) => ClearForm();

            btnAnexar.Click += BtnAnexar_Click;
            lstAnexos.DoubleClick += LstAnexos_DoubleClick;

            // ensure status items set (designer already adds them but keep safe)
            if (cmbStatus.Items.Count == 0)
            {
                cmbStatus.Items.Add("Pendente");
                cmbStatus.Items.Add("Pago");
                cmbStatus.Items.Add("Vencido");
            }

            if (cmbStatus.Items.Count > 0 && cmbStatus.SelectedIndex < 0)
                cmbStatus.SelectedIndex = 0;
        }

        private void BtnAnexar_Click(object? sender, EventArgs e)
        {
            // Require type selected before allowing attach
            if (!rbBoleto.Checked && !rbNota.Checked)
            {
                MessageBox.Show("Selecione o tipo (Boleto ou Nota Fiscal) antes de anexar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var ofd = new OpenFileDialog();
            ofd.Title = "Anexar Boleto/Nota";
            ofd.Filter = "PDF files (*.pdf)|*.pdf|Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // If there is an existing pending attachment, just replace it (no file deletion since not yet copied)
                pendingAttachmentSourcePath = ofd.FileName;

                // show single item in UI (file name only)
                lstAnexos.Items.Clear();
                lstAnexos.Items.Add(Path.GetFileName(pendingAttachmentSourcePath));

                // do not copy file yet; will copy on save
            }
        }

        private void LstAnexos_DoubleClick(object? sender, EventArgs e)
        {
            if (lstAnexos.SelectedItem == null) return;

            // If there's a pending source path (not yet saved), open that
            if (!string.IsNullOrEmpty(pendingAttachmentSourcePath))
            {
                var source = pendingAttachmentSourcePath!;
                if (File.Exists(source))
                {
                    try
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                        {
                            FileName = source,
                            UseShellExecute = true
                        });
                    }
                    catch
                    {
                        MessageBox.Show("Não foi possível abrir o anexo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Arquivo de anexo não encontrado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                return;
            }

            // No pending source: nothing to open in this control context
        }

        private void BtnCadastrar_Click(object? sender, EventArgs e)
        {
            // Validações usando os controles internos
            if (string.IsNullOrWhiteSpace(txtNumero.Text))
            {
                MessageBox.Show("O campo Número é obrigatório.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumero.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFornecedor.Text))
            {
                MessageBox.Show("O campo Fornecedor é obrigatório.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFornecedor.Focus();
                return;
            }

            if (numValor.Value <= 0)
            {
                MessageBox.Show("O valor deve ser maior que zero.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numValor.Focus();
                return;
            }

            if (cmbStatus.SelectedIndex < 0 || string.IsNullOrEmpty(cmbStatus.SelectedItem?.ToString()))
            {
                MessageBox.Show("Selecione um status.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbStatus.Focus();
                return;
            }

            var novoItem = new Item
            {
                Id = Guid.NewGuid().ToString(),
                Tipo = rbBoleto.Checked ? TipoItem.Boleto : TipoItem.Nota,
                Numero = txtNumero.Text,
                Fornecedor = txtFornecedor.Text,
                Valor = numValor.Value,
                Vencimento = dtpVencimento.Value.Date,
                Status = (StatusItem)Enum.Parse(typeof(StatusItem), cmbStatus.SelectedItem!.ToString()!)
            };

            // If there's a pending attachment selected, copy it now into data attachments folder and create Attachment
            if (!string.IsNullOrEmpty(pendingAttachmentSourcePath) && File.Exists(pendingAttachmentSourcePath))
            {
                var source = pendingAttachmentSourcePath!;
                var destFileName = Path.GetFileName(source);
                var dest = dataService.GetAttachmentPath(destFileName);
                dest = dataService.GetUniqueAttachmentPath(dest);
                File.Copy(source, dest);

                var relative = Path.GetRelativePath(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), dest);

                var attachment = new Attachment
                {
                    Id = Guid.NewGuid().ToString(),
                    FileName = Path.GetFileName(dest),
                    RelativePath = relative,
                    CreatedAt = DateTime.Now
                };

                novoItem.Attachments.Add(attachment);

                // inform parent if needed
                AttachmentAdded?.Invoke(attachment);

                // clear pending source
                pendingAttachmentSourcePath = null;
            }

            // raise event to notify parent form
            ItemCreated?.Invoke(novoItem);

            // clear UI list
            lstAnexos.Items.Clear();
        }

        public string Numero => txtNumero.Text;
        public string Fornecedor => txtFornecedor.Text;
        public decimal Valor => numValor.Value;
        public DateTime Vencimento => dtpVencimento.Value.Date;
        public int SelectedStatusIndex => cmbStatus.SelectedIndex;
        public string? SelectedStatusString => cmbStatus.SelectedItem?.ToString();
        public bool IsBoleto => rbBoleto.Checked;

        public void ClearForm()
        {
            // If there's a pending attachment selection (not yet copied), just forget it
            pendingAttachmentSourcePath = null;
            lstAnexos.Items.Clear();

            rbBoleto.Checked = true;
            txtNumero.Clear();
            txtFornecedor.Clear();
            numValor.Value = 0;
            dtpVencimento.Value = DateTime.Now;
            if (cmbStatus.Items.Count > 0)
                cmbStatus.SelectedIndex = 0;
            txtNumero.Focus();
        }

        private void TryDeleteAttachmentFile(Attachment attachment)
        {
            try
            {
                var absolute = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), attachment.RelativePath);
                if (File.Exists(absolute))
                    File.Delete(absolute);
            }
            catch
            {
                // failing to delete is not critical; ignore
            }
        }
    }
}
