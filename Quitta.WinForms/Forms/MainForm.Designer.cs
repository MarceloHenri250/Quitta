namespace Quitta.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            panelHeader = new Panel();
            btnSair = new Button();
            lblSubtitulo = new Label();
            lblTitulo = new Label();
            tabControl = new TabControl();
            tabDashboard = new TabPage();
            dashboardControl = new Quitta.UserControls.DashboardControl();
            tabBudget = new TabPage();
            budgetAnualControl = new Quitta.UserControls.BudgetAnualControl();
            tabCadastro = new TabPage();
            cadastrarControl = new Quitta.UserControls.CadastrarControl();
            tabListagem = new TabPage();
            listagemControl = new Quitta.UserControls.ListagemControl();
            tabRelatorios = new TabPage();
            relatorioControl = new Quitta.UserControls.RelatorioControl();
            panelHeader.SuspendLayout();
            tabControl.SuspendLayout();
            tabDashboard.SuspendLayout();
            tabBudget.SuspendLayout();
            tabCadastro.SuspendLayout();
            tabListagem.SuspendLayout();
            tabRelatorios.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.WhiteSmoke;
            panelHeader.Controls.Add(btnSair);
            panelHeader.Controls.Add(lblSubtitulo);
            panelHeader.Controls.Add(lblTitulo);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1382, 80);
            panelHeader.TabIndex = 0;
            // 
            // btnSair
            // 
            btnSair.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSair.FlatStyle = FlatStyle.Flat;
            btnSair.Font = new Font("Segoe UI", 10F);
            btnSair.Location = new Point(1270, 20);
            btnSair.Name = "btnSair";
            btnSair.Size = new Size(100, 40);
            btnSair.TabIndex = 3;
            btnSair.Text = "Sair";
            btnSair.UseVisualStyleBackColor = true;
            btnSair.Click += btnSair_Click;
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Font = new Font("Segoe UI", 10F);
            lblSubtitulo.ForeColor = Color.Gray;
            lblSubtitulo.Location = new Point(20, 45);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(286, 23);
            lblSubtitulo.TabIndex = 2;
            lblSubtitulo.Text = "Gerencie seus boletos e notas fiscais";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(17, 14);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(400, 38);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "Sistema de Gestão Financeira";
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabDashboard);
            tabControl.Controls.Add(tabBudget);
            tabControl.Controls.Add(tabCadastro);
            tabControl.Controls.Add(tabListagem);
            tabControl.Controls.Add(tabRelatorios);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Segoe UI", 10F);
            tabControl.Location = new Point(0, 80);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1382, 773);
            tabControl.TabIndex = 1;
            // 
            // tabDashboard
            // 
            tabDashboard.Controls.Add(dashboardControl);
            tabDashboard.Location = new Point(4, 32);
            tabDashboard.Name = "tabDashboard";
            tabDashboard.Padding = new Padding(3);
            tabDashboard.Size = new Size(1374, 737);
            tabDashboard.TabIndex = 0;
            tabDashboard.Text = "Dashboard";
            tabDashboard.UseVisualStyleBackColor = true;
            // 
            // dashboardControl
            // 
            dashboardControl.Dock = DockStyle.Fill;
            dashboardControl.Location = new Point(3, 3);
            dashboardControl.Name = "dashboardControl";
            dashboardControl.Size = new Size(1368, 731);
            dashboardControl.TabIndex = 0;
            // 
            // tabBudget
            // 
            tabBudget.Controls.Add(budgetAnualControl);
            tabBudget.Location = new Point(4, 32);
            tabBudget.Name = "tabBudget";
            tabBudget.Padding = new Padding(3);
            tabBudget.Size = new Size(192, 64);
            tabBudget.TabIndex = 1;
            tabBudget.Text = "Budget";
            tabBudget.UseVisualStyleBackColor = true;
            // 
            // budgetAnualControl
            // 
            budgetAnualControl.Dock = DockStyle.Fill;
            budgetAnualControl.Location = new Point(3, 3);
            budgetAnualControl.Name = "budgetAnualControl";
            budgetAnualControl.Size = new Size(186, 58);
            budgetAnualControl.TabIndex = 0;
            // 
            // tabCadastro
            // 
            tabCadastro.Controls.Add(cadastrarControl);
            tabCadastro.Location = new Point(4, 32);
            tabCadastro.Name = "tabCadastro";
            tabCadastro.Padding = new Padding(3);
            tabCadastro.Size = new Size(192, 64);
            tabCadastro.TabIndex = 2;
            tabCadastro.Text = "Cadastrar";
            tabCadastro.UseVisualStyleBackColor = true;
            // 
            // cadastrarControl
            // 
            cadastrarControl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cadastrarControl.Dock = DockStyle.Fill;
            cadastrarControl.Location = new Point(3, 3);
            cadastrarControl.Name = "cadastrarControl";
            cadastrarControl.Size = new Size(186, 58);
            cadastrarControl.TabIndex = 0;
            // 
            // tabListagem
            // 
            tabListagem.Controls.Add(listagemControl);
            tabListagem.Location = new Point(4, 32);
            tabListagem.Name = "tabListagem";
            tabListagem.Padding = new Padding(3);
            tabListagem.Size = new Size(192, 64);
            tabListagem.TabIndex = 3;
            tabListagem.Text = "Listagem";
            tabListagem.UseVisualStyleBackColor = true;
            // 
            // listagemControl
            // 
            listagemControl.Dock = DockStyle.Fill;
            listagemControl.Location = new Point(3, 3);
            listagemControl.Name = "listagemControl";
            listagemControl.Size = new Size(186, 58);
            listagemControl.TabIndex = 0;
            // 
            // tabRelatorios
            // 
            tabRelatorios.Controls.Add(relatorioControl);
            tabRelatorios.Location = new Point(4, 32);
            tabRelatorios.Name = "tabRelatorios";
            tabRelatorios.Padding = new Padding(3);
            tabRelatorios.Size = new Size(1374, 737);
            tabRelatorios.TabIndex = 5;
            tabRelatorios.Text = "Relatórios";
            tabRelatorios.UseVisualStyleBackColor = true;
            // 
            // relatorioControl
            // 
            relatorioControl.Dock = DockStyle.Fill;
            relatorioControl.Location = new Point(3, 3);
            relatorioControl.Name = "relatorioControl";
            relatorioControl.Padding = new Padding(6);
            relatorioControl.Size = new Size(1368, 731);
            relatorioControl.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1382, 853);
            Controls.Add(tabControl);
            Controls.Add(panelHeader);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Gestão Financeira";
            WindowState = FormWindowState.Maximized;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            tabControl.ResumeLayout(false);
            tabDashboard.ResumeLayout(false);
            tabBudget.ResumeLayout(false);
            tabCadastro.ResumeLayout(false);
            tabListagem.ResumeLayout(false);
            tabRelatorios.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private Label lblSubtitulo;
        private Label lblTitulo;
        private Button btnSair;
        private TabControl tabControl;
        private TabPage tabDashboard;
        private TabPage tabBudget;
        private TabPage tabCadastro;
        private TabPage tabListagem;
        private TabPage tabRelatorios;
        private TabPage tabConfiguracoes;
        private TableLayoutPanel tableLayoutCards;
        private Panel panelCardPendente;
        private Label lblValorPendente;
        private Label lblTituloPendente;
        private Panel panelTituloVencido;
        private Label lblValorVencido;
        private Label lblTituloVencido;
        private Panel panelCardPago;
        private Label lblValorPago;
        private Label lblTituloPago;
        private Panel panelCardBudget;
        private Label lblValorBudget;
        private Label lblTituloBudget;
        private Panel panelCardSaldo;
        private Label lblValorSaldo;
        private Label lblTituloSaldo;
        private DataGridView dgvVencimentos;
        private DataGridViewTextBoxColumn Tipo;
        private DataGridViewTextBoxColumn Número;
        private DataGridViewTextBoxColumn Fornecedor;
        private DataGridViewTextBoxColumn Valor;
        private DataGridViewTextBoxColumn Vencimento;
        private DataGridViewTextBoxColumn Status;
        private Label lblTituloVencimentos;
        // budget UI moved to BudgetAnualControl
        private Quitta.UserControls.DashboardControl dashboardControl;
        private Quitta.UserControls.BudgetAnualControl budgetAnualControl;
        private Quitta.UserControls.CadastrarControl cadastrarControl;
        private Quitta.UserControls.ListagemControl listagemControl;
        private Quitta.UserControls.RelatorioControl relatorioControl;
        private Panel panelFiltros;
        private Label lblBusca;
        private TextBox txtBusca;
        private Label lblTipoFiltro;
        private ComboBox cmbFiltroTipo;
        private Label lblStatusFiltro;
        private ComboBox cmbFiltroStatus;
        private Label lblPeriodo;
        private DateTimePicker dtpFiltroInicio;
        private Label lblAte;
        private DateTimePicker dtpFiltroFim;
        private Button btnFiltrar;
        private Button btnLimparFiltros;
        private DataGridView dgvListagem;
        private Panel panelPaginacao;
        private Button btnPrimeira;
        private Button btnAnterior;
        private Label lblPagina;
        private Button btnProxima;
        private Button btnUltima;
        private Panel panelRelatorioFiltros;
        private Label lblRelPeriodo;
        private DateTimePicker dtpRelInicio;
        private Label lblRelAte;
        private DateTimePicker dtpRelFim;
        private Label lblRelTipo;
        private ComboBox cmbRelTipo;
        private Label lblRelStatus;
        private ComboBox cmbRelStatus;
        private Button btnGerarRelatorio;
        private Panel panelRelatorioResultados;
        private Label lblTotalRelatorio;
        private Label lblTotalPagoRelatorio;
        private Label lblTotalPendenteRelatorio;
        private Label lblTotalVencidoRelatorio;
        private DataGridView dgvRelatorio;
        private Button btnExportarPDF;
        private Button btnExportarExcel;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewButtonColumn dataGridViewButtonColumn1;
        private DataGridViewButtonColumn dataGridViewButtonColumn2;
        private DataGridViewButtonColumn dataGridViewButtonColumn3;
    }
}