namespace Quitta.UserControls
{
    partial class DashboardControl
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
            tableLayoutCards = new TableLayoutPanel();
            panelCardPendente = new Panel();
            lblValorPendente = new Label();
            lblTituloPendente = new Label();
            panelTituloVencido = new Panel();
            lblValorVencido = new Label();
            lblTituloVencido = new Label();
            panelCardPago = new Panel();
            lblValorPago = new Label();
            lblTituloPago = new Label();
            panelCardBudget = new Panel();
            lblValorBudget = new Label();
            lblTituloBudget = new Label();
            panelCardSaldo = new Panel();
            lblValorSaldo = new Label();
            lblTituloSaldo = new Label();
            lblTituloVencimentos = new Label();
            dgvVencimentos = new DataGridView();
            mainLayout = new TableLayoutPanel();
            panelGrafico = new Panel();
            lblGraficoTitulo = new Label();
            lblGraficoSub = new Label();
            picGrafico = new PictureBox();
            panelTabela = new Panel();
            lblTabelaTitulo = new Label();
            lblTabelaSub = new Label();
            tableLayoutCards.SuspendLayout();
            panelCardPendente.SuspendLayout();
            panelTituloVencido.SuspendLayout();
            panelCardPago.SuspendLayout();
            panelCardBudget.SuspendLayout();
            panelCardSaldo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVencimentos).BeginInit();
            mainLayout.SuspendLayout();
            panelGrafico.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picGrafico).BeginInit();
            panelTabela.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutCards
            // 
            tableLayoutCards.ColumnCount = 5;
            tableLayoutCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutCards.Controls.Add(panelCardPendente, 0, 0);
            tableLayoutCards.Controls.Add(panelTituloVencido, 1, 0);
            tableLayoutCards.Controls.Add(panelCardPago, 2, 0);
            tableLayoutCards.Controls.Add(panelCardBudget, 3, 0);
            tableLayoutCards.Controls.Add(panelCardSaldo, 4, 0);
            tableLayoutCards.Dock = DockStyle.Top;
            tableLayoutCards.Location = new Point(23, 23);
            tableLayoutCards.Name = "tableLayoutCards";
            tableLayoutCards.RowCount = 1;
            tableLayoutCards.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutCards.Size = new Size(1328, 120);
            tableLayoutCards.TabIndex = 0;
            // 
            // panelCardPendente
            // 
            panelCardPendente.BackColor = Color.FromArgb(254, 243, 199);
            panelCardPendente.BorderStyle = BorderStyle.FixedSingle;
            panelCardPendente.Controls.Add(lblValorPendente);
            panelCardPendente.Controls.Add(lblTituloPendente);
            panelCardPendente.Dock = DockStyle.Fill;
            panelCardPendente.Location = new Point(10, 10);
            panelCardPendente.Margin = new Padding(10);
            panelCardPendente.Name = "panelCardPendente";
            panelCardPendente.Size = new Size(245, 100);
            panelCardPendente.TabIndex = 0;
            // 
            // lblValorPendente
            // 
            lblValorPendente.AutoSize = true;
            lblValorPendente.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblValorPendente.ForeColor = Color.FromArgb(133, 77, 14);
            lblValorPendente.Location = new Point(10, 40);
            lblValorPendente.Name = "lblValorPendente";
            lblValorPendente.Size = new Size(115, 38);
            lblValorPendente.TabIndex = 1;
            lblValorPendente.Text = "R$ 0,00";
            // 
            // lblTituloPendente
            // 
            lblTituloPendente.AutoSize = true;
            lblTituloPendente.Location = new Point(10, 10);
            lblTituloPendente.Name = "lblTituloPendente";
            lblTituloPendente.Size = new Size(107, 20);
            lblTituloPendente.TabIndex = 0;
            lblTituloPendente.Text = "Total Pendente";
            // 
            // panelTituloVencido
            // 
            panelTituloVencido.BackColor = Color.FromArgb(254, 226, 226);
            panelTituloVencido.BorderStyle = BorderStyle.FixedSingle;
            panelTituloVencido.Controls.Add(lblValorVencido);
            panelTituloVencido.Controls.Add(lblTituloVencido);
            panelTituloVencido.Dock = DockStyle.Fill;
            panelTituloVencido.Location = new Point(275, 10);
            panelTituloVencido.Margin = new Padding(10);
            panelTituloVencido.Name = "panelTituloVencido";
            panelTituloVencido.Size = new Size(245, 100);
            panelTituloVencido.TabIndex = 1;
            // 
            // lblValorVencido
            // 
            lblValorVencido.AutoSize = true;
            lblValorVencido.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblValorVencido.ForeColor = Color.FromArgb(153, 27, 27);
            lblValorVencido.Location = new Point(10, 40);
            lblValorVencido.Name = "lblValorVencido";
            lblValorVencido.Size = new Size(115, 38);
            lblValorVencido.TabIndex = 2;
            lblValorVencido.Text = "R$ 0,00";
            // 
            // lblTituloVencido
            // 
            lblTituloVencido.AutoSize = true;
            lblTituloVencido.Location = new Point(10, 10);
            lblTituloVencido.Name = "lblTituloVencido";
            lblTituloVencido.Size = new Size(99, 20);
            lblTituloVencido.TabIndex = 1;
            lblTituloVencido.Text = "Total Vencido";
            // 
            // panelCardPago
            // 
            panelCardPago.BackColor = Color.FromArgb(209, 250, 229);
            panelCardPago.BorderStyle = BorderStyle.FixedSingle;
            panelCardPago.Controls.Add(lblValorPago);
            panelCardPago.Controls.Add(lblTituloPago);
            panelCardPago.Dock = DockStyle.Fill;
            panelCardPago.Location = new Point(540, 10);
            panelCardPago.Margin = new Padding(10);
            panelCardPago.Name = "panelCardPago";
            panelCardPago.Size = new Size(245, 100);
            panelCardPago.TabIndex = 2;
            // 
            // lblValorPago
            // 
            lblValorPago.AutoSize = true;
            lblValorPago.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblValorPago.ForeColor = Color.FromArgb(6, 95, 70);
            lblValorPago.Location = new Point(10, 40);
            lblValorPago.Name = "lblValorPago";
            lblValorPago.Size = new Size(115, 38);
            lblValorPago.TabIndex = 3;
            lblValorPago.Text = "R$ 0,00";
            // 
            // lblTituloPago
            // 
            lblTituloPago.AutoSize = true;
            lblTituloPago.Location = new Point(10, 10);
            lblTituloPago.Name = "lblTituloPago";
            lblTituloPago.Size = new Size(79, 20);
            lblTituloPago.TabIndex = 4;
            lblTituloPago.Text = "Total Pago";
            // 
            // panelCardBudget
            // 
            panelCardBudget.BackColor = Color.FromArgb(219, 234, 254);
            panelCardBudget.BorderStyle = BorderStyle.FixedSingle;
            panelCardBudget.Controls.Add(lblValorBudget);
            panelCardBudget.Controls.Add(lblTituloBudget);
            panelCardBudget.Dock = DockStyle.Fill;
            panelCardBudget.Location = new Point(805, 10);
            panelCardBudget.Margin = new Padding(10);
            panelCardBudget.Name = "panelCardBudget";
            panelCardBudget.Size = new Size(245, 100);
            panelCardBudget.TabIndex = 3;
            // 
            // lblValorBudget
            // 
            lblValorBudget.AutoSize = true;
            lblValorBudget.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblValorBudget.ForeColor = Color.FromArgb(30, 64, 175);
            lblValorBudget.Location = new Point(10, 40);
            lblValorBudget.Name = "lblValorBudget";
            lblValorBudget.Size = new Size(115, 38);
            lblValorBudget.TabIndex = 5;
            lblValorBudget.Text = "R$ 0,00";
            // 
            // lblTituloBudget
            // 
            lblTituloBudget.AutoSize = true;
            lblTituloBudget.Location = new Point(10, 10);
            lblTituloBudget.Name = "lblTituloBudget";
            lblTituloBudget.Size = new Size(145, 20);
            lblTituloBudget.TabIndex = 3;
            lblTituloBudget.Text = "Total Budget Mensal";
            // 
            // panelCardSaldo
            // 
            panelCardSaldo.BackColor = Color.FromArgb(224, 231, 255);
            panelCardSaldo.BorderStyle = BorderStyle.FixedSingle;
            panelCardSaldo.Controls.Add(lblValorSaldo);
            panelCardSaldo.Controls.Add(lblTituloSaldo);
            panelCardSaldo.Dock = DockStyle.Fill;
            panelCardSaldo.Location = new Point(1070, 10);
            panelCardSaldo.Margin = new Padding(10);
            panelCardSaldo.Name = "panelCardSaldo";
            panelCardSaldo.Size = new Size(248, 100);
            panelCardSaldo.TabIndex = 4;
            // 
            // lblValorSaldo
            // 
            lblValorSaldo.AutoSize = true;
            lblValorSaldo.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblValorSaldo.ForeColor = Color.Red;
            lblValorSaldo.Location = new Point(10, 40);
            lblValorSaldo.Name = "lblValorSaldo";
            lblValorSaldo.Size = new Size(115, 38);
            lblValorSaldo.TabIndex = 6;
            lblValorSaldo.Text = "R$ 0,00";
            // 
            // lblTituloSaldo
            // 
            lblTituloSaldo.AutoSize = true;
            lblTituloSaldo.Location = new Point(10, 10);
            lblTituloSaldo.Name = "lblTituloSaldo";
            lblTituloSaldo.Size = new Size(98, 20);
            lblTituloSaldo.TabIndex = 4;
            lblTituloSaldo.Text = "Saldo Mensal";
            // 
            // lblTituloVencimentos
            // 
            lblTituloVencimentos.AutoSize = true;
            lblTituloVencimentos.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTituloVencimentos.Location = new Point(20, 130);
            lblTituloVencimentos.Name = "lblTituloVencimentos";
            lblTituloVencimentos.Size = new Size(315, 28);
            lblTituloVencimentos.TabIndex = 2;
            lblTituloVencimentos.Text = "Próximos Vencimentos (30 dias)";
            // 
            // dgvVencimentos
            // 
            dgvVencimentos.AllowUserToAddRows = false;
            dgvVencimentos.AllowUserToDeleteRows = false;
            dgvVencimentos.AllowUserToResizeColumns = false;
            dgvVencimentos.AllowUserToResizeRows = false;
            dgvVencimentos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVencimentos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVencimentos.Location = new Point(8, 64);
            dgvVencimentos.Name = "dgvVencimentos";
            dgvVencimentos.ReadOnly = true;
            dgvVencimentos.RowHeadersWidth = 51;
            dgvVencimentos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVencimentos.Size = new Size(1320, 220);
            dgvVencimentos.TabIndex = 1;
            // 
            // mainLayout
            // 
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.Controls.Add(tableLayoutCards, 0, 0);
            mainLayout.Controls.Add(panelGrafico, 0, 1);
            mainLayout.Controls.Add(panelTabela, 0, 2);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 0);
            mainLayout.Name = "mainLayout";
            mainLayout.Padding = new Padding(20);
            mainLayout.RowCount = 3;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 140F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            mainLayout.Size = new Size(1374, 737);
            mainLayout.TabIndex = 1;
            // 
            // panelGrafico
            // 
            panelGrafico.BackColor = Color.White;
            panelGrafico.Controls.Add(lblGraficoTitulo);
            panelGrafico.Controls.Add(lblGraficoSub);
            panelGrafico.Controls.Add(picGrafico);
            panelGrafico.Dock = DockStyle.Fill;
            panelGrafico.Location = new Point(20, 160);
            panelGrafico.Margin = new Padding(0, 0, 0, 16);
            panelGrafico.Name = "panelGrafico";
            panelGrafico.Padding = new Padding(16);
            panelGrafico.Size = new Size(1334, 262);
            panelGrafico.TabIndex = 1;
            // 
            // lblGraficoTitulo
            // 
            lblGraficoTitulo.AutoSize = true;
            lblGraficoTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblGraficoTitulo.Location = new Point(8, 8);
            lblGraficoTitulo.Name = "lblGraficoTitulo";
            lblGraficoTitulo.Size = new Size(295, 28);
            lblGraficoTitulo.TabIndex = 0;
            lblGraficoTitulo.Text = "Visão Geral - Últimos 6 Meses";
            // 
            // lblGraficoSub
            // 
            lblGraficoSub.AutoSize = true;
            lblGraficoSub.Font = new Font("Segoe UI", 9F);
            lblGraficoSub.ForeColor = Color.Gray;
            lblGraficoSub.Location = new Point(8, 36);
            lblGraficoSub.Name = "lblGraficoSub";
            lblGraficoSub.Size = new Size(282, 20);
            lblGraficoSub.TabIndex = 1;
            lblGraficoSub.Text = "Comparação entre boletos e notas fiscais";
            // 
            // picGrafico
            // 
            picGrafico.BackColor = Color.WhiteSmoke;
            picGrafico.Location = new Point(8, 64);
            picGrafico.Name = "picGrafico";
            picGrafico.Size = new Size(1320, 220);
            picGrafico.TabIndex = 2;
            picGrafico.TabStop = false;
            // 
            // panelTabela
            // 
            panelTabela.BackColor = Color.White;
            panelTabela.Controls.Add(lblTabelaTitulo);
            panelTabela.Controls.Add(lblTabelaSub);
            panelTabela.Controls.Add(dgvVencimentos);
            panelTabela.Dock = DockStyle.Fill;
            panelTabela.Location = new Point(23, 441);
            panelTabela.Name = "panelTabela";
            panelTabela.Padding = new Padding(16);
            panelTabela.Size = new Size(1328, 273);
            panelTabela.TabIndex = 2;
            // 
            // lblTabelaTitulo
            // 
            lblTabelaTitulo.AutoSize = true;
            lblTabelaTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTabelaTitulo.Location = new Point(8, 8);
            lblTabelaTitulo.Name = "lblTabelaTitulo";
            lblTabelaTitulo.Size = new Size(216, 25);
            lblTabelaTitulo.TabIndex = 0;
            lblTabelaTitulo.Text = "Próximos Vencimentos";
            // 
            // lblTabelaSub
            // 
            lblTabelaSub.AutoSize = true;
            lblTabelaSub.Font = new Font("Segoe UI", 9F);
            lblTabelaSub.ForeColor = Color.Gray;
            lblTabelaSub.Location = new Point(8, 34);
            lblTabelaSub.Name = "lblTabelaSub";
            lblTabelaSub.Size = new Size(298, 20);
            lblTabelaSub.TabIndex = 1;
            lblTabelaSub.Text = "Itens com vencimento nos próximos 30 dias";
            // 
            // DashboardControl
            // 
            Controls.Add(mainLayout);
            Name = "DashboardControl";
            Size = new Size(1374, 737);
            tableLayoutCards.ResumeLayout(false);
            panelCardPendente.ResumeLayout(false);
            panelCardPendente.PerformLayout();
            panelTituloVencido.ResumeLayout(false);
            panelTituloVencido.PerformLayout();
            panelCardPago.ResumeLayout(false);
            panelCardPago.PerformLayout();
            panelCardBudget.ResumeLayout(false);
            panelCardBudget.PerformLayout();
            panelCardSaldo.ResumeLayout(false);
            panelCardSaldo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVencimentos).EndInit();
            mainLayout.ResumeLayout(false);
            panelGrafico.ResumeLayout(false);
            panelGrafico.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picGrafico).EndInit();
            panelTabela.ResumeLayout(false);
            panelTabela.PerformLayout();
            ResumeLayout(false);
        }

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
        private Label lblTituloVencimentos;
        private DataGridView dgvVencimentos;
        private DataGridViewTextBoxColumn Tipo;
        private DataGridViewTextBoxColumn Número;
        private DataGridViewTextBoxColumn Fornecedor;
        private DataGridViewTextBoxColumn Valor;
        private DataGridViewTextBoxColumn Vencimento;
        private DataGridViewTextBoxColumn Status;

        // new fields for layout controls
        private TableLayoutPanel mainLayout;
        private Panel panelGrafico;
        private Label lblGraficoTitulo;
        private Label lblGraficoSub;
        private PictureBox picGrafico;
        private Panel panelTabela;
        private Label lblTabelaTitulo;
        private Label lblTabelaSub;

        #endregion
    }
}
