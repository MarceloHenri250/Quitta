using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quitta.Models;
using ClosedXML.Excel;
using System.IO;
// removed DataVisualization.Charting to avoid extra package

namespace Quitta.UserControls
{
    public partial class RelatorioControl : UserControl
    {
        // Events expected by MainForm
        public event Action? ExportPdfRequested;
        public event Action? ExportExcelRequested;
        public event Action<List<Item>>? FilterApplied;

        private List<Item> allItems = new List<Item>();
        private List<Item> filteredItems = new List<Item>();
        private bool filtersVisible = true;

        public RelatorioControl()
        {
            InitializeComponent();

            // ensure splitter distance set when control is visible
            this.Load += RelatorioControl_Load;
            this.Resize += RelatorioControl_Load;

            // wire events
            btnApply.Click += BtnApply_Click;
            btnClear.Click += BtnClear_Click;
            btnExportExcel.Click += BtnExportExcel_Click;
            btnExportPdf.Click += BtnExportPdf_Click;

            cmbPeriodo.SelectedIndexChanged += CmbPeriodo_SelectedIndexChanged;

            // setup combo defaults
            cmbPeriodo.Items.AddRange(new object[] { "Todos", "Mês Atual", "Mês Anterior", "Trimestre", "Ano Atual", "Personalizado" });
            cmbPeriodo.SelectedIndex = 0;

            cmbTipo.Items.AddRange(new object[] { "Todos", "Boleto", "Nota" });
            cmbTipo.SelectedIndex = 0;

            cmbStatus.Items.AddRange(new object[] { "Todos", "Pendente", "Pago", "Vencido" });
            cmbStatus.SelectedIndex = 0;

            nudMin.Minimum = 0;
            nudMin.Maximum = decimal.MaxValue;
            nudMax.Minimum = 0;
            nudMax.Maximum = decimal.MaxValue;

            // configure grid
            dgvPreview.AutoGenerateColumns = false;
            dgvPreview.Columns.Clear();
            var colTipo = new DataGridViewTextBoxColumn() { Name = "Tipo", HeaderText = "Tipo", DataPropertyName = "Tipo", FillWeight = 8 };
            var colNumero = new DataGridViewTextBoxColumn() { Name = "Numero", HeaderText = "Número", DataPropertyName = "Numero", FillWeight = 12 };
            var colFornecedor = new DataGridViewTextBoxColumn() { Name = "Fornecedor", HeaderText = "Fornecedor", DataPropertyName = "Fornecedor", FillWeight = 40 };
            var colVenc = new DataGridViewTextBoxColumn() { Name = "Vencimento", HeaderText = "Vencimento", DataPropertyName = "Vencimento", FillWeight = 12 };
            var colValor = new DataGridViewTextBoxColumn() { Name = "Valor", HeaderText = "Valor", DataPropertyName = "Valor", FillWeight = 14 };
            var colStatus = new DataGridViewTextBoxColumn() { Name = "Status", HeaderText = "Status", DataPropertyName = "Status", FillWeight = 8 };

            dgvPreview.Columns.AddRange(new DataGridViewColumn[] { colTipo, colNumero, colFornecedor, colVenc, colValor, colStatus });

            dgvPreview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPreview.ReadOnly = true;
            dgvPreview.AllowUserToAddRows = false;
            dgvPreview.AllowUserToDeleteRows = false;
            dgvPreview.RowTemplate.Height = 22;

            // configure chart panel paint
            chartStatus.Paint += ChartStatus_Paint;
        }

        private void CmbPeriodo_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var sel = cmbPeriodo.SelectedItem?.ToString() ?? "Todos";
            var enabled = sel == "Personalizado";
            dtpInicio.Enabled = enabled;
            dtpFim.Enabled = enabled;
        }

        private void RelatorioControl_Load(object? sender, EventArgs e)
        {
            try
            {
                if (splitMain != null)
                {
                    int preferred = 320;
                    int min = Math.Max(splitMain.Panel1MinSize, 200);
                    int maxSplitter = this.Width - Math.Max(splitMain.Panel2MinSize, 400);
                    if (maxSplitter <= min)
                    {
                        splitMain.SplitterDistance = min;
                    }
                    else
                    {
                        splitMain.SplitterDistance = Math.Min(preferred, maxSplitter);
                    }
                }
            }
            catch
            {
                // ignore sizing errors at early initialization
            }
        }

        private void BtnExportPdf_Click(object? sender, EventArgs e)
        {
            // keep existing event for backwards compatibility
            ExportPdfRequested?.Invoke();
        }

        private void BtnExportExcel_Click(object? sender, EventArgs e)
        {
            // perform export using ClosedXML with current filteredItems
            if (filteredItems == null || filteredItems.Count == 0)
            {
                MessageBox.Show("Nenhum item para exportar.", "Exportar Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Arquivos Excel|*.xlsx";
                sfd.FileName = "Relatorio.xlsx";
                if (sfd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (var wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add("Relatorio");
                        // headers
                        ws.Cell(1, 1).Value = "Tipo";
                        ws.Cell(1, 2).Value = "Número";
                        ws.Cell(1, 3).Value = "Fornecedor";
                        ws.Cell(1, 4).Value = "Vencimento";
                        ws.Cell(1, 5).Value = "Valor";
                        ws.Cell(1, 6).Value = "Status";

                        int row = 2;
                        foreach (var it in filteredItems)
                        {
                            ws.Cell(row, 1).Value = it.Tipo.ToString();
                            ws.Cell(row, 2).Value = it.Numero;
                            ws.Cell(row, 3).Value = it.Fornecedor;
                            ws.Cell(row, 4).Value = it.Vencimento.ToString("dd/MM/yyyy");
                            ws.Cell(row, 5).Value = it.Valor;
                            ws.Cell(row, 6).Value = it.Status.ToString();
                            row++;
                        }

                        // format valor column
                        ws.Column(5).Style.NumberFormat.Format = "R$ #,##0.00";
                        ws.Columns().AdjustToContents();

                        wb.SaveAs(sfd.FileName);
                    }

                    MessageBox.Show("Exportação concluída com sucesso.", "Exportar Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ExportExcelRequested?.Invoke();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao exportar: {ex.Message}", "Exportar Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnClear_Click(object? sender, EventArgs e)
        {
            cmbPeriodo.SelectedIndex = 0;
            cmbTipo.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;
            cmbFornecedor.Items.Clear();
            cmbFornecedor.Items.Add("Todos");
            cmbFornecedor.SelectedIndex = 0;
            dtpInicio.Value = DateTime.Now.Date;
            dtpFim.Value = DateTime.Now.Date;
            nudMin.Value = 0;
            nudMax.Value = 0;

            ApplyFilters(true);
        }

        private void BtnApply_Click(object? sender, EventArgs e)
        {
            ApplyFilters(true);
        }

        // Called by MainForm to populate/update the control
        public void SetData(List<Item>? items)
        {
            allItems = items != null ? new List<Item>(items) : new List<Item>();

            // populate fornecedores
            var fornecedores = allItems.Select(i => i.Fornecedor).Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().OrderBy(s => s).ToList();
            cmbFornecedor.Items.Clear();
            cmbFornecedor.Items.Add("Todos");
            foreach (var f in fornecedores) cmbFornecedor.Items.Add(f);
            cmbFornecedor.SelectedIndex = 0;

            // default dates
            dtpInicio.Value = allItems.Any() ? allItems.Min(i => i.Vencimento).Date : DateTime.Now.Date;
            dtpFim.Value = allItems.Any() ? allItems.Max(i => i.Vencimento).Date : DateTime.Now.Date;

            // initial filter and display
            ApplyFilters(false);
        }

        private void ApplyFilters(bool raiseEvent)
        {
            if (allItems == null) allItems = new List<Item>();

            IEnumerable<Item> query = allItems;

            // period filter
            var periodo = cmbPeriodo.SelectedItem?.ToString() ?? "Todos";
            DateTime? start = null, end = null;
            var today = DateTime.Now.Date;
            switch (periodo)
            {
                case "Mês Atual":
                    start = new DateTime(today.Year, today.Month, 1);
                    end = start.Value.AddMonths(1).AddDays(-1);
                    break;
                case "Mês Anterior":
                    var prev = today.AddMonths(-1);
                    start = new DateTime(prev.Year, prev.Month, 1);
                    end = start.Value.AddMonths(1).AddDays(-1);
                    break;
                case "Trimestre":
                    int currentQuarter = (today.Month - 1) / 3 + 1;
                    int firstMonth = (currentQuarter - 1) * 3 + 1;
                    start = new DateTime(today.Year, firstMonth, 1);
                    end = start.Value.AddMonths(3).AddDays(-1);
                    break;
                case "Ano Atual":
                    start = new DateTime(today.Year, 1, 1);
                    end = new DateTime(today.Year, 12, 31);
                    break;
                case "Personalizado":
                    start = dtpInicio.Value.Date;
                    end = dtpFim.Value.Date;
                    break;
                default:
                    start = null; end = null;
                    break;
            }

            if (start.HasValue && end.HasValue)
            {
                query = query.Where(i => i.Vencimento.Date >= start.Value && i.Vencimento.Date <= end.Value);
            }

            // tipo
            var tipo = cmbTipo.SelectedItem?.ToString() ?? "Todos";
            if (tipo != "Todos")
            {
                if (Enum.TryParse<TipoItem>(tipo, out var t))
                {
                    query = query.Where(i => i.Tipo == t);
                }
            }

            // status
            var status = cmbStatus.SelectedItem?.ToString() ?? "Todos";
            if (status != "Todos")
            {
                if (Enum.TryParse<StatusItem>(status, out var s))
                {
                    query = query.Where(i => i.Status == s);
                }
            }

            // fornecedor
            var fornecedor = cmbFornecedor.SelectedItem?.ToString() ?? "Todos";
            if (!string.IsNullOrWhiteSpace(fornecedor) && fornecedor != "Todos")
            {
                query = query.Where(i => string.Equals(i.Fornecedor, fornecedor, StringComparison.OrdinalIgnoreCase));
            }

            // valor min/max
            if (nudMin.Value > 0) query = query.Where(i => i.Valor >= nudMin.Value);
            if (nudMax.Value > 0) query = query.Where(i => i.Valor <= nudMax.Value);

            filteredItems = query.ToList();

            // update grid
            var binding = filteredItems.Select(i => new
            {
                Tipo = i.Tipo.ToString(),
                Numero = i.Numero,
                Fornecedor = i.Fornecedor,
                Vencimento = i.Vencimento.ToString("dd/MM/yyyy"),
                Valor = i.Valor.ToString("C"),
                Status = i.Status.ToString()
            }).ToList();

            dgvPreview.DataSource = binding;

            // update statistics
            UpdateStatistics();

            // update chart
            UpdateChart();

            if (raiseEvent)
            {
                FilterApplied?.Invoke(new List<Item>(filteredItems));
            }
        }

        private void UpdateChart()
        {
            // just invalidate panel; painting logic will read filteredItems
            try
            {
                chartStatus.Invalidate();
            }
            catch
            {
            }
        }

        private void ChartStatus_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var total = filteredItems.Count;
            if (total == 0)
            {
                // draw placeholder text
                using var f = new Font("Segoe UI", 10);
                var txt = "Sem dados";
                var sz = g.MeasureString(txt, f);
                g.DrawString(txt, f, Brushes.Gray, (chartStatus.Width - sz.Width) / 2, (chartStatus.Height - sz.Height) / 2);
                return;
            }

            var groups = filteredItems.GroupBy(i => i.Status).Select(gp => new { Status = gp.Key, Count = gp.Count() }).ToList();
            float startAngle = 0f;
            var rect = new Rectangle(10, 10, Math.Min(chartStatus.Width, chartStatus.Height) - 40, Math.Min(chartStatus.Width, chartStatus.Height) - 40);

            foreach (var grp in groups)
            {
                float sweep = (float)grp.Count / total * 360f;
                Brush brush = Brushes.Gray;
                switch (grp.Status)
                {
                    case StatusItem.Pendente: brush = Brushes.Gold; break;
                    case StatusItem.Pago: brush = Brushes.Green; break;
                    case StatusItem.Vencido: brush = Brushes.Red; break;
                }
                g.FillPie(brush, rect, startAngle, sweep);
                startAngle += sweep;
            }

            // draw legend on right
            int lx = rect.Right + 10;
            int ly = rect.Top;
            int box = 14;
            using var lblFont = new Font("Segoe UI", 9);
            foreach (var grp in groups)
            {
                Brush brush = Brushes.Gray;
                switch (grp.Status)
                {
                    case StatusItem.Pendente: brush = Brushes.Gold; break;
                    case StatusItem.Pago: brush = Brushes.Green; break;
                    case StatusItem.Vencido: brush = Brushes.Red; break;
                }
                g.FillRectangle(brush, lx, ly, box, box);
                g.DrawRectangle(Pens.Black, lx, ly, box, box);
                var text = $"{grp.Status} ({grp.Count})";
                g.DrawString(text, lblFont, Brushes.Black, lx + box + 6, ly - 3);
                ly += box + 8;
            }
        }

        private void UpdateStatistics()
        {
            if (filteredItems == null) filteredItems = new List<Item>();

            decimal total = filteredItems.Sum(i => i.Valor);
            var toPay = filteredItems.Where(i => i.Status == StatusItem.Pendente).ToList();
            var paid = filteredItems.Where(i => i.Status == StatusItem.Pago).ToList();
            var overdue = filteredItems.Where(i => i.Status == StatusItem.Vencido).ToList();

            if (lblTotalValue != null) lblTotalValue.Text = total.ToString("C");
            if (lblTotalCount != null) lblTotalCount.Text = filteredItems.Count + " itens";

            if (lblToPayValue != null) lblToPayValue.Text = toPay.Sum(i => i.Valor).ToString("C");
            if (lblToPayCount != null) lblToPayCount.Text = toPay.Count + " itens";

            if (lblPaidValue != null) lblPaidValue.Text = paid.Sum(i => i.Valor).ToString("C");
            if (lblPaidCount != null) lblPaidCount.Text = paid.Count + " itens";

            if (lblOverdueValue != null) lblOverdueValue.Text = overdue.Sum(i => i.Valor).ToString("C");
            if (lblOverdueCount != null) lblOverdueCount.Text = overdue.Count + " itens";
        }
    }
}
