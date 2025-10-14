using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitta.Models
{
    public class BudgetMonthRow
    {
        public string Month { get; set; }
        public string MesAno { get; set; }
        public decimal BudgetPlanejado { get; set; }
        public decimal TotalPago { get; set; }
        public decimal Pendente { get; set; }
        public decimal Saldo { get; set; }
        public string StatusDisplay { get; set; }
    }
}
