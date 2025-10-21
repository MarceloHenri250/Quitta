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
            // Placeholder: desenha um gráfico simples no PictureBox
            if (picGrafico == null) return;

            var bmp = new Bitmap(picGrafico.Width, picGrafico.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                using (var pen = new Pen(Color.LightGray))
                {
                    // draw placeholder grid
                    for (int i = 0; i < 5; i++)
                    {
                        int y = 20 + i * (bmp.Height - 40) / 5;
                        g.DrawLine(pen, 40, y, bmp.Width - 20, y);
                    }
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

                double maxVal = Math.Max(1, Math.Max(valoresBoletos.Max(), valoresNotas.Max()));
                int plotWidth = bmp.Width - 60;
                int plotHeight = bmp.Height - 80;
                int startX = 50;
                int barGroupWidth = plotWidth / meses.Count();
                int barWidth = (int)(barGroupWidth * 0.35);

                for (int i = 0; i < meses.Count; i++)
                {
                    int groupX = startX + i * barGroupWidth;
                    int h1 = (int)(valoresBoletos[i] / maxVal * plotHeight);
                    int h2 = (int)(valoresNotas[i] / maxVal * plotHeight);

                    var rect1 = new Rectangle(groupX - barWidth / 2, bmp.Height - 30 - h1, barWidth, h1);
                    var rect2 = new Rectangle(groupX + barWidth / 2, bmp.Height - 30 - h2, barWidth, h2);

                    g.FillRectangle(Brushes.DarkBlue, rect1);
                    g.FillRectangle(Brushes.SaddleBrown, rect2);
                }

                // draw x labels
                using (var sf = new StringFormat { Alignment = StringAlignment.Center })
                using (var font = new Font("Segoe UI", 8))
                {
                    for (int i = 0; i < meses.Count; i++)
                    {
                        int groupX = startX + i * barGroupWidth;
                        g.DrawString(meses[i].ToString("MMM"), font, Brushes.Black, groupX, bmp.Height - 20, sf);
                    }
                }
            }

            picGrafico.Image?.Dispose();
            picGrafico.Image = bmp;
        }
    }
}
