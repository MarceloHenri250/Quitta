namespace Quitta.UserControls
{
    partial class BudgetAnualControl
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            tableLayoutCardsBudget = new TableLayoutPanel();
            panelCardBudgetTotal = new Panel();
            lblValorBudgetTotal = new Label();
            lblTituloBudgetTotal = new Label();
            panelCardPagoAnual = new Panel();
            lblValorPagoAnual = new Label();
            lblTituloPagoAnual = new Label();
            panelCardSaldoAnual = new Panel();
            lblValorSaldoAnual = new Label();
            lblTituloSaldoAnual = new Label();
            panelBudget = new Panel();
            lblTituloBudgetAnual = new Label();
            lblDescricaoBudget = new Label();
            dgvBudgetMensal = new DataGridView();
            Mês = new DataGridViewTextBoxColumn();
            Budget = new DataGridViewTextBoxColumn();
            Pago = new DataGridViewTextBoxColumn();
            Pendente = new DataGridViewTextBoxColumn();
            Saldo = new DataGridViewTextBoxColumn();
            Situacao = new DataGridViewTextBoxColumn();
            Acoes = new DataGridViewButtonColumn();
            SuspendLayout();
            // 
            // tableLayoutCardsBudget
            // 
            tableLayoutCardsBudget.ColumnCount = 3;
            tableLayoutCardsBudget.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tableLayoutCardsBudget.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tableLayoutCardsBudget.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tableLayoutCardsBudget.Controls.Add(panelCardBudgetTotal, 0, 0);
            tableLayoutCardsBudget.Controls.Add(panelCardPagoAnual, 1, 0);
            tableLayoutCardsBudget.Controls.Add(panelCardSaldoAnual, 2, 0);
            tableLayoutCardsBudget.Dock = DockStyle.Top;
            tableLayoutCardsBudget.Location = new Point(3, 81);
            tableLayoutCardsBudget.Name = "tableLayoutCardsBudget";
            tableLayoutCardsBudget.RowCount = 1;
            tableLayoutCardsBudget.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutCardsBudget.Size = new Size(1328, 125);
            tableLayoutCardsBudget.TabIndex = 0;
            tableLayoutCardsBudget.Padding = new Padding(8);
            // 
            // panelCardBudgetTotal
            // 
            panelCardBudgetTotal.Controls.Add(lblValorBudgetTotal);
            panelCardBudgetTotal.Controls.Add(lblTituloBudgetTotal);
            panelCardBudgetTotal.Dock = DockStyle.Fill;
            panelCardBudgetTotal.Location = new Point(3, 3);
            panelCardBudgetTotal.Name = "panelCardBudgetTotal";
            panelCardBudgetTotal.Size = new Size(438, 119);
            panelCardBudgetTotal.TabIndex = 0;
            panelCardBudgetTotal.BorderStyle = BorderStyle.FixedSingle;
            panelCardBudgetTotal.Padding = new Padding(12);
            // 
            // lblValorBudgetTotal
            // 
            lblValorBudgetTotal.AutoSize = true;
            lblValorBudgetTotal.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            lblValorBudgetTotal.Location = new Point(16, 48);
            lblValorBudgetTotal.Name = "lblValorBudgetTotal";
            lblValorBudgetTotal.Size = new Size(115, 38);
            lblValorBudgetTotal.TabIndex = 1;
            lblValorBudgetTotal.Text = "R$ 0,00";
            // 
            // lblTituloBudgetTotal
            // 
            lblTituloBudgetTotal.AutoSize = true;
            lblTituloBudgetTotal.Location = new Point(16, 16);
            lblTituloBudgetTotal.Name = "lblTituloBudgetTotal";
            lblTituloBudgetTotal.Size = new Size(106, 23);
            lblTituloBudgetTotal.TabIndex = 0;
            lblTituloBudgetTotal.Text = "Budget Total";
            // 
            // panelCardPagoAnual
            // 
            panelCardPagoAnual.Controls.Add(lblValorPagoAnual);
            panelCardPagoAnual.Controls.Add(lblTituloPagoAnual);
            panelCardPagoAnual.Dock = DockStyle.Fill;
            panelCardPagoAnual.Location = new Point(447, 3);
            panelCardPagoAnual.Name = "panelCardPagoAnual";
            panelCardPagoAnual.Size = new Size(438, 119);
            panelCardPagoAnual.TabIndex = 1;
            panelCardPagoAnual.BorderStyle = BorderStyle.FixedSingle;
            panelCardPagoAnual.Padding = new Padding(12);
            // 
            // lblValorPagoAnual
            // 
            lblValorPagoAnual.AutoSize = true;
            lblValorPagoAnual.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            lblValorPagoAnual.Location = new Point(16, 48);
            lblValorPagoAnual.Name = "lblValorPagoAnual";
            lblValorPagoAnual.Size = new Size(115, 38);
            lblValorPagoAnual.TabIndex = 1;
            lblValorPagoAnual.Text = "R$ 0,00";
            // 
            // lblTituloPagoAnual
            // 
            lblTituloPagoAnual.AutoSize = true;
            lblTituloPagoAnual.Location = new Point(16, 16);
            lblTituloPagoAnual.Name = "lblTituloPagoAnual";
            lblTituloPagoAnual.Size = new Size(174, 23);
            lblTituloPagoAnual.TabIndex = 0;
            lblTituloPagoAnual.Text = "Total Pago (12 meses)";
            // 
            // panelCardSaldoAnual
            // 
            panelCardSaldoAnual.Controls.Add(lblValorSaldoAnual);
            panelCardSaldoAnual.Controls.Add(lblTituloSaldoAnual);
            panelCardSaldoAnual.Dock = DockStyle.Fill;
            panelCardSaldoAnual.Location = new Point(891, 3);
            panelCardSaldoAnual.Name = "panelCardSaldoAnual";
            panelCardSaldoAnual.Size = new Size(434, 119);
            panelCardSaldoAnual.TabIndex = 2;
            panelCardSaldoAnual.BorderStyle = BorderStyle.FixedSingle;
            panelCardSaldoAnual.Padding = new Padding(12);
            // 
            // lblValorSaldoAnual
            // 
            lblValorSaldoAnual.AutoSize = true;
            lblValorSaldoAnual.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            lblValorSaldoAnual.Location = new Point(16, 48);
            lblValorSaldoAnual.Name = "lblValorSaldoAnual";
            lblValorSaldoAnual.Size = new Size(115, 38);
            lblValorSaldoAnual.TabIndex = 1;
            lblValorSaldoAnual.Text = "R$ 0,00";
            // 
            // lblTituloSaldoAnual
            // 
            lblTituloSaldoAnual.AutoSize = true;
            lblTituloSaldoAnual.Location = new Point(16, 16);
            lblTituloSaldoAnual.Name = "lblTituloSaldoAnual";
            lblTituloSaldoAnual.Size = new Size(135, 23);
            lblTituloSaldoAnual.TabIndex = 0;
            lblTituloSaldoAnual.Text = "Saldo Disponível";
            // 
            // panelBudget
            // 
            panelBudget.Controls.Add(lblTituloBudgetAnual);
            panelBudget.Controls.Add(lblDescricaoBudget);
            panelBudget.Dock = DockStyle.Top;
            panelBudget.Location = new Point(3, 3);
            panelBudget.Name = "panelBudget";
            panelBudget.Size = new Size(1328, 78);
            panelBudget.TabIndex = 2;
            // 
            // lblTituloBudgetAnual
            // 
            lblTituloBudgetAnual.AutoSize = true;
            lblTituloBudgetAnual.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            lblTituloBudgetAnual.Location = new Point(13, 12);
            lblTituloBudgetAnual.Name = "lblTituloBudgetAnual";
            lblTituloBudgetAnual.Size = new Size(197, 38);
            lblTituloBudgetAnual.TabIndex = 0;
            lblTituloBudgetAnual.Text = "Budget Anual";
            // 
            // lblDescricaoBudget
            // 
            lblDescricaoBudget.AutoSize = true;
            lblDescricaoBudget.Font = new Font("Segoe UI", 10F);
            lblDescricaoBudget.ForeColor = Color.Gray;
            lblDescricaoBudget.Location = new Point(15, 50);
            lblDescricaoBudget.Name = "lblDescricaoBudget";
            lblDescricaoBudget.Size = new Size(516, 23);
            lblDescricaoBudget.TabIndex = 1;
            lblDescricaoBudget.Text = "Gerencie o orçamento disponível ao longo dos próximos 12 meses";
            // 
            // dgvBudgetMensal
            // 
            dgvBudgetMensal.AllowUserToAddRows = false;
            dgvBudgetMensal.AllowUserToDeleteRows = false;
            dgvBudgetMensal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // designer-driven DataGridView visual settings
            dgvBudgetMensal.AllowUserToResizeColumns = false;
            dgvBudgetMensal.AllowUserToResizeRows = false;
            dgvBudgetMensal.RowHeadersVisible = false;
            dgvBudgetMensal.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBudgetMensal.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgvBudgetMensal.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvBudgetMensal.EnableHeadersVisualStyles = false;
            dgvBudgetMensal.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvBudgetMensal.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvBudgetMensal.GridColor = Color.FromArgb(230, 230, 230);
            dgvBudgetMensal.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBudgetMensal.Dock = DockStyle.Fill;
            dgvBudgetMensal.Name = "dgvBudgetMensal";
            dgvBudgetMensal.RowHeadersWidth = 51;
            dgvBudgetMensal.TabIndex = 3;
            dgvBudgetMensal.Columns.AddRange(new DataGridViewColumn[] { Mês, Budget, Pago, Pendente, Saldo, Situacao, Acoes });
            // 
            // Mês
            // 
            Mês.DataPropertyName = "MesAno";
            Mês.HeaderText = "Mês";
            Mês.MinimumWidth = 6;
            Mês.Name = "Mês";
            Mês.Visible = true;
            Mês.FillWeight = 12F;
            // 
            // Budget
            // 
            Budget.DataPropertyName = "BudgetPlanejado";
            dataGridViewCellStyle1.Format = "c2";
            Budget.DefaultCellStyle = dataGridViewCellStyle1;
            Budget.HeaderText = "Budget Planejado";
            Budget.MinimumWidth = 6;
            Budget.Name = "Budget";
            Budget.FillWeight = 30F;
            // 
            // Pago
            // 
            Pago.DataPropertyName = "TotalPago";
            dataGridViewCellStyle2.Format = "c2";
            Pago.DefaultCellStyle = dataGridViewCellStyle2;
            Pago.HeaderText = "Total Pago";
            Pago.MinimumWidth = 6;
            Pago.Name = "Pago";
            Pago.FillWeight = 15F;
            // 
            // Pendente
            // 
            Pendente.DataPropertyName = "Pendente";
            dataGridViewCellStyle3.Format = "c2";
            Pendente.DefaultCellStyle = dataGridViewCellStyle3;
            Pendente.HeaderText = "Pendente";
            Pendente.MinimumWidth = 6;
            Pendente.Name = "Pendente";
            Pendente.FillWeight = 15F;
            // 
            // Saldo
            // 
            Saldo.DataPropertyName = "Saldo";
            dataGridViewCellStyle4.Format = "c2";
            Saldo.DefaultCellStyle = dataGridViewCellStyle4;
            Saldo.HeaderText = "Saldo";
            Saldo.MinimumWidth = 6;
            Saldo.Name = "Saldo";
            Saldo.FillWeight = 15F;
            // 
            // Situacao
            // 
            Situacao.DataPropertyName = "StatusDisplay";
            Situacao.HeaderText = "Status";
            Situacao.MinimumWidth = 6;
            Situacao.Name = "Situacao";
            Situacao.FillWeight = 8F;
            // 
            // Acoes
            // 
            ((DataGridViewButtonColumn)Acoes).HeaderText = "Ações";
            ((DataGridViewButtonColumn)Acoes).MinimumWidth = 6;
            Acoes.Name = "Acoes";
            ((DataGridViewButtonColumn)Acoes).Text = "✎";
            ((DataGridViewButtonColumn)Acoes).UseColumnTextForButtonValue = true;
            ((DataGridViewButtonColumn)Acoes).Width = 60;
            ((DataGridViewButtonColumn)Acoes).FillWeight = 5F;
            // 
            // BudgetAnualControl
            // 
            Controls.Add(dgvBudgetMensal);
            Controls.Add(tableLayoutCardsBudget);
            Controls.Add(panelBudget);
            Name = "BudgetAnualControl";
            Size = new Size(1328, 683);
            ResumeLayout(false);
        }

        private TableLayoutPanel tableLayoutCardsBudget;
        private Panel panelCardBudgetTotal;
        private Label lblValorBudgetTotal;
        private Label lblTituloBudgetTotal;
        private Panel panelCardPagoAnual;
        private Label lblValorPagoAnual;
        private Label lblTituloPagoAnual;
        private Panel panelCardSaldoAnual;
        private Label lblValorSaldoAnual;
        private Label lblTituloSaldoAnual;
        private Panel panelBudget;
        private Label lblTituloBudgetAnual;
        private Label lblDescricaoBudget;
        private DataGridView dgvBudgetMensal;
        private DataGridViewTextBoxColumn Mês;
        private DataGridViewTextBoxColumn Budget;
        private DataGridViewTextBoxColumn Pago;
        private DataGridViewTextBoxColumn Pendente;
        private DataGridViewTextBoxColumn Saldo;
        private DataGridViewTextBoxColumn Situacao;
        private DataGridViewButtonColumn Acoes;

        #endregion
    }
}
