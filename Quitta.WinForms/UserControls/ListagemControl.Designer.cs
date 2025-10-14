namespace Quitta.UserControls
{
    partial class ListagemControl
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            panelFiltros = new Panel();
            lblBusca = new Label();
            txtBusca = new TextBox();
            lblFiltroTipo = new Label();
            cmbFiltroTipo = new ComboBox();
            lblFiltroStatus = new Label();
            cmbFiltroStatus = new ComboBox();
            lblPeriodo = new Label();
            dtpFiltroInicio = new DateTimePicker();
            lblAte = new Label();
            dtpFiltroFim = new DateTimePicker();
            btnFiltrar = new Button();
            btnLimparFiltros = new Button();
            dgvListagem = new DataGridView();
            colTipo = new DataGridViewTextBoxColumn();
            colNumero = new DataGridViewTextBoxColumn();
            colFornecedor = new DataGridViewTextBoxColumn();
            colValor = new DataGridViewTextBoxColumn();
            colVencimento = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewComboBoxColumn();
            colAnexo = new DataGridViewButtonColumn();
            colEditar = new DataGridViewButtonColumn();
            colExcluir = new DataGridViewButtonColumn();
            panelPaginacao = new Panel();
            btnPrimeira = new Button();
            btnAnterior = new Button();
            lblPagina = new Label();
            btnProxima = new Button();
            btnUltima = new Button();
            panelFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvListagem).BeginInit();
            panelPaginacao.SuspendLayout();
            SuspendLayout();
            // 
            // panelFiltros
            // 
            panelFiltros.BackColor = Color.WhiteSmoke;
            panelFiltros.Controls.Add(lblBusca);
            panelFiltros.Controls.Add(txtBusca);
            panelFiltros.Controls.Add(lblFiltroTipo);
            panelFiltros.Controls.Add(cmbFiltroTipo);
            panelFiltros.Controls.Add(lblFiltroStatus);
            panelFiltros.Controls.Add(cmbFiltroStatus);
            panelFiltros.Controls.Add(lblPeriodo);
            panelFiltros.Controls.Add(dtpFiltroInicio);
            panelFiltros.Controls.Add(lblAte);
            panelFiltros.Controls.Add(dtpFiltroFim);
            panelFiltros.Controls.Add(btnFiltrar);
            panelFiltros.Controls.Add(btnLimparFiltros);
            panelFiltros.Dock = DockStyle.Top;
            panelFiltros.Location = new Point(0, 0);
            panelFiltros.Name = "panelFiltros";
            panelFiltros.Padding = new Padding(12);
            panelFiltros.Size = new Size(1400, 110);
            panelFiltros.TabIndex = 0;
            // 
            // lblBusca
            // 
            lblBusca.AutoSize = true;
            lblBusca.Font = new Font("Segoe UI", 9F);
            lblBusca.Location = new Point(20, 15);
            lblBusca.Name = "lblBusca";
            lblBusca.Size = new Size(55, 20);
            lblBusca.TabIndex = 0;
            lblBusca.Text = "Buscar:";
            // 
            // txtBusca
            // 
            txtBusca.Font = new Font("Segoe UI", 9F);
            txtBusca.Location = new Point(90, 12);
            txtBusca.Name = "txtBusca";
            txtBusca.PlaceholderText = "Digite número ou fornecedor...";
            txtBusca.Size = new Size(300, 27);
            txtBusca.TabIndex = 1;
            // 
            // lblFiltroTipo
            // 
            lblFiltroTipo.AutoSize = true;
            lblFiltroTipo.Font = new Font("Segoe UI", 9F);
            lblFiltroTipo.Location = new Point(420, 15);
            lblFiltroTipo.Name = "lblFiltroTipo";
            lblFiltroTipo.Size = new Size(42, 20);
            lblFiltroTipo.TabIndex = 2;
            lblFiltroTipo.Text = "Tipo:";
            // 
            // cmbFiltroTipo
            // 
            cmbFiltroTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroTipo.Font = new Font("Segoe UI", 9F);
            cmbFiltroTipo.Items.AddRange(new object[] { "Todos", "Boleto", "Nota" });
            cmbFiltroTipo.Location = new Point(470, 12);
            cmbFiltroTipo.Name = "cmbFiltroTipo";
            cmbFiltroTipo.Size = new Size(150, 28);
            cmbFiltroTipo.TabIndex = 3;
            // 
            // lblFiltroStatus
            // 
            lblFiltroStatus.AutoSize = true;
            lblFiltroStatus.Font = new Font("Segoe UI", 9F);
            lblFiltroStatus.Location = new Point(650, 15);
            lblFiltroStatus.Name = "lblFiltroStatus";
            lblFiltroStatus.Size = new Size(52, 20);
            lblFiltroStatus.TabIndex = 4;
            lblFiltroStatus.Text = "Status:";
            // 
            // cmbFiltroStatus
            // 
            cmbFiltroStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroStatus.Font = new Font("Segoe UI", 9F);
            cmbFiltroStatus.Items.AddRange(new object[] { "Todos", "Pendente", "Pago", "Vencido" });
            cmbFiltroStatus.Location = new Point(710, 12);
            cmbFiltroStatus.Name = "cmbFiltroStatus";
            cmbFiltroStatus.Size = new Size(150, 28);
            cmbFiltroStatus.TabIndex = 5;
            // 
            // lblPeriodo
            // 
            lblPeriodo.AutoSize = true;
            lblPeriodo.Font = new Font("Segoe UI", 9F);
            lblPeriodo.Location = new Point(20, 55);
            lblPeriodo.Name = "lblPeriodo";
            lblPeriodo.Size = new Size(63, 20);
            lblPeriodo.TabIndex = 6;
            lblPeriodo.Text = "Período:";
            // 
            // dtpFiltroInicio
            // 
            dtpFiltroInicio.Checked = false;
            dtpFiltroInicio.Font = new Font("Segoe UI", 9F);
            dtpFiltroInicio.Format = DateTimePickerFormat.Short;
            dtpFiltroInicio.Location = new Point(90, 52);
            dtpFiltroInicio.Name = "dtpFiltroInicio";
            dtpFiltroInicio.ShowCheckBox = true;
            dtpFiltroInicio.Size = new Size(150, 27);
            dtpFiltroInicio.TabIndex = 7;
            // 
            // lblAte
            // 
            lblAte.AutoSize = true;
            lblAte.Font = new Font("Segoe UI", 9F);
            lblAte.Location = new Point(250, 55);
            lblAte.Name = "lblAte";
            lblAte.Size = new Size(30, 20);
            lblAte.TabIndex = 8;
            lblAte.Text = "até";
            // 
            // dtpFiltroFim
            // 
            dtpFiltroFim.Checked = false;
            dtpFiltroFim.Font = new Font("Segoe UI", 9F);
            dtpFiltroFim.Format = DateTimePickerFormat.Short;
            dtpFiltroFim.Location = new Point(285, 52);
            dtpFiltroFim.Name = "dtpFiltroFim";
            dtpFiltroFim.ShowCheckBox = true;
            dtpFiltroFim.Size = new Size(150, 27);
            dtpFiltroFim.TabIndex = 9;
            // 
            // btnFiltrar
            // 
            btnFiltrar.FlatStyle = FlatStyle.Flat;
            btnFiltrar.Font = new Font("Segoe UI", 9F);
            btnFiltrar.Location = new Point(470, 52);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.Size = new Size(100, 30);
            btnFiltrar.TabIndex = 10;
            btnFiltrar.Text = "Filtrar";
            // 
            // btnLimparFiltros
            // 
            btnLimparFiltros.FlatStyle = FlatStyle.Flat;
            btnLimparFiltros.Font = new Font("Segoe UI", 9F);
            btnLimparFiltros.Location = new Point(580, 52);
            btnLimparFiltros.Name = "btnLimparFiltros";
            btnLimparFiltros.Size = new Size(120, 30);
            btnLimparFiltros.TabIndex = 11;
            btnLimparFiltros.Text = "Limpar Filtros";
            // 
            // dgvListagem
            // 
            dgvListagem.AllowUserToAddRows = false;
            dgvListagem.AllowUserToDeleteRows = false;
            dgvListagem.AllowUserToResizeColumns = false;
            dgvListagem.AllowUserToResizeRows = false;
            dgvListagem.AllowUserToOrderColumns = false;
            dgvListagem.AutoGenerateColumns = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(250, 250, 250);
            dgvListagem.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvListagem.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvListagem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvListagem.BackgroundColor = Color.White;
            dgvListagem.ScrollBars = ScrollBars.Both;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(240, 240, 240);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvListagem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvListagem.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvListagem.Columns.AddRange(new DataGridViewColumn[] { colTipo, colNumero, colFornecedor, colValor, colVencimento, colStatus, colAnexo, colEditar, colExcluir });
            dgvListagem.EnableHeadersVisualStyles = false;
            dgvListagem.Location = new Point(20, 132);
            dgvListagem.Name = "dgvListagem";
            dgvListagem.RowHeadersVisible = false;
            dgvListagem.RowHeadersWidth = 4;
            dgvListagem.RowTemplate.Height = 28;
            dgvListagem.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvListagem.Size = new Size(1340, 450);
            dgvListagem.TabIndex = 2;
            // 
            // colTipo
            // 
            colTipo.DataPropertyName = "Tipo";
            colTipo.HeaderText = "Tipo";
            colTipo.Name = "colTipo";
            colTipo.ReadOnly = true;
            colTipo.SortMode = DataGridViewColumnSortMode.NotSortable;
            colTipo.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colTipo.Width = 90;
            // 
            // colNumero
            // 
            colNumero.DataPropertyName = "Numero";
            colNumero.HeaderText = "Número";
            colNumero.Name = "colNumero";
            colNumero.ReadOnly = true;
            colNumero.SortMode = DataGridViewColumnSortMode.NotSortable;
            colNumero.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colNumero.Width = 120;
            // 
            // colFornecedor
            // 
            colFornecedor.DataPropertyName = "Fornecedor";
            colFornecedor.HeaderText = "Fornecedor";
            colFornecedor.Name = "colFornecedor";
            colFornecedor.ReadOnly = true;
            colFornecedor.SortMode = DataGridViewColumnSortMode.NotSortable;
            colFornecedor.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colFornecedor.Width = 300;
            // 
            // colValor
            // 
            colValor.DataPropertyName = "Valor";
            colValor.HeaderText = "Valor";
            colValor.DefaultCellStyle.Format = "C2";
            colValor.Name = "colValor";
            colValor.ReadOnly = true;
            colValor.SortMode = DataGridViewColumnSortMode.NotSortable;
            colValor.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colValor.Width = 100;
            // 
            // colVencimento
            // 
            colVencimento.DataPropertyName = "Vencimento";
            colVencimento.HeaderText = "Vencimento";
            colVencimento.DefaultCellStyle.Format = "dd/MM/yyyy";
            colVencimento.Name = "colVencimento";
            colVencimento.ReadOnly = true;
            colVencimento.SortMode = DataGridViewColumnSortMode.NotSortable;
            colVencimento.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colVencimento.Width = 100;
            // 
            // colStatus
            // 
            ((DataGridViewComboBoxColumn)colStatus).DataPropertyName = "Status";
            ((DataGridViewComboBoxColumn)colStatus).HeaderText = "Status";
            ((DataGridViewComboBoxColumn)colStatus).Name = "colStatus";
            ((DataGridViewComboBoxColumn)colStatus).ReadOnly = false;
            ((DataGridViewComboBoxColumn)colStatus).DataSource = Enum.GetValues(typeof(Quitta.Models.StatusItem));
            ((DataGridViewComboBoxColumn)colStatus).ValueType = typeof(Quitta.Models.StatusItem);
            ((DataGridViewComboBoxColumn)colStatus).SortMode = DataGridViewColumnSortMode.NotSortable;
            ((DataGridViewComboBoxColumn)colStatus).AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ((DataGridViewComboBoxColumn)colStatus).Width = 90;
            // 
            // colAnexo
            // 
            ((DataGridViewButtonColumn)colAnexo).DataPropertyName = "AnexoDisplay";
            ((DataGridViewButtonColumn)colAnexo).HeaderText = "Anexo";
            ((DataGridViewButtonColumn)colAnexo).Name = "colAnexo";
            ((DataGridViewButtonColumn)colAnexo).ReadOnly = true;
            ((DataGridViewButtonColumn)colAnexo).SortMode = DataGridViewColumnSortMode.NotSortable;
            ((DataGridViewButtonColumn)colAnexo).AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ((DataGridViewButtonColumn)colAnexo).Width = 140;
            ((DataGridViewButtonColumn)colAnexo).FillWeight = 9;
            ((DataGridViewButtonColumn)colAnexo).UseColumnTextForButtonValue = false;
            // 
            // colEditar
            // 
            colEditar.HeaderText = "Editar";
            colEditar.MinimumWidth = 6;
            colEditar.Name = "colEditar";
            colEditar.ReadOnly = true;
            colEditar.Text = "Editar";
            colEditar.UseColumnTextForButtonValue = true;
            colEditar.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            colEditar.FillWeight = 6;
            // 
            // colExcluir
            // 
            colExcluir.HeaderText = "Excluir";
            colExcluir.MinimumWidth = 6;
            colExcluir.Name = "colExcluir";
            colExcluir.ReadOnly = true;
            colExcluir.Text = "Excluir";
            colExcluir.UseColumnTextForButtonValue = true;
            colExcluir.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            colExcluir.FillWeight = 6;
            // 
            // panelPaginacao
            // 
            panelPaginacao.Controls.Add(btnPrimeira);
            panelPaginacao.Controls.Add(btnAnterior);
            panelPaginacao.Controls.Add(lblPagina);
            panelPaginacao.Controls.Add(btnProxima);
            panelPaginacao.Controls.Add(btnUltima);
            panelPaginacao.Dock = DockStyle.Bottom;
            panelPaginacao.Location = new Point(0, 600);
            panelPaginacao.Name = "panelPaginacao";
            panelPaginacao.Size = new Size(1400, 50);
            panelPaginacao.TabIndex = 3;
            // 
            // btnPrimeira
            // 
            btnPrimeira.FlatStyle = FlatStyle.Flat;
            btnPrimeira.Font = new Font("Segoe UI", 9F);
            btnPrimeira.Location = new Point(20, 10);
            btnPrimeira.Name = "btnPrimeira";
            btnPrimeira.Size = new Size(40, 30);
            btnPrimeira.TabIndex = 0;
            btnPrimeira.Text = "|<";
            // 
            // btnAnterior
            // 
            btnAnterior.FlatStyle = FlatStyle.Flat;
            btnAnterior.Font = new Font("Segoe UI", 9F);
            btnAnterior.Location = new Point(70, 10);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Size = new Size(40, 30);
            btnAnterior.TabIndex = 1;
            btnAnterior.Text = "<";
            // 
            // lblPagina
            // 
            lblPagina.AutoSize = true;
            lblPagina.Font = new Font("Segoe UI", 9F);
            lblPagina.Location = new Point(130, 15);
            lblPagina.Name = "lblPagina";
            lblPagina.Size = new Size(98, 20);
            lblPagina.TabIndex = 2;
            lblPagina.Text = "Página 1 de 1";
            // 
            // btnProxima
            // 
            btnProxima.FlatStyle = FlatStyle.Flat;
            btnProxima.Font = new Font("Segoe UI", 9F);
            btnProxima.Location = new Point(260, 10);
            btnProxima.Name = "btnProxima";
            btnProxima.Size = new Size(40, 30);
            btnProxima.TabIndex = 3;
            btnProxima.Text = ">";
            // 
            // btnUltima
            // 
            btnUltima.FlatStyle = FlatStyle.Flat;
            btnUltima.Font = new Font("Segoe UI", 9F);
            btnUltima.Location = new Point(310, 10);
            btnUltima.Name = "btnUltima";
            btnUltima.Size = new Size(40, 30);
            btnUltima.TabIndex = 4;
            btnUltima.Text = ">|";
            // 
            // ListagemControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvListagem);
            Controls.Add(panelFiltros);
            Controls.Add(panelPaginacao);
            Name = "ListagemControl";
            Size = new Size(1400, 650);
            panelFiltros.ResumeLayout(false);
            panelFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvListagem).EndInit();
            panelPaginacao.ResumeLayout(false);
            panelPaginacao.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelFiltros;
        private DataGridView dgvListagem;
        private Panel panelPaginacao;

        // filter controls
        private Label lblBusca;
        private TextBox txtBusca;
        private Label lblFiltroTipo;
        private ComboBox cmbFiltroTipo;
        private Label lblFiltroStatus;
        private ComboBox cmbFiltroStatus;
        private Label lblPeriodo;
        private DateTimePicker dtpFiltroInicio;
        private Label lblAte;
        private DateTimePicker dtpFiltroFim;
        private Button btnFiltrar;
        private Button btnLimparFiltros;

        // datagrid columns
        private DataGridViewTextBoxColumn colTipo;
        private DataGridViewTextBoxColumn colNumero;
        private DataGridViewTextBoxColumn colFornecedor;
        private DataGridViewTextBoxColumn colValor;
        private DataGridViewTextBoxColumn colVencimento;
        private DataGridViewComboBoxColumn colStatus;
        private DataGridViewButtonColumn colAnexo;
        private DataGridViewButtonColumn colEditar;
        private DataGridViewButtonColumn colExcluir;

        // pagination
        private Button btnPrimeira;
        private Button btnAnterior;
        private Label lblPagina;
        private Button btnProxima;
        private Button btnUltima;
    }
}
