using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Quitta.Services
{
    internal sealed class BackupManager : IDisposable
    {
        #region Singleton
        // Instância única (lazy) do gerenciador
        private static readonly Lazy<BackupManager> _instance = new(() => new BackupManager());
        public static BackupManager Instance => _instance.Value;
        #endregion

        #region Campos privados / estado
        private System.Threading.Timer? _timer;
        private readonly object _lock = new();
        private bool _running = false;
        #endregion

        #region Construtor
        // Construtor privado para singleton
        private BackupManager() { }
        #endregion

        #region Inicialização a partir das configurações
        // Inicializa ou reinicia o agendamento com base em Properties.Settings
        public void InitializeFromSettings()
        {
            Restart();
        }

        // Reinicia o agendamento conforme configuração atual
        public void Restart()
        {
            Stop();

            if (!Properties.Settings.Default.AutoBackup)
                return;

            var frequency = Properties.Settings.Default.BackupFrequency ?? "Semanal";
            TimeSpan due = GetTimeUntilNext(frequency);

            // cria timer que executa apenas uma vez; após execução agendamos a próxima
            _timer = new System.Threading.Timer(async _ => await TimerTick(), null, due, Timeout.InfiniteTimeSpan);
            _running = true;
        }
        #endregion

        #region Agendamento e execução periódica
        // Tick do timer que executa o backup e agenda o próximo
        private async Task TimerTick()
        {
            try
            {
                await PerformBackupAsync();
            }
            catch
            {
                // não propagar exceções do agendador
            }
            finally
            {
                ScheduleNext();
            }
        }

        // Agenda a próxima execução considerando as configurações ativas
        private void ScheduleNext()
        {
            lock (_lock)
            {
                if (_timer == null) return;
                if (!Properties.Settings.Default.AutoBackup)
                {
                    _timer?.Dispose();
                    _timer = null;
                    _running = false;
                    return;
                }

                var frequency = Properties.Settings.Default.BackupFrequency ?? "Semanal";
                TimeSpan next = GetTimeUntilNext(frequency);
                _timer.Change(next, Timeout.InfiniteTimeSpan);
            }
        }
        #endregion

        #region Helpers de cálculo de próxima execução
        // Calcula quanto tempo falta até a próxima execução baseada na string de frequência
        private TimeSpan GetTimeUntilNext(string frequency)
        {
            var now = DateTime.Now;
            return frequency switch
            {
                "Diário" => (now.Date.AddDays(1) - now).Add(TimeSpan.FromHours(1)), // próximo dia + 1h
                "Semanal" => GetNextWeekday(now, DayOfWeek.Sunday).Subtract(now),
                "Mensal" => GetNextMonth(now).Subtract(now),
                _ => TimeSpan.FromDays(7),
            };
        }

        // Retorna DateTime do próximo dia da semana especificado (usa horário 2h)
        private static DateTime GetNextWeekday(DateTime from, DayOfWeek day)
        {
            int start = (int)from.DayOfWeek;
            int target = (int)day;
            int daysToAdd = ((target - start) + 7) % 7;
            if (daysToAdd == 0) daysToAdd = 7; // próxima semana
            return from.Date.AddDays(daysToAdd).AddHours(2); // agendar às 2h
        }

        // Retorna DateTime do início do próximo mês (usa horário 3h)
        private static DateTime GetNextMonth(DateTime from)
        {
            var firstOfNextMonth = new DateTime(from.Year, from.Month, 1).AddMonths(1);
            return firstOfNextMonth.AddHours(3); // 3h
        }
        #endregion

        #region Execução do backup
        // Executa o backup em zip da pasta de dados (garante single-run via lock)
        public async Task PerformBackupAsync()
        {
            // Garante que apenas um backup rode por vez
            if (!Monitor.TryEnter(_lock)) return;
            try
            {
                string destBase = Properties.Settings.Default.BackupPath;
                if (string.IsNullOrWhiteSpace(destBase))
                    destBase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "QuittaBackups");

                Directory.CreateDirectory(destBase);

                // Usa DataService para localizar pasta de dados da aplicação
                var ds = new DataService();
                string source = ds.GetDataFolder();
                if (!Directory.Exists(source))
                {
                    // Garantir existência
                    Directory.CreateDirectory(source);
                }

                string fileName = $"backup_{DateTime.Now:yyyyMMdd_HHmmss}.zip";
                string dest = Path.Combine(destBase, fileName);

                // Criar zip em arquivo temporário e mover para evitar arquivos parciais
                string temp = dest + ".tmp";
                if (File.Exists(temp)) File.Delete(temp);

                ZipFile.CreateFromDirectory(source, temp, CompressionLevel.Optimal, true);
                File.Move(temp, dest);

                // Atualiza configurações com timestamp do último backup
                Properties.Settings.Default.LastBackupUtc = DateTime.UtcNow;
                Properties.Settings.Default.Save();

                // Limpeza de backups antigos
                CleanupOldBackups(destBase, Properties.Settings.Default.KeepLastBackups);
            }
            finally
            {
                Monitor.Exit(_lock);
            }

            await Task.CompletedTask;
        }
        #endregion

        #region Cancelamento / Stop
        public void Stop()
        {
            lock (_lock)
            {
                _timer?.Dispose();
                _timer = null;
                _running = false;
            }
        }
        #endregion

        #region Limpeza de backups antigos
        private void CleanupOldBackups(string folder, int keep)
        {
            try
            {
                var files = Directory.GetFiles(folder, "backup_*.zip")
                    .Select(f => new FileInfo(f))
                    .OrderByDescending(fi => fi.CreationTimeUtc)
                    .ToList();

                for (int i = keep; i < files.Count; i++)
                {
                    try { files[i].Delete(); } catch { }
                }
            }
            catch
            {
            }
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            Stop();
        }
        #endregion
    }
}
