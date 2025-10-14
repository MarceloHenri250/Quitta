using System;
using System.Drawing;
using System.Windows.Forms;

namespace Quitta.Forms
{
    partial class EditItemDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controls
        private RadioButton rbBoleto;
        private RadioButton rbNota;
        private TextBox txtNumero;
        private TextBox txtFornecedor;
        private NumericUpDown numValor;
        private DateTimePicker dtpVencimento;
        private ComboBox cmbStatus;
        private Button btnSalvar;
        private Button btnCancelar;

        // Labels
        private Label lblTipo;
        private Label lblNumero;
        private Label lblFornecedor;
        private Label lblValor;
        private Label lblVencimento;
        private Label lblStatus;

        // attachment controls
        private Label lblAnexoLabel;
        private Label lblAnexoName;
        private Button btnOpenAnexo;
        private Button btnReplaceAnexo;
        private Button btnRemoveAnexo;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Text = "Editar Item";
            this.Size = new Size(520, 560);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            lblTipo = new Label { Text = "Tipo:", Location = new Point(20, 20), AutoSize = true };
            rbBoleto = new RadioButton { Text = "Boleto", Location = new Point(20, 45), AutoSize = true };
            rbNota = new RadioButton { Text = "Nota Fiscal", Location = new Point(140, 45), AutoSize = true };

            lblNumero = new Label { Text = "Número:", Location = new Point(20, 80), AutoSize = true };
            txtNumero = new TextBox { Location = new Point(20, 105), Size = new Size(460, 30) };

            lblFornecedor = new Label { Text = "Fornecedor:", Location = new Point(20, 145), AutoSize = true };
            txtFornecedor = new TextBox { Location = new Point(20, 170), Size = new Size(460, 30) };

            lblValor = new Label { Text = "Valor:", Location = new Point(20, 210), AutoSize = true };
            numValor = new NumericUpDown { Location = new Point(20, 235), Size = new Size(200, 30), DecimalPlaces = 2, Maximum = 999999, ThousandsSeparator = true };

            lblVencimento = new Label { Text = "Vencimento:", Location = new Point(240, 210), AutoSize = true };
            dtpVencimento = new DateTimePicker { Location = new Point(240, 235), Size = new Size(120, 30), Format = DateTimePickerFormat.Short };

            lblStatus = new Label { Text = "Status:", Location = new Point(20, 275), AutoSize = true };
            cmbStatus = new ComboBox { Location = new Point(20, 300), Size = new Size(200, 31), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbStatus.Items.AddRange(new object[] { "Pendente", "Pago", "Vencido" });

            // attachment area
            lblAnexoLabel = new Label { Text = "Anexo:", Location = new Point(20, 345), AutoSize = true };
            lblAnexoName = new Label { Text = "Sem anexo", Location = new Point(20, 370), AutoSize = false, Size = new Size(300, 24), BorderStyle = BorderStyle.FixedSingle };

            btnOpenAnexo = new Button { Text = "Abrir", Location = new Point(330, 365), Size = new Size(70, 30), FlatStyle = FlatStyle.Flat };
            btnReplaceAnexo = new Button { Text = "Substituir", Location = new Point(410, 365), Size = new Size(70, 30), FlatStyle = FlatStyle.Flat };
            btnRemoveAnexo = new Button { Text = "Remover", Location = new Point(330, 405), Size = new Size(150, 30), FlatStyle = FlatStyle.Flat };

            btnSalvar = new Button { Text = "Salvar", Location = new Point(20, 460), Size = new Size(200, 40), BackColor = Color.FromArgb(3, 2, 19), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnCancelar = new Button { Text = "Cancelar", Location = new Point(240, 460), Size = new Size(240, 40), FlatStyle = FlatStyle.Flat };

            // event hookups
            btnOpenAnexo.Click += BtnOpenAnexo_Click;
            btnReplaceAnexo.Click += BtnReplaceAnexo_Click;
            btnRemoveAnexo.Click += BtnRemoveAnexo_Click;
            btnSalvar.Click += btnSalvar_Click;
            btnCancelar.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            // add to form
            this.Controls.Add(lblTipo);
            this.Controls.Add(rbBoleto);
            this.Controls.Add(rbNota);
            this.Controls.Add(lblNumero);
            this.Controls.Add(txtNumero);
            this.Controls.Add(lblFornecedor);
            this.Controls.Add(txtFornecedor);
            this.Controls.Add(lblValor);
            this.Controls.Add(numValor);
            this.Controls.Add(lblVencimento);
            this.Controls.Add(dtpVencimento);
            this.Controls.Add(lblStatus);
            this.Controls.Add(cmbStatus);

            // attachment controls
            this.Controls.Add(lblAnexoLabel);
            this.Controls.Add(lblAnexoName);
            this.Controls.Add(btnOpenAnexo);
            this.Controls.Add(btnReplaceAnexo);
            this.Controls.Add(btnRemoveAnexo);

            this.Controls.Add(btnSalvar);
            this.Controls.Add(btnCancelar);
        }

        #endregion
    }
}