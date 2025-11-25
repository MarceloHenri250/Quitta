using System;
using System.Globalization;
using System.Windows.Forms;

namespace Quitta.Forms
{
    public partial class EditBudget : Form
    {
        #region Propriedades públicas
        // Valor do budget definido pelo usuário
        public decimal BudgetValue { get; private set; }
        // Texto do mês exibido no diálogo (ex: "Janeiro de 2024")
        public string MonthDisplay { get; private set; }
        #endregion

        #region Construtor
        public EditBudget()
        {
            InitializeComponent();
        }
        #endregion

        #region Inicialização de dados
        // Define o mês exibido e o valor atual do budget para edição
        public void SetMonth(string monthDisplay, decimal currentBudget)
        {
            MonthDisplay = monthDisplay;
            lblMonth.Text = monthDisplay;
            txtBudget.Text = currentBudget.ToString("F2", CultureInfo.CurrentCulture);
        }
        #endregion

        #region Handlers de UI
        // Handler do botão OK: valida e aplica o valor informado
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
        #endregion
    }
}
