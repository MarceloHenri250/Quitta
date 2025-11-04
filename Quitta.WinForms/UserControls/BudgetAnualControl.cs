using Quitta.Models;
using Quitta.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace Quitta.UserControls
{
    public partial class BudgetAnualControl : UserControl
    {
        private List<Item> items = new();
        private List<MonthlyBudget> budgets = new();

        // raised when budgets are modified so parent can persist and refresh other views
        public event Action BudgetsChanged;

        public BudgetAnualControl()
        {
            InitializeComponent();
            // Visual configuration moved to designer.
        }

        public void SetData(List<Item> items, List<MonthlyBudget> budgets)
        {
            this.items = items ?? new List<Item>();
            this.budgets = budgets ?? new List<MonthlyBudget>();
            PopulateBudget();
        }

        // expose budgets for owners to persist (returns the actual list instance)
        public List<MonthlyBudget> GetBudgets()
        {
            return budgets;
        }

        private void PopulateBudget()
        {
            var budgetRows = new List<BudgetMonthRow>();

            // Determine range of months to display based on settings
            string mode = Properties.Settings.Default.BudgetMode ?? "CurrentYear";
            DateTime startMonth;
            DateTime endMonth;
            var now = DateTime.Now;

            switch (mode)
            {
                case "CurrentPlusNext":
                    // show current year and next year
                    startMonth = new DateTime(now.Year, 1, 1);
                    endMonth = new DateTime(now.Year + 1, 12, 1);
                    break;
                case "Last2":
                    // last two full years (previous and current)
                    startMonth = new DateTime(now.Year - 1, 1, 1);
                    endMonth = new DateTime(now.Year, 12, 1);
                    break;
                case "Last3":
                    // last three full years
                    startMonth = new DateTime(now.Year - 2, 1, 1);
                    endMonth = new DateTime(now.Year, 12, 1);
                    break;
                case "Custom":
                    int sy = Properties.Settings.Default.BudgetCustomStartYear;
                    int ey = Properties.Settings.Default.BudgetCustomEndYear;
                    if (sy <= 0) sy = now.Year;
                    if (ey <= 0) ey = now.Year;
                    if (sy > ey) // swap if inverted
                    {
                        var t = sy; sy = ey; ey = t;
                    }
                    startMonth = new DateTime(sy, 1, 1);
                    endMonth = new DateTime(ey, 12, 1);
                    break;
                case "CurrentYear":
                default:
                    startMonth = new DateTime(now.Year, 1, 1);
                    endMonth = new DateTime(now.Year, 12, 1);
                    break;
            }

            // Build months from startMonth to endMonth inclusive
            for (var month = startMonth; month <= endMonth; month = month.AddMonths(1))
            {
                var monthKey = month.ToString("yyyy-MM");
                var monthName = month.ToString("MMMM 'de' yyyy", CultureInfo.CurrentCulture);

                var budget = budgets.FirstOrDefault(b => b.Month == monthKey)?.Budget ?? 0;
                var paid = items
                    .Where(item => item.Status == StatusItem.Pago &&
                                  item.Vencimento.ToString("yyyy-MM") == monthKey)
                    .Sum(item => item.Valor);
                var pending = items
                    .Where(item => item.Status != StatusItem.Pago &&
                                  item.Vencimento.ToString("yyyy-MM") == monthKey)
                    .Sum(item => item.Valor);
                var balance = budget - paid;

                string status = "Sem budget";
                if (budget > 0)
                {
                    var percentage = budget == 0 ? 0 : (paid / budget) * 100;
                    if (percentage <= 80) status = "Saudável";
                    else if (percentage <= 100) status = "Atenção";
                    else status = "Excedido";
                }

                budgetRows.Add(new BudgetMonthRow
                {
                    Month = monthKey,
                    MesAno = char.ToUpper(monthName[0]) + monthName.Substring(1),
                    BudgetPlanejado = budget,
                    TotalPago = paid,
                    Pendente = pending,
                    Saldo = balance,
                    StatusDisplay = status
                });
            }

            // bind
            dgvBudgetMensal.AutoGenerateColumns = false;
            dgvBudgetMensal.DataSource = null;
            dgvBudgetMensal.DataSource = budgetRows;

            //Atualizar cards
            var totalBudget = budgetRows.Sum(b => b.BudgetPlanejado);
            var totalPaid = budgetRows.Sum(b => b.TotalPago);
            var totalBalance = totalBudget - totalPaid;

            lblValorBudgetTotal.Text = totalBudget.ToString("C2", CultureInfo.CurrentCulture);
            lblValorPagoAnual.Text = totalPaid.ToString("C2", CultureInfo.CurrentCulture);
            lblValorSaldoAnual.Text = totalBalance.ToString("C2", CultureInfo.CurrentCulture);

            // lock resizing and make columns non-resizable
            foreach (DataGridViewColumn c in dgvBudgetMensal.Columns)
            {
                c.Resizable = DataGridViewTriState.False;
            }

            // small visual adjustments to specific columns if present
            if (dgvBudgetMensal.Columns["MesAno"] != null) dgvBudgetMensal.Columns["MesAno"].HeaderText = "Mês";
            if (dgvBudgetMensal.Columns["BudgetPlanejado"] != null) dgvBudgetMensal.Columns["BudgetPlanejado"].HeaderText = "Budget Planejado";
            if (dgvBudgetMensal.Columns["TotalPago"] != null) dgvBudgetMensal.Columns["TotalPago"].HeaderText = "Total Pago";
            if (dgvBudgetMensal.Columns["Pendente"] != null) dgvBudgetMensal.Columns["Pendente"].HeaderText = "Pendente";
            if (dgvBudgetMensal.Columns["Saldo"] != null) dgvBudgetMensal.Columns["Saldo"].HeaderText = "Saldo";
            if (dgvBudgetMensal.Columns["StatusDisplay"] != null) dgvBudgetMensal.Columns["StatusDisplay"].HeaderText = "Status";

            dgvBudgetMensal.Refresh();
        }

        private void DgvBudgetMensal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && dgvBudgetMensal.Columns.Count > e.ColumnIndex)
            {
                var col = dgvBudgetMensal.Columns[e.ColumnIndex];
                // support both designer-named column and legacy names
                if (col.Name == "Acoes" || col.Name == "btnEdit" || col.Name == "Editar")
                {
                    var row = (BudgetMonthRow)dgvBudgetMensal.Rows[e.RowIndex].DataBoundItem;

                    using var dlg = new EditBudget();
                    dlg.SetMonth(row.MesAno, row.BudgetPlanejado);
                    var result = dlg.ShowDialog(this);
                    if (result == DialogResult.OK)
                    {
                        var newBudget = dlg.BudgetValue;
                        var monthKey = row.Month;
                        var existing = budgets.FirstOrDefault(b => b.Month == monthKey);
                        if (existing != null)
                        {
                            if (existing.Budget != newBudget)
                            {
                                existing.Budget = newBudget;
                                BudgetsChanged?.Invoke();
                            }
                        }
                        else
                        {
                            budgets.Add(new MonthlyBudget { Month = monthKey, Budget = newBudget });
                            BudgetsChanged?.Invoke();
                        }

                        PopulateBudget();
                    }
                }
            }
        }

        private void DgvBudgetMensal_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvBudgetMensal.Rows.Count == 0) return;

            var col = dgvBudgetMensal.Columns[e.ColumnIndex];
            var row = dgvBudgetMensal.Rows[e.RowIndex];
            if (row.DataBoundItem is not BudgetMonthRow data) return;

            // Format BudgetPlanejado: show "Não definido" when zero
            if (col.DataPropertyName == "BudgetPlanejado")
            {
                if (data.BudgetPlanejado == 0)
                {
                    e.Value = "Não definido";
                    e.CellStyle.ForeColor = Color.Gray;
                    e.FormattingApplied = true;
                }
                else
                {
                    e.Value = data.BudgetPlanejado.ToString("C2", CultureInfo.CurrentCulture);
                    e.CellStyle.ForeColor = Color.Black;
                    e.FormattingApplied = true;
                }
            }

            // Format TotalPago, Pendente, Saldo as currency when numeric
            if (col.DataPropertyName == "TotalPago" || col.DataPropertyName == "Pendente" || col.DataPropertyName == "Saldo")
            {
                decimal val = 0;
                if (col.DataPropertyName == "TotalPago") val = data.TotalPago;
                if (col.DataPropertyName == "Pendente") val = data.Pendente;
                if (col.DataPropertyName == "Saldo") val = data.Saldo;

                e.Value = val.ToString("C2", CultureInfo.CurrentCulture);
                e.FormattingApplied = true;

                if (col.DataPropertyName == "Pendente" && val > 0)
                {
                    e.CellStyle.ForeColor = Color.OrangeRed;
                }
                if (col.DataPropertyName == "Saldo")
                {
                    e.CellStyle.ForeColor = val >= 0 ? Color.FromArgb(6, 120, 60) : Color.FromArgb(153, 27, 27);
                }
            }

            // Status column: render as badge-like text color
            if (col.DataPropertyName == "StatusDisplay")
            {
                if (data.StatusDisplay == "Saudável")
                {
                    e.CellStyle.ForeColor = Color.FromArgb(6, 120, 60);
                }
                else if (data.StatusDisplay == "Atenção")
                {
                    e.CellStyle.ForeColor = Color.OrangeRed;
                }
                else if (data.StatusDisplay == "Excedido")
                {
                    e.CellStyle.ForeColor = Color.DarkRed;
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Gray;
                }
                e.Value = data.StatusDisplay;
                e.FormattingApplied = true;
            }

            // Month column text left aligned
            if (col.DataPropertyName == "MesAno")
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }
    }
}
