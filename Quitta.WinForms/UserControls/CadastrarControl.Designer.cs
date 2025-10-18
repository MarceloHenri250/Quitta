namespace Quitta.UserControls
{
    partial class CadastrarControl
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            lblCadastroTitulo = new Label();
            groupBoxForm = new GroupBox();
            rbBoleto = new RadioButton();
            rbNota = new RadioButton();
            lblTipo = new Label();
            lblNumero = new Label();
            txtNumero = new TextBox();
            lblFornecedor = new Label();
            txtFornecedor = new TextBox();
            lblValor = new Label();
            numValor = new NumericUpDown();
            lblVencimento = new Label();
            dtpVencimento = new DateTimePicker();
            lblStatus = new Label();
            cmbStatus = new ComboBox();
            btnAnexar = new Button();
            lstAnexos = new ListBox();
            groupBoxUpload = new GroupBox();
            btnOpenAttachment = new Button();
            btnDeleteAttachment = new Button();
            btnUpload = new Button();
            lblUploadInfo = new Label();
            lblExtracaoDev = new Label();
            btnLimpar = new Button();
            btnCadastrar = new Button();
            panelButtons = new Panel();
            groupBoxForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numValor).BeginInit();
            groupBoxUpload.SuspendLayout();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // lblCadastroTitulo
            // 
            lblCadastroTitulo.AutoSize = true;
            lblCadastroTitulo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblCadastroTitulo.Location = new Point(20, 20);
            lblCadastroTitulo.Name = "lblCadastroTitulo";
            lblCadastroTitulo.Size = new Size(409, 32);
            lblCadastroTitulo.TabIndex = 0;
            lblCadastroTitulo.Text = "Cadastrar Novo Boleto/Nota Fiscal";
            // 
            // groupBoxForm
            // 
            groupBoxForm.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            groupBoxForm.Controls.Add(panelButtons);
            groupBoxForm.Controls.Add(rbBoleto);
            groupBoxForm.Controls.Add(rbNota);
            groupBoxForm.Controls.Add(lblTipo);
            groupBoxForm.Controls.Add(lblNumero);
            groupBoxForm.Controls.Add(txtNumero);
            groupBoxForm.Controls.Add(lblFornecedor);
            groupBoxForm.Controls.Add(txtFornecedor);
            groupBoxForm.Controls.Add(lblValor);
            groupBoxForm.Controls.Add(numValor);
            groupBoxForm.Controls.Add(lblVencimento);
            groupBoxForm.Controls.Add(dtpVencimento);
            groupBoxForm.Controls.Add(lblStatus);
            groupBoxForm.Controls.Add(cmbStatus);
            groupBoxForm.Location = new Point(20, 70);
            groupBoxForm.Name = "groupBoxForm";
            groupBoxForm.Size = new Size(760, 760);
            groupBoxForm.TabIndex = 1;
            groupBoxForm.TabStop = false;
            groupBoxForm.Text = "Dados do Boleto/Nota";
            // 
            // rbBoleto
            // 
            rbBoleto.AutoSize = true;
            rbBoleto.Checked = true;
            rbBoleto.Location = new Point(20, 60);
            rbBoleto.Name = "rbBoleto";
            rbBoleto.Size = new Size(74, 24);
            rbBoleto.TabIndex = 1;
            rbBoleto.TabStop = true;
            rbBoleto.Text = "Boleto";
            rbBoleto.UseVisualStyleBackColor = true;
            // 
            // rbNota
            // 
            rbNota.AutoSize = true;
            rbNota.Location = new Point(140, 60);
            rbNota.Name = "rbNota";
            rbNota.Size = new Size(103, 24);
            rbNota.TabIndex = 2;
            rbNota.Text = "Nota Fiscal";
            rbNota.UseVisualStyleBackColor = true;
            // 
            // lblTipo
            // 
            lblTipo.AutoSize = true;
            lblTipo.Location = new Point(20, 30);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(42, 20);
            lblTipo.TabIndex = 0;
            lblTipo.Text = "Tipo:";
            // 
            // lblNumero
            // 
            lblNumero.AutoSize = true;
            lblNumero.Location = new Point(20, 114);
            lblNumero.Name = "lblNumero";
            lblNumero.Size = new Size(66, 20);
            lblNumero.TabIndex = 3;
            lblNumero.Text = "Número:";
            // 
            // txtNumero
            // 
            txtNumero.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtNumero.Font = new Font("Segoe UI", 10F);
            txtNumero.Location = new Point(20, 139);
            txtNumero.Name = "txtNumero";
            txtNumero.Size = new Size(700, 30);
            txtNumero.TabIndex = 4;
            // 
            // lblFornecedor
            // 
            lblFornecedor.AutoSize = true;
            lblFornecedor.Location = new Point(20, 179);
            lblFornecedor.Name = "lblFornecedor";
            lblFornecedor.Size = new Size(87, 20);
            lblFornecedor.TabIndex = 5;
            lblFornecedor.Text = "Fornecedor:";
            // 
            // txtFornecedor
            // 
            txtFornecedor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFornecedor.Font = new Font("Segoe UI", 10F);
            txtFornecedor.Location = new Point(20, 204);
            txtFornecedor.Name = "txtFornecedor";
            txtFornecedor.Size = new Size(700, 30);
            txtFornecedor.TabIndex = 6;
            // 
            // lblValor
            // 
            lblValor.AutoSize = true;
            lblValor.Location = new Point(20, 244);
            lblValor.Name = "lblValor";
            lblValor.Size = new Size(46, 20);
            lblValor.TabIndex = 7;
            lblValor.Text = "Valor:";
            // 
            // numValor
            // 
            numValor.DecimalPlaces = 2;
            numValor.Location = new Point(20, 269);
            numValor.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            numValor.Name = "numValor";
            numValor.Size = new Size(300, 27);
            numValor.TabIndex = 8;
            numValor.ThousandsSeparator = true;
            // 
            // lblVencimento
            // 
            lblVencimento.AutoSize = true;
            lblVencimento.Location = new Point(340, 244);
            lblVencimento.Name = "lblVencimento";
            lblVencimento.Size = new Size(90, 20);
            lblVencimento.TabIndex = 9;
            lblVencimento.Text = "Vencimento:";
            // 
            // dtpVencimento
            // 
            dtpVencimento.Format = DateTimePickerFormat.Short;
            dtpVencimento.Location = new Point(340, 269);
            dtpVencimento.Name = "dtpVencimento";
            dtpVencimento.Size = new Size(200, 27);
            dtpVencimento.TabIndex = 10;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(20, 309);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(52, 20);
            lblStatus.TabIndex = 11;
            lblStatus.Text = "Status:";
            // 
            // cmbStatus
            // 
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Items.AddRange(new object[] { "Pendente", "Pago", "Vencido" });
            cmbStatus.Location = new Point(20, 334);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(200, 28);
            cmbStatus.TabIndex = 12;
            // 
            // btnAnexar
            // 
            btnAnexar.Location = new Point(0, 0);
            btnAnexar.Name = "btnAnexar";
            btnAnexar.Size = new Size(75, 23);
            btnAnexar.TabIndex = 0;
            // 
            // lstAnexos
            // 
            lstAnexos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstAnexos.BorderStyle = BorderStyle.FixedSingle;
            lstAnexos.Location = new Point(20, 160);
            lstAnexos.Name = "lstAnexos";
            lstAnexos.Size = new Size(520, 322);
            lstAnexos.TabIndex = 3;
            // 
            // groupBoxUpload
            // 
            groupBoxUpload.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxUpload.Controls.Add(btnOpenAttachment);
            groupBoxUpload.Controls.Add(btnDeleteAttachment);
            groupBoxUpload.Controls.Add(btnUpload);
            groupBoxUpload.Controls.Add(lblUploadInfo);
            groupBoxUpload.Controls.Add(lstAnexos);
            groupBoxUpload.Controls.Add(lblExtracaoDev);
            groupBoxUpload.Location = new Point(800, 70);
            groupBoxUpload.Name = "groupBoxUpload";
            groupBoxUpload.Size = new Size(560, 760);
            groupBoxUpload.TabIndex = 2;
            groupBoxUpload.TabStop = false;
            groupBoxUpload.Text = "Upload de Boleto/Nota (Extração Automática)";
            // 
            // btnOpenAttachment
            // 
            btnOpenAttachment.Anchor = AnchorStyles.Bottom;
            btnOpenAttachment.FlatStyle = FlatStyle.System;
            btnOpenAttachment.Location = new Point(110, 520);
            btnOpenAttachment.Name = "btnOpenAttachment";
            btnOpenAttachment.Size = new Size(140, 36);
            btnOpenAttachment.TabIndex = 5;
            btnOpenAttachment.Text = "Abrir Anexo";
            btnOpenAttachment.UseVisualStyleBackColor = true;
            // 
            // btnDeleteAttachment
            // 
            btnDeleteAttachment.Anchor = AnchorStyles.Bottom;
            btnDeleteAttachment.FlatStyle = FlatStyle.System;
            btnDeleteAttachment.Location = new Point(310, 520);
            btnDeleteAttachment.Name = "btnDeleteAttachment";
            btnDeleteAttachment.Size = new Size(140, 36);
            btnDeleteAttachment.TabIndex = 6;
            btnDeleteAttachment.Text = "Excluir Anexo";
            btnDeleteAttachment.UseVisualStyleBackColor = true;
            // 
            // btnUpload
            // 
            btnUpload.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnUpload.FlatStyle = FlatStyle.Flat;
            btnUpload.Location = new Point(20, 40);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(520, 48);
            btnUpload.TabIndex = 0;
            btnUpload.Text = "Selecionar Arquivo...";
            btnUpload.Click += BtnAnexar_Click;
            // 
            // lblUploadInfo
            // 
            lblUploadInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblUploadInfo.Location = new Point(20, 100);
            lblUploadInfo.Name = "lblUploadInfo";
            lblUploadInfo.RightToLeft = RightToLeft.No;
            lblUploadInfo.Size = new Size(520, 48);
            lblUploadInfo.TabIndex = 1;
            lblUploadInfo.Text = "(Opcional) Selecione um arquivo PDF ou imagem do boleto/nota para anexar.";
            lblUploadInfo.TextAlign = ContentAlignment.MiddleLeft;
            lblUploadInfo.UseMnemonic = false;
            // 
            // lblExtracaoDev
            // 
            lblExtracaoDev.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblExtracaoDev.AutoSize = true;
            lblExtracaoDev.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
            lblExtracaoDev.ForeColor = Color.Gray;
            lblExtracaoDev.Location = new Point(20, 720);
            lblExtracaoDev.Name = "lblExtracaoDev";
            lblExtracaoDev.Size = new Size(282, 19);
            lblExtracaoDev.TabIndex = 4;
            lblExtracaoDev.Text = "(Extração automática: Em desenvolvimento)";
            // 
            // btnLimpar
            // 
            btnLimpar.FlatStyle = FlatStyle.Flat;
            btnLimpar.Location = new Point(330, 12);
            btnLimpar.Name = "btnLimpar";
            btnLimpar.Size = new Size(200, 40);
            btnLimpar.TabIndex = 14;
            btnLimpar.Text = "Limpar Formulário";
            // 
            // btnCadastrar
            // 
            btnCadastrar.BackColor = Color.FromArgb(3, 2, 19);
            btnCadastrar.FlatStyle = FlatStyle.Flat;
            btnCadastrar.ForeColor = Color.White;
            btnCadastrar.Location = new Point(10, 12);
            btnCadastrar.Name = "btnCadastrar";
            btnCadastrar.Size = new Size(300, 40);
            btnCadastrar.TabIndex = 13;
            btnCadastrar.Text = "Cadastrar";
            btnCadastrar.UseVisualStyleBackColor = false;
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(btnCadastrar);
            panelButtons.Controls.Add(btnLimpar);
            panelButtons.Dock = DockStyle.Bottom;
            panelButtons.Location = new Point(3, 685);
            panelButtons.Name = "panelButtons";
            panelButtons.Padding = new Padding(10);
            panelButtons.Size = new Size(754, 72);
            panelButtons.TabIndex = 20;
            // 
            // CadastrarControl
            // 
            Controls.Add(groupBoxUpload);
            Controls.Add(groupBoxForm);
            Controls.Add(lblCadastroTitulo);
            Name = "CadastrarControl";
            Size = new Size(1400, 900);
            groupBoxForm.ResumeLayout(false);
            groupBoxForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numValor).EndInit();
            groupBoxUpload.ResumeLayout(false);
            groupBoxUpload.PerformLayout();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCadastroTitulo;
        private GroupBox groupBoxForm;
        private Label lblTipo;
        private RadioButton rbBoleto;
        private RadioButton rbNota;
        private Label lblNumero;
        private TextBox txtNumero;
        private Label lblFornecedor;
        private TextBox txtFornecedor;
        private Label lblValor;
        private NumericUpDown numValor;
        private Label lblVencimento;
        private DateTimePicker dtpVencimento;
        private Label lblStatus;
        private ComboBox cmbStatus;
        private Button btnAnexar;
        private ListBox lstAnexos;
        private GroupBox groupBoxUpload;
        private Button btnUpload;
        private Label lblUploadInfo;
        private Label lblExtracaoDev;
        private Button btnOpenAttachment;
        private Button btnDeleteAttachment;
        private Panel panelButtons;
        private Button btnCadastrar;
        private Button btnLimpar;
    }
}
