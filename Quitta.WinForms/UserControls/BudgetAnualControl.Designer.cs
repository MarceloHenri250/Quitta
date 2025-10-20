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
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
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
            tableLayoutCardsBudget.SuspendLayout();
            panelCardBudgetTotal.SuspendLayout();
            panelCardPagoAnual.SuspendLayout();
            panelCardSaldoAnual.SuspendLayout();
            panelBudget.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBudgetMensal).BeginInit();
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
            tableLayoutCardsBudget.Location = new Point(0, 78);
            tableLayoutCardsBudget.Name = "tableLayoutCardsBudget";
            tableLayoutCardsBudget.Padding = new Padding(8);
            tableLayoutCardsBudget.RowCount = 1;
            tableLayoutCardsBudget.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutCardsBudget.Size = new Size(1328, 125);
            tableLayoutCardsBudget.TabIndex = 0;
            // 
            // panelCardBudgetTotal
            // 
            panelCardBudgetTotal.BorderStyle = BorderStyle.FixedSingle;
            panelCardBudgetTotal.Controls.Add(lblValorBudgetTotal);
            panelCardBudgetTotal.Controls.Add(lblTituloBudgetTotal);
            panelCardBudgetTotal.Dock = DockStyle.Fill;
            panelCardBudgetTotal.Location = new Point(11, 11);
            panelCardBudgetTotal.Name = "panelCardBudgetTotal";
            panelCardBudgetTotal.Padding = new Padding(12);
            panelCardBudgetTotal.Size = new Size(431, 103);
            panelCardBudgetTotal.TabIndex = 0;
            // 
            // lblValorBudgetTotal
            // 
            lblValorBudgetTotal.AutoSize = true;
            lblValorBudgetTotal.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            lblValorBudgetTotal.Location = new Point(28, 60);
            lblValorBudgetTotal.Name = "lblValorBudgetTotal";
            lblValorBudgetTotal.Size = new Size(115, 38);
            lblValorBudgetTotal.TabIndex = 1;
            lblValorBudgetTotal.Text = "R$ 0,00";
            // 
            // lblTituloBudgetTotal
            // 
            lblTituloBudgetTotal.AutoSize = true;
            lblTituloBudgetTotal.Location = new Point(28, 28);
            lblTituloBudgetTotal.Name = "lblTituloBudgetTotal";
            lblTituloBudgetTotal.Size = new Size(94, 20);
            lblTituloBudgetTotal.TabIndex = 0;
            lblTituloBudgetTotal.Text = "Budget Total";
            // 
            // panelCardPagoAnual
            // 
            panelCardPagoAnual.BorderStyle = BorderStyle.FixedSingle;
            panelCardPagoAnual.Controls.Add(lblValorPagoAnual);
            panelCardPagoAnual.Controls.Add(lblTituloPagoAnual);
            panelCardPagoAnual.Dock = DockStyle.Fill;
            panelCardPagoAnual.Location = new Point(448, 11);
            panelCardPagoAnual.Name = "panelCardPagoAnual";
            panelCardPagoAnual.Padding = new Padding(12);
            panelCardPagoAnual.Size = new Size(431, 103);
            panelCardPagoAnual.TabIndex = 1;
            // 
            // lblValorPagoAnual
            // 
            lblValorPagoAnual.AutoSize = true;
            lblValorPagoAnual.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            lblValorPagoAnual.Location = new Point(28, 60);
            lblValorPagoAnual.Name = "lblValorPagoAnual";
            lblValorPagoAnual.Size = new Size(115, 38);
            lblValorPagoAnual.TabIndex = 1;
            lblValorPagoAnual.Text = "R$ 0,00";
            // 
            // lblTituloPagoAnual
            // 
            lblTituloPagoAnual.AutoSize = true;
            lblTituloPagoAnual.Location = new Point(28, 28);
            lblTituloPagoAnual.Name = "lblTituloPagoAnual";
            lblTituloPagoAnual.Size = new Size(154, 20);
            lblTituloPagoAnual.TabIndex = 0;
            lblTituloPagoAnual.Text = "Total Pago (12 meses)";
            // 
            // panelCardSaldoAnual
            // 
            panelCardSaldoAnual.BorderStyle = BorderStyle.FixedSingle;
            panelCardSaldoAnual.Controls.Add(lblValorSaldoAnual);
            panelCardSaldoAnual.Controls.Add(lblTituloSaldoAnual);
            panelCardSaldoAnual.Dock = DockStyle.Fill;
            panelCardSaldoAnual.Location = new Point(885, 11);
            panelCardSaldoAnual.Name = "panelCardSaldoAnual";
            panelCardSaldoAnual.Padding = new Padding(12);
            panelCardSaldoAnual.Size = new Size(432, 103);
            panelCardSaldoAnual.TabIndex = 2;
            // 
            // lblValorSaldoAnual
            // 
            lblValorSaldoAnual.AutoSize = true;
            lblValorSaldoAnual.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            lblValorSaldoAnual.Location = new Point(28, 60);
            lblValorSaldoAnual.Name = "lblValorSaldoAnual";
            lblValorSaldoAnual.Size = new Size(115, 38);
            lblValorSaldoAnual.TabIndex = 1;
            lblValorSaldoAnual.Text = "R$ 0,00";
            // 
            // lblTituloSaldoAnual
            // 
            lblTituloSaldoAnual.AutoSize = true;
            lblTituloSaldoAnual.Location = new Point(28, 28);
            lblTituloSaldoAnual.Name = "lblTituloSaldoAnual";
            lblTituloSaldoAnual.Size = new Size(121, 20);
            lblTituloSaldoAnual.TabIndex = 0;
            lblTituloSaldoAnual.Text = "Saldo Disponível";
            // 
            // panelBudget
            // 
            panelBudget.Controls.Add(lblTituloBudgetAnual);
            panelBudget.Controls.Add(lblDescricaoBudget);
            panelBudget.Dock = DockStyle.Top;
            panelBudget.Location = new Point(0, 0);
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
            lblDescricaoBudget.Size = new Size(397, 23);
            lblDescricaoBudget.TabIndex = 1;
            lblDescricaoBudget.Text = "Gerencie o orçamento disponível ao longo do ano.";
            // 
            // dgvBudgetMensal
            // 
            dgvBudgetMensal.AllowUserToAddRows = false;
            dgvBudgetMensal.AllowUserToDeleteRows = false;
            dgvBudgetMensal.AllowUserToResizeColumns = false;
            dgvBudgetMensal.AllowUserToResizeRows = false;
            dgvBudgetMensal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvBudgetMensal.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvBudgetMensal.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBudgetMensal.Columns.AddRange(new DataGridViewColumn[] { Mês, Budget, Pago, Pendente, Saldo, Situacao, Acoes });
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Window;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dataGridViewCellStyle6.SelectionForeColor = Color.Black;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvBudgetMensal.DefaultCellStyle = dataGridViewCellStyle6;
            dgvBudgetMensal.Dock = DockStyle.Fill;
            dgvBudgetMensal.EnableHeadersVisualStyles = false;
            dgvBudgetMensal.GridColor = Color.FromArgb(230, 230, 230);
            // Prevent direct cell editing - use the Edit dialog instead
            dgvBudgetMensal.ReadOnly = true;
            dgvBudgetMensal.Location = new Point(0, 203);
            dgvBudgetMensal.Name = "dgvBudgetMensal";
            dgvBudgetMensal.CellClick += new DataGridViewCellEventHandler(this.DgvBudgetMensal_CellClick);
            dgvBudgetMensal.CellFormatting += new DataGridViewCellFormattingEventHandler(this.DgvBudgetMensal_CellFormatting);
            dgvBudgetMensal.RowHeadersVisible = false;
            dgvBudgetMensal.RowHeadersWidth = 51;
            dgvBudgetMensal.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBudgetMensal.Size = new Size(1328, 480);
            dgvBudgetMensal.TabIndex = 3;
            // 
            // Mês
            // 
            Mês.DataPropertyName = "MesAno";
            Mês.FillWeight = 12F;
            Mês.HeaderText = "Mês";
            Mês.MinimumWidth = 6;
            Mês.Name = "Mês";
            // 
            // Budget
            // 
            Budget.DataPropertyName = "BudgetPlanejado";
            dataGridViewCellStyle2.Format = "c2";
            Budget.DefaultCellStyle = dataGridViewCellStyle2;
            Budget.FillWeight = 30F;
            Budget.HeaderText = "Budget Planejado";
            Budget.MinimumWidth = 6;
            Budget.Name = "Budget";
            // 
            // Pago
            // 
            Pago.DataPropertyName = "TotalPago";
            dataGridViewCellStyle3.Format = "c2";
            Pago.DefaultCellStyle = dataGridViewCellStyle3;
            Pago.FillWeight = 15F;
            Pago.HeaderText = "Total Pago";
            Pago.MinimumWidth = 6;
            Pago.Name = "Pago";
            // 
            // Pendente
            // 
            Pendente.DataPropertyName = "Pendente";
            dataGridViewCellStyle4.Format = "c2";
            Pendente.DefaultCellStyle = dataGridViewCellStyle4;
            Pendente.FillWeight = 15F;
            Pendente.HeaderText = "Pendente";
            Pendente.MinimumWidth = 6;
            Pendente.Name = "Pendente";
            // 
            // Saldo
            // 
            Saldo.DataPropertyName = "Saldo";
            dataGridViewCellStyle5.Format = "c2";
            Saldo.DefaultCellStyle = dataGridViewCellStyle5;
            Saldo.FillWeight = 15F;
            Saldo.HeaderText = "Saldo";
            Saldo.MinimumWidth = 6;
            Saldo.Name = "Saldo";
            // 
            // Situacao
            // 
            Situacao.DataPropertyName = "StatusDisplay";
            Situacao.FillWeight = 8F;
            Situacao.HeaderText = "Status";
            Situacao.MinimumWidth = 6;
            Situacao.Name = "Situacao";
            // 
            // Acoes
            // 
            Acoes.FillWeight = 5F;
            Acoes.HeaderText = "Ações";
            Acoes.MinimumWidth = 6;
            Acoes.Name = "Acoes";
            Acoes.Text = "✎";
            Acoes.UseColumnTextForButtonValue = true;
            // 
            // BudgetAnualControl
            // 
            Controls.Add(dgvBudgetMensal);
            Controls.Add(tableLayoutCardsBudget);
            Controls.Add(panelBudget);
            Name = "BudgetAnualControl";
            Size = new Size(1328, 683);
            tableLayoutCardsBudget.ResumeLayout(false);
            panelCardBudgetTotal.ResumeLayout(false);
            panelCardBudgetTotal.PerformLayout();
            panelCardPagoAnual.ResumeLayout(false);
            panelCardPagoAnual.PerformLayout();
            panelCardSaldoAnual.ResumeLayout(false);
            panelCardSaldoAnual.PerformLayout();
            panelBudget.ResumeLayout(false);
            panelBudget.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBudgetMensal).EndInit();
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
