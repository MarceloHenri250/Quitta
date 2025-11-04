namespace Quitta.UserControls
{
    partial class RelatorioControl
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controls
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.Panel pnlFilters;

        private System.Windows.Forms.ComboBox cmbPeriodo;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.ComboBox cmbFornecedor;
        private System.Windows.Forms.DateTimePicker dtpInicio;
        private System.Windows.Forms.DateTimePicker dtpFim;
        private System.Windows.Forms.NumericUpDown nudMin;
        private System.Windows.Forms.NumericUpDown nudMax;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnClear;

        private System.Windows.Forms.TableLayoutPanel tableRight;
        private System.Windows.Forms.Panel pnlStats;
        private System.Windows.Forms.DataGridView dgvPreview;
        private System.Windows.Forms.FlowLayoutPanel pnlButtons;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnExportPdf;
        private System.Windows.Forms.Panel chartStatus;

        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.Label lblToPayValue;
        private System.Windows.Forms.Label lblToPayCount;
        private System.Windows.Forms.Label lblPaidValue;
        private System.Windows.Forms.Label lblPaidCount;
        private System.Windows.Forms.Label lblOverdueValue;
        private System.Windows.Forms.Label lblOverdueCount;

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
            splitMain = new SplitContainer();
            pnlFilters = new Panel();
            lblPeriodo = new Label();
            cmbPeriodo = new ComboBox();
            lblPersonalizado = new Label();
            lblInicio = new Label();
            dtpInicio = new DateTimePicker();
            lblFim = new Label();
            dtpFim = new DateTimePicker();
            lblTipo = new Label();
            cmbTipo = new ComboBox();
            lblStatus = new Label();
            cmbStatus = new ComboBox();
            lblFornecedor = new Label();
            cmbFornecedor = new ComboBox();
            lblFaixa = new Label();
            lblMin = new Label();
            nudMin = new NumericUpDown();
            lblMax = new Label();
            nudMax = new NumericUpDown();
            btnApply = new Button();
            btnClear = new Button();
            tableRight = new TableLayoutPanel();
            pnlStats = new Panel();
            chartStatus = new Panel();
            tableStats = new TableLayoutPanel();
            lblTotalTitle = new Label();
            lblTotalValue = new Label();
            lblToPayTitle = new Label();
            lblToPayValue = new Label();
            lblPaidTitle = new Label();
            lblPaidValue = new Label();
            lblOverdueTitle = new Label();
            lblOverdueValue = new Label();
            lblTotalCount = new Label();
            lblToPayCount = new Label();
            lblPaidCount = new Label();
            lblOverdueCount = new Label();
            dgvPreview = new DataGridView();
            pnlButtons = new FlowLayoutPanel();
            btnExportPdf = new Button();
            btnExportExcel = new Button();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            pnlFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMax).BeginInit();
            tableRight.SuspendLayout();
            pnlStats.SuspendLayout();
            tableStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPreview).BeginInit();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // splitMain
            // 
            splitMain.Dock = DockStyle.Fill;
            splitMain.FixedPanel = FixedPanel.Panel1;
            splitMain.Location = new Point(6, 6);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(pnlFilters);
            splitMain.Panel1MinSize = 200;
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(tableRight);
            splitMain.Panel2MinSize = 320;
            splitMain.Size = new Size(1388, 638);
            splitMain.SplitterDistance = 200;
            splitMain.TabIndex = 0;
            // 
            // pnlFilters
            // 
            pnlFilters.AutoScroll = true;
            pnlFilters.Controls.Add(lblPeriodo);
            pnlFilters.Controls.Add(cmbPeriodo);
            pnlFilters.Controls.Add(lblPersonalizado);
            pnlFilters.Controls.Add(lblInicio);
            pnlFilters.Controls.Add(dtpInicio);
            pnlFilters.Controls.Add(lblFim);
            pnlFilters.Controls.Add(dtpFim);
            pnlFilters.Controls.Add(lblTipo);
            pnlFilters.Controls.Add(cmbTipo);
            pnlFilters.Controls.Add(lblStatus);
            pnlFilters.Controls.Add(cmbStatus);
            pnlFilters.Controls.Add(lblFornecedor);
            pnlFilters.Controls.Add(cmbFornecedor);
            pnlFilters.Controls.Add(lblFaixa);
            pnlFilters.Controls.Add(lblMin);
            pnlFilters.Controls.Add(nudMin);
            pnlFilters.Controls.Add(lblMax);
            pnlFilters.Controls.Add(nudMax);
            pnlFilters.Controls.Add(btnApply);
            pnlFilters.Controls.Add(btnClear);
            pnlFilters.Dock = DockStyle.Fill;
            pnlFilters.Location = new Point(0, 0);
            pnlFilters.Name = "pnlFilters";
            pnlFilters.Padding = new Padding(8);
            pnlFilters.Size = new Size(200, 638);
            pnlFilters.TabIndex = 0;
            // 
            // lblPeriodo
            // 
            lblPeriodo.AutoSize = true;
            lblPeriodo.Location = new Point(8, 40);
            lblPeriodo.Name = "lblPeriodo";
            lblPeriodo.Size = new Size(63, 20);
            lblPeriodo.TabIndex = 0;
            lblPeriodo.Text = "Período:";
            // 
            // cmbPeriodo
            // 
            cmbPeriodo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPeriodo.Location = new Point(8, 62);
            cmbPeriodo.Name = "cmbPeriodo";
            cmbPeriodo.Size = new Size(184, 28);
            cmbPeriodo.TabIndex = 1;
            // 
            // lblPersonalizado
            // 
            lblPersonalizado.AutoSize = true;
            lblPersonalizado.Location = new Point(8, 96);
            lblPersonalizado.Name = "lblPersonalizado";
            lblPersonalizado.Size = new Size(159, 20);
            lblPersonalizado.TabIndex = 2;
            lblPersonalizado.Text = "Período Personalizado:";
            // 
            // lblInicio
            // 
            lblInicio.AutoSize = true;
            lblInicio.Location = new Point(8, 128);
            lblInicio.Name = "lblInicio";
            lblInicio.Size = new Size(48, 20);
            lblInicio.TabIndex = 3;
            lblInicio.Text = "Início:";
            // 
            // dtpInicio
            // 
            dtpInicio.Format = DateTimePickerFormat.Short;
            dtpInicio.Location = new Point(64, 124);
            dtpInicio.Name = "dtpInicio";
            dtpInicio.Size = new Size(128, 27);
            dtpInicio.TabIndex = 4;
            // 
            // lblFim
            // 
            lblFim.AutoSize = true;
            lblFim.Location = new Point(8, 160);
            lblFim.Name = "lblFim";
            lblFim.Size = new Size(36, 20);
            lblFim.TabIndex = 5;
            lblFim.Text = "Fim:";
            // 
            // dtpFim
            // 
            dtpFim.Format = DateTimePickerFormat.Short;
            dtpFim.Location = new Point(64, 156);
            dtpFim.Name = "dtpFim";
            dtpFim.Size = new Size(128, 27);
            dtpFim.TabIndex = 6;
            // 
            // lblTipo
            // 
            lblTipo.AutoSize = true;
            lblTipo.Location = new Point(8, 192);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(42, 20);
            lblTipo.TabIndex = 7;
            lblTipo.Text = "Tipo:";
            // 
            // cmbTipo
            // 
            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipo.Location = new Point(8, 214);
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new Size(184, 28);
            cmbTipo.TabIndex = 8;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(8, 248);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(52, 20);
            lblStatus.TabIndex = 9;
            lblStatus.Text = "Status:";
            // 
            // cmbStatus
            // 
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Location = new Point(8, 270);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(184, 28);
            cmbStatus.TabIndex = 10;
            // 
            // lblFornecedor
            // 
            lblFornecedor.AutoSize = true;
            lblFornecedor.Location = new Point(8, 304);
            lblFornecedor.Name = "lblFornecedor";
            lblFornecedor.Size = new Size(87, 20);
            lblFornecedor.TabIndex = 11;
            lblFornecedor.Text = "Fornecedor:";
            // 
            // cmbFornecedor
            // 
            cmbFornecedor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFornecedor.Location = new Point(8, 326);
            cmbFornecedor.Name = "cmbFornecedor";
            cmbFornecedor.Size = new Size(184, 28);
            cmbFornecedor.TabIndex = 12;
            // 
            // lblFaixa
            // 
            lblFaixa.AutoSize = true;
            lblFaixa.Location = new Point(8, 360);
            lblFaixa.Name = "lblFaixa";
            lblFaixa.Size = new Size(104, 20);
            lblFaixa.TabIndex = 13;
            lblFaixa.Text = "Faixa de Valor:";
            // 
            // lblMin
            // 
            lblMin.AutoSize = true;
            lblMin.Location = new Point(8, 392);
            lblMin.Name = "lblMin";
            lblMin.Size = new Size(63, 20);
            lblMin.TabIndex = 14;
            lblMin.Text = "Mínimo:";
            // 
            // nudMin
            // 
            nudMin.DecimalPlaces = 2;
            nudMin.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            nudMin.Location = new Point(8, 414);
            nudMin.Maximum = new decimal(new int[] { -1, -1, -1, 0 });
            nudMin.Name = "nudMin";
            nudMin.Size = new Size(184, 27);
            nudMin.TabIndex = 15;
            nudMin.ThousandsSeparator = true;
            // 
            // lblMax
            // 
            lblMax.AutoSize = true;
            lblMax.Location = new Point(8, 448);
            lblMax.Name = "lblMax";
            lblMax.Size = new Size(66, 20);
            lblMax.TabIndex = 16;
            lblMax.Text = "Máximo:";
            // 
            // nudMax
            // 
            nudMax.DecimalPlaces = 2;
            nudMax.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            nudMax.Location = new Point(8, 470);
            nudMax.Maximum = new decimal(new int[] { -1, -1, -1, 0 });
            nudMax.Name = "nudMax";
            nudMax.Size = new Size(184, 27);
            nudMax.TabIndex = 17;
            nudMax.ThousandsSeparator = true;
            // 
            // btnApply
            // 
            btnApply.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnApply.Location = new Point(8, 504);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(88, 39);
            btnApply.TabIndex = 18;
            btnApply.Text = "Aplicar";
            // 
            // btnClear
            // 
            btnClear.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnClear.Location = new Point(104, 504);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(88, 39);
            btnClear.TabIndex = 19;
            btnClear.Text = "Limpar";
            // 
            // tableRight
            // 
            tableRight.ColumnCount = 1;
            tableRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableRight.Controls.Add(pnlStats, 0, 0);
            tableRight.Controls.Add(dgvPreview, 0, 1);
            tableRight.Controls.Add(pnlButtons, 0, 2);
            tableRight.Dock = DockStyle.Fill;
            tableRight.Location = new Point(0, 0);
            tableRight.Name = "tableRight";
            tableRight.Padding = new Padding(10);
            tableRight.RowCount = 3;
            tableRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            tableRight.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tableRight.Size = new Size(1184, 638);
            tableRight.TabIndex = 0;
            // 
            // pnlStats
            // 
            pnlStats.Controls.Add(chartStatus);
            pnlStats.Controls.Add(tableStats);
            pnlStats.Dock = DockStyle.Fill;
            pnlStats.Location = new Point(13, 13);
            pnlStats.Name = "pnlStats";
            pnlStats.Size = new Size(1158, 84);
            pnlStats.TabIndex = 0;
            // 
            // chartStatus
            // 
            chartStatus.Dock = DockStyle.Right;
            chartStatus.Location = new Point(948, 0);
            chartStatus.Name = "chartStatus";
            chartStatus.Size = new Size(210, 84);
            chartStatus.TabIndex = 5;
            // 
            // tableStats
            // 
            tableStats.ColumnCount = 4;
            tableStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableStats.Controls.Add(lblTotalTitle, 0, 0);
            tableStats.Controls.Add(lblTotalValue, 0, 1);
            tableStats.Controls.Add(lblToPayTitle, 1, 0);
            tableStats.Controls.Add(lblToPayValue, 1, 1);
            tableStats.Controls.Add(lblPaidTitle, 2, 0);
            tableStats.Controls.Add(lblPaidValue, 2, 1);
            tableStats.Controls.Add(lblOverdueTitle, 3, 0);
            tableStats.Controls.Add(lblOverdueValue, 3, 1);
            tableStats.Controls.Add(lblTotalCount, 0, 2);
            tableStats.Controls.Add(lblToPayCount, 1, 2);
            tableStats.Controls.Add(lblPaidCount, 2, 2);
            tableStats.Controls.Add(lblOverdueCount, 3, 2);
            tableStats.Location = new Point(0, 0);
            tableStats.Name = "tableStats";
            tableStats.RowCount = 3;
            tableStats.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableStats.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tableStats.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableStats.Size = new Size(1125, 84);
            tableStats.TabIndex = 0;
            // 
            // lblTotalTitle
            // 
            lblTotalTitle.Dock = DockStyle.Fill;
            lblTotalTitle.Location = new Point(3, 0);
            lblTotalTitle.Name = "lblTotalTitle";
            lblTotalTitle.Size = new Size(275, 42);
            lblTotalTitle.TabIndex = 0;
            lblTotalTitle.Text = "Total Geral";
            lblTotalTitle.TextAlign = ContentAlignment.BottomLeft;
            // 
            // lblTotalValue
            // 
            lblTotalValue.Dock = DockStyle.Fill;
            lblTotalValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalValue.Location = new Point(3, 42);
            lblTotalValue.Name = "lblTotalValue";
            lblTotalValue.Size = new Size(275, 25);
            lblTotalValue.TabIndex = 1;
            lblTotalValue.Text = "R$ 0,00";
            lblTotalValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblToPayTitle
            // 
            lblToPayTitle.Dock = DockStyle.Fill;
            lblToPayTitle.Location = new Point(284, 0);
            lblToPayTitle.Name = "lblToPayTitle";
            lblToPayTitle.Size = new Size(275, 42);
            lblToPayTitle.TabIndex = 2;
            lblToPayTitle.Text = "A Pagar";
            lblToPayTitle.TextAlign = ContentAlignment.BottomLeft;
            // 
            // lblToPayValue
            // 
            lblToPayValue.Dock = DockStyle.Fill;
            lblToPayValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblToPayValue.Location = new Point(284, 42);
            lblToPayValue.Name = "lblToPayValue";
            lblToPayValue.Size = new Size(275, 25);
            lblToPayValue.TabIndex = 3;
            lblToPayValue.Text = "R$ 0,00";
            lblToPayValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblPaidTitle
            // 
            lblPaidTitle.Dock = DockStyle.Fill;
            lblPaidTitle.Location = new Point(565, 0);
            lblPaidTitle.Name = "lblPaidTitle";
            lblPaidTitle.Size = new Size(275, 42);
            lblPaidTitle.TabIndex = 4;
            lblPaidTitle.Text = "Pagos";
            lblPaidTitle.TextAlign = ContentAlignment.BottomLeft;
            // 
            // lblPaidValue
            // 
            lblPaidValue.Dock = DockStyle.Fill;
            lblPaidValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPaidValue.Location = new Point(565, 42);
            lblPaidValue.Name = "lblPaidValue";
            lblPaidValue.Size = new Size(275, 25);
            lblPaidValue.TabIndex = 5;
            lblPaidValue.Text = "R$ 0,00";
            lblPaidValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblOverdueTitle
            // 
            lblOverdueTitle.Dock = DockStyle.Fill;
            lblOverdueTitle.Location = new Point(846, 0);
            lblOverdueTitle.Name = "lblOverdueTitle";
            lblOverdueTitle.Size = new Size(276, 42);
            lblOverdueTitle.TabIndex = 6;
            lblOverdueTitle.Text = "Vencidos";
            lblOverdueTitle.TextAlign = ContentAlignment.BottomLeft;
            // 
            // lblOverdueValue
            // 
            lblOverdueValue.Dock = DockStyle.Fill;
            lblOverdueValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblOverdueValue.Location = new Point(846, 42);
            lblOverdueValue.Name = "lblOverdueValue";
            lblOverdueValue.Size = new Size(276, 25);
            lblOverdueValue.TabIndex = 7;
            lblOverdueValue.Text = "R$ 0,00";
            lblOverdueValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTotalCount
            // 
            lblTotalCount.Dock = DockStyle.Fill;
            lblTotalCount.Font = new Font("Segoe UI", 8F);
            lblTotalCount.Location = new Point(3, 67);
            lblTotalCount.Name = "lblTotalCount";
            lblTotalCount.Size = new Size(275, 17);
            lblTotalCount.TabIndex = 8;
            lblTotalCount.Text = "0 itens";
            // 
            // lblToPayCount
            // 
            lblToPayCount.Dock = DockStyle.Fill;
            lblToPayCount.Font = new Font("Segoe UI", 8F);
            lblToPayCount.Location = new Point(284, 67);
            lblToPayCount.Name = "lblToPayCount";
            lblToPayCount.Size = new Size(275, 17);
            lblToPayCount.TabIndex = 9;
            lblToPayCount.Text = "0 itens";
            // 
            // lblPaidCount
            // 
            lblPaidCount.Dock = DockStyle.Fill;
            lblPaidCount.Font = new Font("Segoe UI", 8F);
            lblPaidCount.Location = new Point(565, 67);
            lblPaidCount.Name = "lblPaidCount";
            lblPaidCount.Size = new Size(275, 17);
            lblPaidCount.TabIndex = 10;
            lblPaidCount.Text = "0 itens";
            // 
            // lblOverdueCount
            // 
            lblOverdueCount.Dock = DockStyle.Fill;
            lblOverdueCount.Font = new Font("Segoe UI", 8F);
            lblOverdueCount.Location = new Point(846, 67);
            lblOverdueCount.Name = "lblOverdueCount";
            lblOverdueCount.Size = new Size(276, 17);
            lblOverdueCount.TabIndex = 11;
            lblOverdueCount.Text = "0 itens";
            // 
            // dgvPreview
            // 
            dgvPreview.AllowUserToAddRows = false;
            dgvPreview.AllowUserToDeleteRows = false;
            dgvPreview.AllowUserToResizeRows = false;
            dgvPreview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPreview.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPreview.Dock = DockStyle.Fill;
            dgvPreview.Location = new Point(14, 104);
            dgvPreview.Margin = new Padding(4);
            dgvPreview.MinimumSize = new Size(200, 150);
            dgvPreview.Name = "dgvPreview";
            dgvPreview.ReadOnly = true;
            dgvPreview.RowHeadersVisible = false;
            dgvPreview.RowHeadersWidth = 51;
            dgvPreview.RowTemplate.Height = 22;
            dgvPreview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPreview.Size = new Size(1156, 472);
            dgvPreview.TabIndex = 1;
            // 
            // pnlButtons
            // 
            pnlButtons.AutoSize = true;
            pnlButtons.Controls.Add(btnExportPdf);
            pnlButtons.Controls.Add(btnExportExcel);
            pnlButtons.Dock = DockStyle.Fill;
            pnlButtons.FlowDirection = FlowDirection.RightToLeft;
            pnlButtons.Location = new Point(13, 583);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Padding = new Padding(8);
            pnlButtons.Size = new Size(1158, 42);
            pnlButtons.TabIndex = 2;
            // 
            // btnExportPdf
            // 
            btnExportPdf.Anchor = AnchorStyles.Right;
            btnExportPdf.AutoSize = true;
            btnExportPdf.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnExportPdf.Location = new Point(1026, 14);
            btnExportPdf.Margin = new Padding(6);
            btnExportPdf.MinimumSize = new Size(110, 30);
            btnExportPdf.Name = "btnExportPdf";
            btnExportPdf.Size = new Size(110, 30);
            btnExportPdf.TabIndex = 0;
            btnExportPdf.Text = "Exportar PDF";
            // 
            // btnExportExcel
            // 
            btnExportExcel.Anchor = AnchorStyles.Right;
            btnExportExcel.AutoSize = true;
            btnExportExcel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnExportExcel.Location = new Point(901, 14);
            btnExportExcel.Margin = new Padding(6);
            btnExportExcel.MinimumSize = new Size(110, 30);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new Size(113, 30);
            btnExportExcel.TabIndex = 0;
            btnExportExcel.Text = "Exportar Excel";
            // 
            // RelatorioControl
            // 
            Controls.Add(splitMain);
            Name = "RelatorioControl";
            Padding = new Padding(6);
            Size = new Size(1400, 650);
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            pnlFilters.ResumeLayout(false);
            pnlFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMax).EndInit();
            tableRight.ResumeLayout(false);
            tableRight.PerformLayout();
            pnlStats.ResumeLayout(false);
            tableStats.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPreview).EndInit();
            pnlButtons.ResumeLayout(false);
            pnlButtons.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lblPeriodo;
        private Label lblPersonalizado;
        private Label lblInicio;
        private Label lblFim;
        private Label lblTipo;
        private Label lblStatus;
        private Label lblFornecedor;
        private Label lblFaixa;
        private Label lblMin;
        private Label lblMax;
        private TableLayoutPanel tableStats;
        private Label lblTotalTitle;
        private Label lblToPayTitle;
        private Label lblPaidTitle;
        private Label lblOverdueTitle;
    }
}
