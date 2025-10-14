using Quitta.Models;
using Quitta.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            numValor.Value = item.Valor;
            dtpVencimento.Value = item.Vencimento;
            cmbStatus.SelectedItem = item.Status.ToString();

            // populate attachment UI
            UpdateAttachmentUI();
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
                EditedItem.Status = (StatusItem)Enum.Parse(typeof(StatusItem), cmbStatus.SelectedItem.ToString());

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
