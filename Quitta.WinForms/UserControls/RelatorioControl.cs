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
using System.Drawing.Imaging;
using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;

namespace Quitta.UserControls
{
    public partial class RelatorioControl : UserControl
    {
        #region Eventos públicos
        // Eventos esperados pelo MainForm para notificar ações de export/filtragem
        public event Action? ExportPdfRequested;
        public event Action? ExportExcelRequested;
        public event Action<List<Item>>? FilterApplied;
        #endregion

        #region Campos privados
        // Dados carregados e filtrados
        private List<Item> allItems = new List<Item>();
        private List<Item> filteredItems = new List<Item>();

        // controle de visibilidade de filtros (padrão true para compatibilidade)
        private bool filtersVisible = true;
        #endregion

        #region Construtor e inicialização
        public RelatorioControl()
        {
            InitializeComponent();

            // garantir ajuste do splitter quando o controle estiver visível
            this.Load += RelatorioControl_Load;
            this.Resize += RelatorioControl_Load;

            // ligar eventos de botões e controles
            btnApply.Click += BtnApply_Click;
            btnClear.Click += BtnClear_Click;
            btnExportExcel.Click += BtnExportExcel_Click;
            btnExportPdf.Click += BtnExportPdf_Click;

            cmbPeriodo.SelectedIndexChanged += CmbPeriodo_SelectedIndexChanged;

            // configurar valores iniciais dos comboboxes
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

            // configurar grid (colunas manuais para consistência)
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

            // configurar pintura do painel de gráfico
            chartStatus.Paint += ChartStatus_Paint;
        }
        #endregion

        #region Eventos de UI (pequenas handlers)
        // Ativa/desativa controles de data quando período for 'Personalizado'
        private void CmbPeriodo_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var sel = cmbPeriodo.SelectedItem?.ToString() ?? "Todos";
            var enabled = sel == "Personalizado";
            dtpInicio.Enabled = enabled;
            dtpFim.Enabled = enabled;
        }

        // Ajusta o splitter na carga/redimensionamento do controle
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
                // ignorar erros de sizing durante inicialização
            }
        }
        #endregion

        #region Exportação para PDF
        // Handler do botão de exportar para PDF
        private void BtnExportPdf_Click(object? sender, EventArgs e)
        {
            // export PDF com renderização de chart e tabela
            if (filteredItems == null || filteredItems.Count == 0)
            {
                MessageBox.Show("Nenhum item para exportar.", "Exportar PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var defaultFile = $"relatorio_{timestamp}.pdf";

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF|*.pdf";
                sfd.FileName = defaultFile;
                if (sfd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    // gerar bitmap do gráfico (maior resolução para PDF)
                    int chartW = 800;
                    int chartH = 480;
                    using var chartBmp = DrawPdfChart(filteredItems, chartW, chartH);

                    using (var imgStream = new MemoryStream())
                    {
                        chartBmp.Save(imgStream, ImageFormat.Png);
                        imgStream.Position = 0;

                        using (var pdf = new PdfDocument())
                        {
                            int pageNumber = 1;
                            var page = pdf.AddPage();
                            page.Size = PdfSharpCore.PageSize.A4;
                            var gfx = XGraphics.FromPdfPage(page);

                            var titleFont = new XFont("Segoe UI", 16, XFontStyle.Bold);
                            var font = new XFont("Segoe UI", 10, XFontStyle.Regular);
                            var headerFont = new XFont("Segoe UI", 10, XFontStyle.Bold);

                            double margin = 40;
                            double y = 30;

                            // Título
                            gfx.DrawString("Relatório Quitta", titleFont, XBrushes.Black, new XRect(margin, y, page.Width - margin * 2, 24), XStringFormats.TopLeft);
                            var genText = "Gerado em: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                            // aproximar conteúdo do título
                            y += 28;

                            // estatísticas (Total, A Pagar, Pagos, Vencidos) calculadas diretamente
                            decimal totalAmount = filteredItems.Sum(i => i.Valor);
                            var toPayList = filteredItems.Where(i => i.Status == StatusItem.Pendente).ToList();
                            var paidList = filteredItems.Where(i => i.Status == StatusItem.Pago).ToList();
                            var overdueList = filteredItems.Where(i => i.Status == StatusItem.Vencido).ToList();

                            var stats = new[]
                            {
                                new { Title = "Total Geral", Amount = totalAmount, Count = filteredItems.Count, Color = XColors.DimGray },
                                new { Title = "A Pagar", Amount = toPayList.Sum(i=>i.Valor), Count = toPayList.Count, Color = XColors.Gold },
                                new { Title = "Pagos", Amount = paidList.Sum(i=>i.Valor), Count = paidList.Count, Color = XColors.Green },
                                new { Title = "Vencidos", Amount = overdueList.Sum(i=>i.Valor), Count = overdueList.Count, Color = XColors.Red }
                            };

                            // espaço extra
                            y += 14;

                            double panelSpacing = 10;
                            int panelCount = stats.Length;
                            double panelWidth = (page.Width - margin * 2 - panelSpacing * (panelCount - 1)) / panelCount;
                            double panelHeight = 64;
                            double px = margin;

                            var amountFont = new XFont("Segoe UI", 12, XFontStyle.Bold);
                            var titleSmallFont = new XFont("Segoe UI", 9, XFontStyle.Bold);
                            var smallFont = new XFont("Segoe UI", 9, XFontStyle.Regular);

                            for (int i = 0; i < stats.Length; i++)
                            {
                                var s = stats[i];
                                var bgColor = XColor.FromArgb(40, s.Color.R, s.Color.G, s.Color.B);
                                var bgBrush = new XSolidBrush(bgColor);
                                var borderPen = new XPen(XColors.LightGray);

                                // painel de fundo
                                gfx.DrawRectangle(borderPen, bgBrush, px, y, panelWidth, panelHeight);

                                // marcador colorido
                                double markerSize = 14;
                                double markerX = px + 10;
                                double markerY = y + (panelHeight - markerSize) / 2;
                                gfx.DrawRectangle(new XPen(XColors.Gray), new XSolidBrush(s.Color), markerX, markerY, markerSize, markerSize);

                                // título e valores
                                double textX = markerX + markerSize + 8;
                                gfx.DrawString(s.Title, titleSmallFont, XBrushes.Gray, new XRect(textX, y + 8, panelWidth - (textX - px) - 12, 12), XStringFormats.TopLeft);
                                gfx.DrawString(s.Amount.ToString("C"), amountFont, XBrushes.Black, new XRect(textX, y + 22, panelWidth - (textX - px) - 12, 26), XStringFormats.TopLeft);
                                var countText = s.Count + " itens";
                                gfx.DrawString(countText, smallFont, XBrushes.DarkGray, new XRect(textX, y + 44, panelWidth - (textX - px) - 12, 12), XStringFormats.TopLeft);

                                px += panelWidth + panelSpacing;
                            }

                            // avançar abaixo dos painéis
                            y += panelHeight + 12;

                            // espaço antes do gráfico
                            y += 12;

                            // desenhar gráfico centralizado
                            using (var ximg = XImage.FromStream(() => new MemoryStream(imgStream.ToArray())))
                            {
                                double maxImgWidth = page.Width - margin * 2;
                                double imgWidth = Math.Min(maxImgWidth, 520);
                                double imgHeight = ximg.PixelHeight * imgWidth / ximg.PixelWidth;
                                double imgX = margin + (maxImgWidth - imgWidth) / 2;
                                double imgY = y;
                                gfx.DrawImage(ximg, imgX, imgY, imgWidth, imgHeight);
                                y = imgY + imgHeight + 8;
                            }

                            // parâmetros da tabela (próxima ao chart)
                            double tableTop = y + 4;
                            double colLeft = margin;
                            double tableWidth = page.Width - margin * 2;

                            double wTipo = tableWidth * 0.08;
                            double wNumero = tableWidth * 0.12;
                            double wFornecedor = tableWidth * 0.40;
                            double wVenc = tableWidth * 0.12;
                            double wValor = tableWidth * 0.14;
                            double wStatus = tableWidth * 0.14;

                            // desenhar cabeçalho na primeira página
                            DrawTableHeader(gfx, colLeft, tableTop, new double[] { wTipo, wNumero, wFornecedor, wVenc, wValor, wStatus }, headerFont);

                            double rowY = tableTop + 18;
                            double lineHeight = 14;

                            int rowIndex = 0;

                            foreach (var it in filteredItems)
                            {
                                // nova página se necessário
                                if (rowY + lineHeight > page.Height - margin)
                                {
                                    DrawFooter(gfx, page, genText, pageNumber);
                                    pageNumber++;
                                    page = pdf.AddPage();
                                    page.Size = PdfSharpCore.PageSize.A4;
                                    gfx = XGraphics.FromPdfPage(page);
                                    DrawTableHeader(gfx, colLeft, margin, new double[] { wTipo, wNumero, wFornecedor, wVenc, wValor, wStatus }, headerFont);
                                    rowY = margin + 18;
                                }

                                // sombreamento alternado
                                double x = colLeft;
                                var brush = (rowIndex % 2 == 0) ? XBrushes.White : new XSolidBrush(XColor.FromArgb(245, 245, 245));

                                gfx.DrawRectangle(brush, x, rowY, wTipo, lineHeight);
                                gfx.DrawRectangle(XPens.Black, x, rowY, wTipo, lineHeight);
                                gfx.DrawString(it.Tipo.ToString(), font, XBrushes.Black, new XRect(x + 4, rowY + 1, wTipo - 8, lineHeight), XStringFormats.TopLeft);
                                x += wTipo;

                                gfx.DrawRectangle(brush, x, rowY, wNumero, lineHeight);
                                gfx.DrawRectangle(XPens.Black, x, rowY, wNumero, lineHeight);
                                gfx.DrawString(it.Numero, font, XBrushes.Black, new XRect(x + 4, rowY + 1, wNumero - 8, lineHeight), XStringFormats.TopLeft);
                                x += wNumero;

                                gfx.DrawRectangle(brush, x, rowY, wFornecedor, lineHeight);
                                gfx.DrawRectangle(XPens.Black, x, rowY, wFornecedor, lineHeight);
                                gfx.DrawString(it.Fornecedor, font, XBrushes.Black, new XRect(x + 4, rowY + 1, wFornecedor - 8, lineHeight), XStringFormats.TopLeft);
                                x += wFornecedor;

                                gfx.DrawRectangle(brush, x, rowY, wVenc, lineHeight);
                                gfx.DrawRectangle(XPens.Black, x, rowY, wVenc, lineHeight);
                                gfx.DrawString(it.Vencimento.ToString("dd/MM/yyyy"), font, XBrushes.Black, new XRect(x + 4, rowY + 1, wVenc - 8, lineHeight), XStringFormats.TopLeft);
                                x += wVenc;

                                gfx.DrawRectangle(brush, x, rowY, wValor, lineHeight);
                                gfx.DrawRectangle(XPens.Black, x, rowY, wValor, lineHeight);
                                gfx.DrawString(it.Valor.ToString("C"), font, XBrushes.Black, new XRect(x + 4, rowY + 1, wValor - 8, lineHeight), XStringFormats.TopLeft);
                                x += wValor;

                                gfx.DrawRectangle(brush, x, rowY, wStatus, lineHeight);
                                gfx.DrawRectangle(XPens.Black, x, rowY, wStatus, lineHeight);
                                gfx.DrawString(it.Status.ToString(), font, XBrushes.Black, new XRect(x + 4, rowY + 1, wStatus - 8, lineHeight), XStringFormats.TopLeft);

                                rowY += lineHeight;
                                rowIndex++;
                            }

                            // rodapé da última página
                            DrawFooter(gfx, page, genText, pageNumber);

                            // salvar PDF
                            using (var fs = File.OpenWrite(sfd.FileName))
                            {
                                pdf.Save(fs);
                            }
                        }
                    }

                    MessageBox.Show("Exportação para PDF concluída com sucesso.", "Exportar PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ExportPdfRequested?.Invoke();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao exportar PDF: {ex.Message}", "Exportar PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Gera bitmap do gráfico usado no PDF
        private Bitmap DrawPdfChart(List<Item> items, int width, int height)
        {
            var bmp = new Bitmap(width, height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // soma por status
                var groups = items.GroupBy(i => i.Status)
                    .Select(gp => new { Status = gp.Key, Total = gp.Sum(x => x.Valor) })
                    .OrderBy(gp => gp.Status)
                    .ToList();

                decimal total = groups.Sum(g => g.Total);
                if (total <= 0)
                {
                    using var f = new Font("Segoe UI", 12);
                    var txt = "Sem dados";
                    var sz = g.MeasureString(txt, f);
                    g.DrawString(txt, f, Brushes.Gray, (width - sz.Width) / 2, (height - sz.Height) / 2);
                    return bmp;
                }

                // rect do pie
                int pieSize = Math.Min(width, height) - 200;
                var pieRect = new Rectangle(20, 20, pieSize, pieSize);

                float startAngle = 0f;
                foreach (var grp in groups)
                {
                    float sweep = (float)(grp.Total / total) * 360f;
                    Brush brush = Brushes.Gray;
                    switch (grp.Status)
                    {
                        case StatusItem.Pendente: brush = Brushes.Gold; break;
                        case StatusItem.Pago: brush = Brushes.Green; break;
                        case StatusItem.Vencido: brush = Brushes.Red; break;
                    }
                    g.FillPie(brush, pieRect, startAngle, sweep);
                    g.DrawPie(Pens.Black, pieRect, startAngle, sweep);
                    startAngle += sweep;
                }

                // legenda à direita
                int lx = pieRect.Right + 20;
                int box = 18;
                using var legendFont = new Font("Segoe UI", 10);
                int totalLegendHeight = groups.Count * (box + 8) - 8;
                int startLy = pieRect.Top + (pieRect.Height - totalLegendHeight) / 2;
                int ly = startLy;
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
                    var text = $"{grp.Status} - {grp.Total.ToString("C")}";
                    g.DrawString(text, legendFont, Brushes.Black, lx + box + 8, ly);
                    ly += box + 8;
                }
            }

            return bmp;
        }

        // Desenha o cabeçalho da tabela no PDF
        private void DrawTableHeader(XGraphics gfx, double colLeft, double tableTop, double[] widths, XFont headerFont)
        {
            double x = colLeft;
            double h = 18;
            double totalW = widths.Sum();

            // fundo do cabeçalho
            gfx.DrawRectangle(XBrushes.LightGray, x, tableTop, totalW, h);

            // colunas
            gfx.DrawRectangle(XPens.Black, x, tableTop, widths[0], h);
            gfx.DrawString("Tipo", headerFont, XBrushes.Black, new XRect(x + 4, tableTop + 2, widths[0] - 8, h), XStringFormats.TopLeft);
            x += widths[0];

            gfx.DrawRectangle(XPens.Black, x, tableTop, widths[1], h);
            gfx.DrawString("Número", headerFont, XBrushes.Black, new XRect(x + 4, tableTop + 2, widths[1] - 8, h), XStringFormats.TopLeft);
            x += widths[1];

            gfx.DrawRectangle(XPens.Black, x, tableTop, widths[2], h);
            gfx.DrawString("Fornecedor", headerFont, XBrushes.Black, new XRect(x + 4, tableTop + 2, widths[2] - 8, h), XStringFormats.TopLeft);
            x += widths[2];

            gfx.DrawRectangle(XPens.Black, x, tableTop, widths[3], h);
            gfx.DrawString("Vencimento", headerFont, XBrushes.Black, new XRect(x + 4, tableTop + 2, widths[3] - 8, h), XStringFormats.TopLeft);
            x += widths[3];

            gfx.DrawRectangle(XPens.Black, x, tableTop, widths[4], h);
            gfx.DrawString("Valor", headerFont, XBrushes.Black, new XRect(x + 4, tableTop + 2, widths[4] - 8, h), XStringFormats.TopLeft);
            x += widths[4];

            gfx.DrawRectangle(XPens.Black, x, tableTop, widths[5], h);
            gfx.DrawString("Status", headerFont, XBrushes.Black, new XRect(x + 4, tableTop + 2, widths[5] - 8, h), XStringFormats.TopLeft);
        }
        #endregion

        #region Exportação para Excel
        // Handler do botão de exportar para Excel usando ClosedXML
        private void BtnExportExcel_Click(object? sender, EventArgs e)
        {
            if (filteredItems == null || filteredItems.Count == 0)
            {
                MessageBox.Show("Nenhum item para exportar.", "Exportar Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var defaultFile = $"relatorio_{timestamp}.xlsx";

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "Arquivos Excel|*.xlsx";
                sfd.FileName = defaultFile;
                if (sfd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (var wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add("Relatorio");
                        // cabeçalhos
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

                        // formatar coluna de valor
                        ws.Column(5).Style.NumberFormat.Format = "R$ #,##0.00";

                        // estilo do cabeçalho
                        var headerRange = ws.Range(1, 1, 1, 6);
                        headerRange.Style.Font.Bold = true;
                        headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

                        // bordas
                        var dataRange = ws.Range(1, 1, row - 1, 6);
                        dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

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
        #endregion

        #region Filtros e carregamento de dados
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

        // Chamado pelo MainForm para popular/atualizar o controle com dados
        public void SetData(List<Item>? items)
        {
            allItems = items != null ? new List<Item>(items) : new List<Item>();

            // popular fornecedores no combo
            var fornecedores = allItems.Select(i => i.Fornecedor).Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().OrderBy(s => s).ToList();
            cmbFornecedor.Items.Clear();
            cmbFornecedor.Items.Add("Todos");
            foreach (var f in fornecedores) cmbFornecedor.Items.Add(f);
            cmbFornecedor.SelectedIndex = 0;

            // datas padrão
            dtpInicio.Value = allItems.Any() ? allItems.Min(i => i.Vencimento).Date : DateTime.Now.Date;
            dtpFim.Value = allItems.Any() ? allItems.Max(i => i.Vencimento).Date : DateTime.Now.Date;

            // aplicar filtro inicial sem disparar evento
            ApplyFilters(false);
        }

        // Aplica filtros conforme controles e atualiza visualizações
        private void ApplyFilters(bool raiseEvent)
        {
            if (allItems == null) allItems = new List<Item>();

            IEnumerable<Item> query = allItems;

            // filtro por período
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

            // filtro por tipo
            var tipo = cmbTipo.SelectedItem?.ToString() ?? "Todos";
            if (tipo != "Todos")
            {
                if (Enum.TryParse<TipoItem>(tipo, out var t))
                {
                    query = query.Where(i => i.Tipo == t);
                }
            }

            // filtro por status
            var status = cmbStatus.SelectedItem?.ToString() ?? "Todos";
            if (status != "Todos")
            {
                if (Enum.TryParse<StatusItem>(status, out var s))
                {
                    query = query.Where(i => i.Status == s);
                }
            }

            // filtro por fornecedor
            var fornecedor = cmbFornecedor.SelectedItem?.ToString() ?? "Todos";
            if (!string.IsNullOrWhiteSpace(fornecedor) && fornecedor != "Todos")
            {
                query = query.Where(i => string.Equals(i.Fornecedor, fornecedor, StringComparison.OrdinalIgnoreCase));
            }

            // filtro por valores mínimo/máximo
            if (nudMin.Value > 0) query = query.Where(i => i.Valor >= nudMin.Value);
            if (nudMax.Value > 0) query = query.Where(i => i.Valor <= nudMax.Value);

            filteredItems = query.ToList();

            // atualizar grid (binding simples)
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

            // atualizar estatísticas e gráfico
            UpdateStatistics();
            UpdateChart();

            if (raiseEvent)
            {
                FilterApplied?.Invoke(new List<Item>(filteredItems));
            }
        }
        #endregion

        #region Gráfico e estatísticas
        // Força repaint do painel de gráfico
        private void UpdateChart()
        {
            try
            {
                chartStatus.Invalidate();
            }
            catch
            {
            }
        }

        // Pintura custom do painel de status (pie chart simples)
        private void ChartStatus_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var total = filteredItems.Count;
            if (total == 0)
            {
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

            // legenda à direita
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

        // Atualiza labels de estatísticas (total, a pagar, pagos, vencidos)
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
        #endregion

        #region Rodapé do PDF (helper)
        // Desenha rodapé com número de página e data de geração
        private void DrawFooter(XGraphics gfx, PdfPage page, string genText, int pageNumber)
        {
            var footerFont = new XFont("Segoe UI", 9, XFontStyle.Regular);
            double margin = 40;
            double y = page.Height - 30;
            // linha acima do rodapé
            gfx.DrawLine(XPens.LightGray, margin, y - 8, page.Width - margin, y - 8);
            // esquerda: timestamp
            gfx.DrawString(genText, footerFont, XBrushes.Gray, new XRect(margin, y - 4, 300, 18), XStringFormats.TopLeft);
            // direita: número da página
            var pageText = $"Página {pageNumber}";
            gfx.DrawString(pageText, footerFont, XBrushes.Gray, new XRect(margin, y - 4, page.Width - margin * 2, 18), XStringFormats.TopRight);
        }
        #endregion
    }
}
