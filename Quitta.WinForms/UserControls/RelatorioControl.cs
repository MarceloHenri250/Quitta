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

namespace Quitta.UserControls
{
    public partial class RelatorioControl : UserControl
    {
        // Events expected by MainForm
        public event Action? ExportPdfRequested;
        public event Action? ExportExcelRequested;
        public event Action<List<Item>>? FilterApplied;

        private Label lblDev;

        public RelatorioControl()
        {
            InitializeComponent();

            // Minimal UI to indicate feature is in development
            lblDev = new Label()
            {
                Text = "Relatório - Em desenvolvimento",
                AutoSize = true,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point),
                Location = new Point(10, 10)
            };

            this.Controls.Clear();
            this.Controls.Add(lblDev);
        }

        // Called by MainForm to populate/update the control
        public void SetData(List<Item>? items)
        {
            // For now, just update label with a count to show it's receiving data
            if (lblDev != null)
            {
                var count = items?.Count ?? 0;
                lblDev.Text = $"Relatório - Em desenvolvimento ({count} itens)";
            }

            // In the future, raise FilterApplied when user filters the report
            // and raise ExportPdfRequested / ExportExcelRequested when user requests exports.
        }
    }
}
