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
        #region Singleton
        private static readonly Lazy<NotificationManager> _instance = new(() => new NotificationManager());
        public static NotificationManager Instance => _instance.Value;
        #endregion

        #region Campos privados / estado
        private System.Threading.Timer? _timer;
        private readonly object _lock = new();
        private NotifyIcon? _notifyIcon;
        #endregion

        #region Construtor
        // Construtor privado para singleton
        private NotificationManager() { }
        #endregion

        #region Inicialização a partir das configurações
        // Inicializa o gerenciador com base nas configurações salvas
        public void InitializeFromSettings()
        {
            Restart();
        }

        // Reinicia verificação/agendamento conforme configurações atuais
        public void Restart()
        {
            Stop();

            if (!Properties.Settings.Default.EnableNotifications || !Properties.Settings.Default.NotifyDesktop)
                return;

            // cria notify icon se necessário
            if (_notifyIcon == null)
            {
                _notifyIcon = new NotifyIcon();
                _notifyIcon.Icon = SystemIcons.Application;
                _notifyIcon.Visible = true;
                _notifyIcon.Text = "Quitta - Notificações";
            }

            // checa imediatamente e depois a cada hora enquanto o app estiver aberto
            _timer = new System.Threading.Timer(async _ => await TimerTick(), null, TimeSpan.Zero, TimeSpan.FromHours(1));
        }
        #endregion

        #region Agendamento / Tick
        // Executado periodicamente pelo timer; delega para CheckAndNotify
        private async Task TimerTick()
        {
            try
            {
                CheckAndNotify();
            }
            catch
            {
                // ignorar exceções internas para não derrubar o timer
            }
            await Task.CompletedTask;
        }
        #endregion

        #region Verificação e exibição de notificações
        // Verifica itens com vencimento próximo e exibe balão de notificação
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

                    // Prepara a mensagem com até 6 linhas
                    var lines = due.Take(6).Select(i => $"{i.Numero} - {i.Fornecedor} - {i.Vencimento:dd/MM} - {i.Valor:C2}");
                    string text = string.Join("\n", lines);
                    if (due.Count > 6) text += $"\n... e mais {due.Count - 6} itens";

                    // Exibe balão na thread da UI se possível
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
                    // ignorar falhas de verificação
                }
            }
        }
        #endregion

        #region Stop / Dispose
        // Para cronômetro e limpa ícone de notificação
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
        #endregion
    }
}
