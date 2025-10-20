using System;
using System.Globalization;
using System.Windows.Forms;

namespace Quitta.Forms
{
    public partial class EditBudget : Form
    {
        public decimal BudgetValue { get; private set; }
        public string MonthDisplay { get; private set; }

        public EditBudget()
        {
            InitializeComponent();
        }

        public void SetMonth(string monthDisplay, decimal currentBudget)
        {
            MonthDisplay = monthDisplay;
            lblMonth.Text = monthDisplay;
            txtBudget.Text = currentBudget.ToString("F2", CultureInfo.CurrentCulture);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtBudget.Text,
                NumberStyles.Number | NumberStyles.AllowCurrencySymbol,
                CultureInfo.CurrentCulture,
                out decimal val))
            {
                if (val < 0)
                {
                    MessageBox.Show("O budget não pode ser negativo.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                BudgetValue = val;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Valor inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
