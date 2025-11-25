using Quitta.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitta.UserControls
{
    public partial class DashboardControl : UserControl
    {
        private List<Item> items = new();
        private List<MonthlyBudget> budgets = new();

        public DashboardControl()
        {
            InitializeComponent();
            ConfigureVencimentosGrid();
        }

        private void ConfigureVencimentosGrid()
        {
            // Ensure we control which columns are shown (do not rely on auto-generation)
            dgvVencimentos.AutoGenerateColumns = false;
            dgvVencimentos.Columns.Clear();

            var colTipo = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Tipo",
                Name = "colTipo",
                HeaderText = "Tipo",
                FillWeight = 12F,
                ReadOnly = true,
                MinimumWidth = 6,
                Resizable = DataGridViewTriState.False
            };

            var colNumero = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Numero",
                Name = "colNumero",
                HeaderText = "Número",
                FillWeight = 20F,
                ReadOnly = true,
                MinimumWidth = 6,
                Resizable = DataGridViewTriState.False
            };

            var colFornecedor = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Fornecedor",
                Name = "colFornecedor",
                HeaderText = "Fornecedor",
                FillWeight = 25F,
                ReadOnly = true,
                MinimumWidth = 6,
                Resizable = DataGridViewTriState.False
            };

            var colValor = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Valor",
                Name = "colValor",
                HeaderText = "Valor",
                FillWeight = 12F,
                ReadOnly = true,
                MinimumWidth = 6,
                Resizable = DataGridViewTriState.False
            };
            colValor.DefaultCellStyle = new DataGridViewCellStyle { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight };

            var colVencimento = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Vencimento",
                Name = "colVencimento",
                HeaderText = "Vencimento",
                FillWeight = 16F,
                ReadOnly = true,
                MinimumWidth = 6,
                Resizable = DataGridViewTriState.False
            };
            colVencimento.DefaultCellStyle = new DataGridViewCellStyle { Format = "d", Alignment = DataGridViewContentAlignment.MiddleLeft };

            dgvVencimentos.Columns.AddRange(new DataGridViewColumn[] { colTipo, colNumero, colFornecedor, colValor, colVencimento });

            // Visual tweaks to match other grids
            dgvVencimentos.EnableHeadersVisualStyles = false;
            dgvVencimentos.BackgroundColor = Color.White;
            dgvVencimentos.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.FromArgb(250, 250, 250) };
        }

        /// <summary>
        /// Recebe os dados para exibição na dashboard.
        /// </summary>
        public void SetData(List<Item> items, List<MonthlyBudget> budgets)
        {
            this.items = items ?? new List<Item>();
            this.budgets = budgets ?? new List<MonthlyBudget>();
            UpdateDashboard();
        }

        /// <summary>
        /// Atualiza todos os cards e o grid da dashboard.
        /// </summary>
        public void UpdateDashboard()
        {
            AtualizarCards();
            AtualizarGridVencimentos();
            AtualizarGrafico();
        }

        private void AtualizarCards()
        {
            var totalPendente = items.Where(i => i.Status == StatusItem.Pendente).Sum(i => i.Valor);
            var totalVencido = items.Where(i => i.Status == StatusItem.Vencido).Sum(i => i.Valor);
            var totalPago = items.Where(i => i.Status == StatusItem.Pago).Sum(i => i.Valor);

            lblValorPendente.Text = totalPendente.ToString("C2");
            lblValorVencido.Text = totalVencido.ToString("C2");
            lblValorPago.Text = totalPago.ToString("C2");

            var currentMonth = DateTime.Now.ToString("yyyy-MM");
            var budgetMensal = budgets.FirstOrDefault(b => b.Month == currentMonth)?.Budget ?? 0;
            var pagoMensal = items
                .Where(i => i.Status == StatusItem.Pago && i.Vencimento.ToString("yyyy-MM") == currentMonth)
                .Sum(i => i.Valor);
            var saldoMensal = budgetMensal - pagoMensal;

            lblValorBudget.Text = budgetMensal.ToString("C2");
            lblValorSaldo.Text = saldoMensal.ToString("C2");
            lblValorSaldo.ForeColor = saldoMensal >= 0 ? Color.FromArgb(6, 95, 70) : Color.FromArgb(153, 27, 27);
        }

        private void AtualizarGridVencimentos()
        {
            var vencimentos = items
                .Where(i => i.Status == StatusItem.Pendente &&
                            i.Vencimento >= DateTime.Now &&
                            i.Vencimento <= DateTime.Now.AddDays(30))
                .OrderBy(i => i.Vencimento)
                .Take(10)
                .ToList();

            dgvVencimentos.DataSource = null;
            dgvVencimentos.DataSource = vencimentos;
        }

        private void AtualizarGrafico()
        {
            // Use fixed dimensions to avoid rendering cuts
            const int targetWidth = 1342;
            const int targetHeight = 187;
            const int legendHeight = 30; // reserve bottom area for legend

            if (picGrafico == null) return;

            // ensure picturebox scales image instead of cropping
            picGrafico.SizeMode = PictureBoxSizeMode.Zoom;

            var bmp = new Bitmap(targetWidth, targetHeight);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);

                // colors
                var colorBoleto = Color.FromArgb(30, 64, 175); // blue
                var colorNota = Color.FromArgb(218, 83, 17); // orange
                var brushBoleto = new SolidBrush(colorBoleto);
                var brushNota = new SolidBrush(colorNota);
                var penGrid = new Pen(Color.LightGray);

                // draw placeholder grid
                for (int i = 0; i < 5; i++)
                {
                    int y = 20 + i * (targetHeight - 40 - legendHeight) / 5;
                    g.DrawLine(penGrid, 40, y, targetWidth - 20, y);
                }

                // draw bars based on data
                var meses = Enumerable.Range(0, 6)
                    .Select(i => DateTime.Now.AddMonths(-5 + i))
                    .ToList();

                double[] valoresBoletos = meses.Select(m =>
                    (double)items.Where(x => x.Tipo == TipoItem.Boleto && x.Vencimento.Month == m.Month && x.Vencimento.Year == m.Year)
                         .Sum(x => x.Valor)
                ).ToArray();

                double[] valoresNotas = meses.Select(m =>
                    (double)items.Where(x => x.Tipo == TipoItem.Nota && x.Vencimento.Month == m.Month && x.Vencimento.Year == m.Year)
                         .Sum(x => x.Valor)
                ).ToArray();

                int[] countBoletos = meses.Select(m =>
                    items.Count(x => x.Tipo == TipoItem.Boleto && x.Vencimento.Month == m.Month && x.Vencimento.Year == m.Year)
                ).ToArray();

                int[] countNotas = meses.Select(m =>
                    items.Count(x => x.Tipo == TipoItem.Nota && x.Vencimento.Month == m.Month && x.Vencimento.Year == m.Year)
                ).ToArray();

                double maxVal = Math.Max(1, Math.Max(valoresBoletos.Max(), valoresNotas.Max()));
                int plotWidth = targetWidth - 60;
                int plotHeight = targetHeight - 80 - legendHeight; // leave top/bottom space + legend
                int startX = 50;
                int barGroupWidth = Math.Max(1, plotWidth / Math.Max(1, meses.Count()));
                int barWidth = (int)(barGroupWidth * 0.35);

                using (var fontValue = new Font("Segoe UI", 8, FontStyle.Bold))
                using (var fontLabel = new Font("Segoe UI", 8))
                using (var sfCenter = new StringFormat { Alignment = StringAlignment.Center })
                using (var sfRight = new StringFormat { Alignment = StringAlignment.Far })
                {
                    for (int i = 0; i < meses.Count; i++)
                    {
                        int groupX = startX + i * barGroupWidth;
                        int h1 = (int)(valoresBoletos[i] / maxVal * plotHeight);
                        int h2 = (int)(valoresNotas[i] / maxVal * plotHeight);

                        var rect1 = new Rectangle(groupX - barWidth / 2, (targetHeight - legendHeight) - 30 - h1, barWidth, h1);
                        var rect2 = new Rectangle(groupX + barWidth / 2, (targetHeight - legendHeight) - 30 - h2, barWidth, h2);

                        g.FillRectangle(brushBoleto, rect1);
                        g.FillRectangle(brushNota, rect2);

                        // draw currency totals above each bar
                        var valStr1 = ((decimal)valoresBoletos[i]).ToString("C0");
                        var valStr2 = ((decimal)valoresNotas[i]).ToString("C0");
                        int labelY1 = rect1.Y - 14;
                        int labelY2 = rect2.Y - 14;
                        g.DrawString(valStr1, fontValue, Brushes.Black, rect1.X + rect1.Width / 2, labelY1, sfCenter);
                        g.DrawString(valStr2, fontValue, Brushes.Black, rect2.X + rect2.Width / 2, labelY2, sfCenter);

                        // draw counts above the currency value
                        var countStr1 = countBoletos[i].ToString();
                        var countStr2 = countNotas[i].ToString();
                        int countY1 = labelY1 - 12;
                        int countY2 = labelY2 - 12;
                        g.DrawString(countStr1, fontLabel, Brushes.Black, rect1.X + rect1.Width / 2, countY1, sfCenter);
                        g.DrawString(countStr2, fontLabel, Brushes.Black, rect2.X + rect2.Width / 2, countY2, sfCenter);

                        // draw month label under each group for clarity
                        var monthLabel = meses[i].ToString("MMM");
                        int monthLabelY = (targetHeight - legendHeight) - 6; // just above legend area
                        g.DrawString(monthLabel, fontLabel, Brushes.Gray, groupX, monthLabelY, sfCenter);
                    }

                    // draw legend area (only labels)
                    int legendY = targetHeight - legendHeight + 6;
                    int legendX = 60;
                    int boxSize = 10;

                    // draw boleto legend label
                    g.FillRectangle(brushBoleto, new Rectangle(legendX, legendY, boxSize, boxSize));
                    g.DrawString("Boletos", fontLabel, Brushes.Black, legendX + boxSize + 6, legendY - 1);

                    // draw nota legend label
                    int gap = 120;
                    g.FillRectangle(brushNota, new Rectangle(legendX + gap, legendY, boxSize, boxSize));
                    g.DrawString("Notas", fontLabel, Brushes.Black, legendX + gap + boxSize + 6, legendY - 1);
                }

                penGrid.Dispose();
                brushBoleto.Dispose();
                brushNota.Dispose();
            }

            picGrafico.Image?.Dispose();
            picGrafico.Image = bmp;
        }
    }
}
