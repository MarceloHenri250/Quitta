using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitta.Models
{
    public class NotificationSettings
    {
        public bool Enabled { get; set; }
        public int DaysBeforeDue { get; set; }
        public bool EmailNotifications { get; set; }
        public string Email { get; set; }
        public bool DesktopNotifications { get; set; }
    }
}
