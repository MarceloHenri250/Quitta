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
            this.lblTipo = new System.Windows.Forms.Label();
            this.rbBoleto = new System.Windows.Forms.RadioButton();
            this.rbNota = new System.Windows.Forms.RadioButton();
            this.lblNumero = new System.Windows.Forms.Label();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.lblFornecedor = new System.Windows.Forms.Label();
            this.txtFornecedor = new System.Windows.Forms.TextBox();
            this.lblValor = new System.Windows.Forms.Label();
            this.numValor = new System.Windows.Forms.NumericUpDown();
            this.lblVencimento = new System.Windows.Forms.Label();
            this.dtpVencimento = new System.Windows.Forms.DateTimePicker();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblAnexoLabel = new System.Windows.Forms.Label();
            this.lblAnexoName = new System.Windows.Forms.Label();
            this.btnOpenAnexo = new System.Windows.Forms.Button();
            this.btnReplaceAnexo = new System.Windows.Forms.Button();
            this.btnRemoveAnexo = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.numValor)).BeginInit();
            this.SuspendLayout();

            // 
            // EditItemDialog (Form)
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 620);
            this.Name = "EditItemDialog";
            this.Text = "Editar Item";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(245, 245, 245);

            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(24, 18);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(32, 15);
            this.lblTipo.TabIndex = 0;
            this.lblTipo.Text = "Tipo:";
            this.lblTipo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblTipo.ForeColor = Color.FromArgb(60, 60, 60);

            // 
            // rbBoleto
            // 
            this.rbBoleto.AutoSize = true;
            this.rbBoleto.Location = new System.Drawing.Point(24, 44);
            this.rbBoleto.Name = "rbBoleto";
            this.rbBoleto.Size = new System.Drawing.Size(60, 19);
            this.rbBoleto.TabIndex = 1;
            this.rbBoleto.Text = "Boleto";
            this.rbBoleto.UseVisualStyleBackColor = true;

            // 
            // rbNota
            // 
            this.rbNota.AutoSize = true;
            this.rbNota.Location = new System.Drawing.Point(120, 44);
            this.rbNota.Name = "rbNota";
            this.rbNota.Size = new System.Drawing.Size(86, 19);
            this.rbNota.TabIndex = 2;
            this.rbNota.Text = "Nota Fiscal";
            this.rbNota.UseVisualStyleBackColor = true;

            // 
            // lblNumero
            // 
            this.lblNumero.AutoSize = true;
            this.lblNumero.Location = new System.Drawing.Point(24, 88);
            this.lblNumero.Name = "lblNumero";
            this.lblNumero.Size = new System.Drawing.Size(51, 15);
            this.lblNumero.TabIndex = 3;
            this.lblNumero.Text = "Número:";
            this.lblNumero.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblNumero.ForeColor = Color.FromArgb(60, 60, 60);

            // 
            // txtNumero
            // 
            this.txtNumero.Location = new System.Drawing.Point(24, 112);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(712, 23);
            this.txtNumero.TabIndex = 4;

            // 
            // lblFornecedor
            // 
            this.lblFornecedor.AutoSize = true;
            this.lblFornecedor.Location = new System.Drawing.Point(24, 150);
            this.lblFornecedor.Name = "lblFornecedor";
            this.lblFornecedor.Size = new System.Drawing.Size(72, 15);
            this.lblFornecedor.TabIndex = 5;
            this.lblFornecedor.Text = "Fornecedor:";
            this.lblFornecedor.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblFornecedor.ForeColor = Color.FromArgb(60, 60, 60);

            // 
            // txtFornecedor
            // 
            this.txtFornecedor.Location = new System.Drawing.Point(24, 174);
            this.txtFornecedor.Name = "txtFornecedor";
            this.txtFornecedor.Size = new System.Drawing.Size(712, 23);
            this.txtFornecedor.TabIndex = 6;

            // 
            // lblValor
            // 
            this.lblValor.AutoSize = true;
            this.lblValor.Location = new System.Drawing.Point(24, 214);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(39, 15);
            this.lblValor.TabIndex = 7;
            this.lblValor.Text = "Valor:";
            this.lblValor.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblValor.ForeColor = Color.FromArgb(60, 60, 60);

            // 
            // numValor
            // 
            this.numValor.Location = new System.Drawing.Point(24, 238);
            this.numValor.Name = "numValor";
            this.numValor.Size = new System.Drawing.Size(240, 23);
            this.numValor.DecimalPlaces = 2;
            this.numValor.Maximum = 9999999;
            this.numValor.Minimum = 0;
            this.numValor.ThousandsSeparator = true;
            this.numValor.TabIndex = 8;

            // 
            // lblVencimento
            // 
            this.lblVencimento.AutoSize = true;
            this.lblVencimento.Location = new System.Drawing.Point(300, 214);
            this.lblVencimento.Name = "lblVencimento";
            this.lblVencimento.Size = new System.Drawing.Size(74, 15);
            this.lblVencimento.TabIndex = 9;
            this.lblVencimento.Text = "Vencimento:";
            this.lblVencimento.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblVencimento.ForeColor = Color.FromArgb(60, 60, 60);

            // 
            // dtpVencimento
            // 
            this.dtpVencimento.Location = new System.Drawing.Point(300, 238);
            this.dtpVencimento.Name = "dtpVencimento";
            this.dtpVencimento.Size = new System.Drawing.Size(160, 23);
            this.dtpVencimento.Format = DateTimePickerFormat.Short;
            this.dtpVencimento.TabIndex = 10;

            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(24, 280);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(44, 15);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "Status:";
            this.lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblStatus.ForeColor = Color.FromArgb(60, 60, 60);

            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Location = new System.Drawing.Point(24, 304);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(240, 23);
            this.cmbStatus.TabIndex = 12;

            // 
            // lblAnexoLabel
            // 
            this.lblAnexoLabel.AutoSize = true;
            this.lblAnexoLabel.Location = new System.Drawing.Point(24, 352);
            this.lblAnexoLabel.Name = "lblAnexoLabel";
            this.lblAnexoLabel.Size = new System.Drawing.Size(45, 15);
            this.lblAnexoLabel.TabIndex = 13;
            this.lblAnexoLabel.Text = "Anexo:";
            this.lblAnexoLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.lblAnexoLabel.ForeColor = Color.FromArgb(60, 60, 60);

            // 
            // lblAnexoName
            // 
            this.lblAnexoName.Location = new System.Drawing.Point(24, 376);
            this.lblAnexoName.Name = "lblAnexoName";
            this.lblAnexoName.Size = new System.Drawing.Size(480, 30);
            this.lblAnexoName.TabIndex = 14;
            this.lblAnexoName.Text = "Sem anexo";
            this.lblAnexoName.BorderStyle = BorderStyle.FixedSingle;
            this.lblAnexoName.TextAlign = ContentAlignment.MiddleLeft;

            // 
            // btnOpenAnexo
            // 
            this.btnOpenAnexo.Location = new System.Drawing.Point(520, 376);
            this.btnOpenAnexo.Name = "btnOpenAnexo";
            this.btnOpenAnexo.Size = new System.Drawing.Size(80, 30);
            this.btnOpenAnexo.TabIndex = 15;
            this.btnOpenAnexo.Text = "Abrir";
            this.btnOpenAnexo.UseVisualStyleBackColor = true;

            // 
            // btnReplaceAnexo
            // 
            this.btnReplaceAnexo.Location = new System.Drawing.Point(610, 376);
            this.btnReplaceAnexo.Name = "btnReplaceAnexo";
            this.btnReplaceAnexo.Size = new System.Drawing.Size(80, 30);
            this.btnReplaceAnexo.TabIndex = 16;
            this.btnReplaceAnexo.Text = "Substituir";
            this.btnReplaceAnexo.UseVisualStyleBackColor = true;

            // 
            // btnRemoveAnexo
            // 
            this.btnRemoveAnexo.Location = new System.Drawing.Point(520, 418);
            this.btnRemoveAnexo.Name = "btnRemoveAnexo";
            this.btnRemoveAnexo.Size = new System.Drawing.Size(170, 36);
            this.btnRemoveAnexo.TabIndex = 17;
            this.btnRemoveAnexo.Text = "Remover";
            this.btnRemoveAnexo.UseVisualStyleBackColor = true;

            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(24, 500);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(320, 48);
            this.btnSalvar.TabIndex = 18;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.BackColor = Color.FromArgb(10, 6, 27);
            this.btnSalvar.ForeColor = Color.White;
            this.btnSalvar.FlatStyle = FlatStyle.Flat;
            this.btnSalvar.FlatAppearance.BorderSize = 0;

            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(368, 500);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(368, 48);
            this.btnCancelar.TabIndex = 19;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.FlatStyle = FlatStyle.Flat;
            this.btnCancelar.BackColor = Color.White;
            this.btnCancelar.ForeColor = Color.FromArgb(40, 40, 40);
            this.btnCancelar.FlatAppearance.BorderColor = Color.FromArgb(100, 100, 100);
            this.btnCancelar.FlatAppearance.BorderSize = 1;

            // add to form
            this.Controls.Add(this.lblTipo);
            this.Controls.Add(this.rbBoleto);
            this.Controls.Add(this.rbNota);
            this.Controls.Add(this.lblNumero);
            this.Controls.Add(this.txtNumero);
            this.Controls.Add(this.lblFornecedor);
            this.Controls.Add(this.txtFornecedor);
            this.Controls.Add(this.lblValor);
            this.Controls.Add(this.numValor);
            this.Controls.Add(this.lblVencimento);
            this.Controls.Add(this.dtpVencimento);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cmbStatus);

            this.Controls.Add(this.lblAnexoLabel);
            this.Controls.Add(this.lblAnexoName);
            this.Controls.Add(this.btnOpenAnexo);
            this.Controls.Add(this.btnReplaceAnexo);
            this.Controls.Add(this.btnRemoveAnexo);

            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnCancelar);

            ((System.ComponentModel.ISupportInitialize)(this.numValor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}