using Quitta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Quitta.Services
{
    internal sealed class NotificationManager : IDisposable
    {
        private static readonly Lazy<NotificationManager> _instance = new(() => new NotificationManager());
        public static NotificationManager Instance => _instance.Value;

        private System.Threading.Timer? _timer;
        private readonly object _lock = new();
        private NotifyIcon? _notifyIcon;

        private NotificationManager() { }

        public void InitializeFromSettings()
        {
            Restart();
        }

        public void Restart()
        {
            Stop();

            if (!Properties.Settings.Default.EnableNotifications || !Properties.Settings.Default.NotifyDesktop)
                return;

            // create notify icon if needed
            if (_notifyIcon == null)
            {
                _notifyIcon = new NotifyIcon();
                _notifyIcon.Icon = SystemIcons.Application;
                _notifyIcon.Visible = true;
                _notifyIcon.Text = "Quitta - Notificações";
            }

            // check immediately and then every hour while app is open
            _timer = new System.Threading.Timer(async _ => await TimerTick(), null, TimeSpan.Zero, TimeSpan.FromHours(1));
        }

        private async Task TimerTick()
        {
            try
            {
                CheckAndNotify();
            }
            catch
            {
                // ignore
            }
            await Task.CompletedTask;
        }

        private void CheckAndNotify()
        {
            lock (_lock)
            {
                try
                {
                    if (!Properties.Settings.Default.EnableNotifications || !Properties.Settings.Default.NotifyDesktop)
                        return;

                    var ds = new DataService();
                    var items = ds.LoadItems();

                    int days = Math.Max(0, Properties.Settings.Default.NotificationAdvanceDays);
                    var today = DateTime.Now.Date;
                    var end = today.AddDays(days);

                    var due = items
                        .Where(i => i.Status != StatusItem.Pago && i.Vencimento.Date >= today && i.Vencimento.Date <= end)
                        .OrderBy(i => i.Vencimento)
                        .ToList();

                    if (due.Count == 0) return;

                    // Prepare message
                    var lines = due.Take(6).Select(i => $"{i.Numero} - {i.Fornecedor} - {i.Vencimento:dd/MM} - {i.Valor:C2}");
                    string text = string.Join("\n", lines);
                    if (due.Count > 6) text += $"\n... e mais {due.Count - 6} itens";

                    // Show balloon on UI thread if available, otherwise direct
                    var mainForm = Application.OpenForms.OfType<Form>().FirstOrDefault();
                    if (mainForm != null && mainForm.IsHandleCreated)
                    {
                        try
                        {
                            mainForm.BeginInvoke((Action)(() =>
                            {
                                try
                                {
                                    _notifyIcon?.ShowBalloonTip(10000, "Lembretes de vencimento", text, ToolTipIcon.Info);
                                }
                                catch { }
                            }));
                        }
                        catch { }
                    }
                    else
                    {
                        try
                        {
                            _notifyIcon?.ShowBalloonTip(10000, "Lembretes de vencimento", text, ToolTipIcon.Info);
                        }
                        catch { }
                    }
                }
                catch
                {
                    // ignore
                }
            }
        }

        public void Stop()
        {
            lock (_lock)
            {
                _timer?.Dispose();
                _timer = null;
                if (_notifyIcon != null)
                {
                    try { _notifyIcon.Visible = false; } catch { }
                    try { _notifyIcon.Dispose(); } catch { }
                    _notifyIcon = null;
                }
            }
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
