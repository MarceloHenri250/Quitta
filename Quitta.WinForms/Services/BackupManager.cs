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
        private static readonly Lazy<BackupManager> _instance = new(() => new BackupManager());
        public static BackupManager Instance => _instance.Value;

        private System.Threading.Timer? _timer;
        private readonly object _lock = new();
        private bool _running = false;

        private BackupManager() { }

        public void InitializeFromSettings()
        {
            Restart();
        }

        public void Restart()
        {
            Stop();

            if (!Properties.Settings.Default.AutoBackup)
                return;

            var frequency = Properties.Settings.Default.BackupFrequency ?? "Semanal";
            TimeSpan due = GetTimeUntilNext(frequency);

            // create timer
            _timer = new System.Threading.Timer(async _ => await TimerTick(), null, due, Timeout.InfiniteTimeSpan);
            _running = true;
        }

        private async Task TimerTick()
        {
            try
            {
                await PerformBackupAsync();
            }
            catch
            {
                // swallow
            }
            finally
            {
                ScheduleNext();
            }
        }

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

        private TimeSpan GetTimeUntilNext(string frequency)
        {
            var now = DateTime.Now;
            return frequency switch
            {
                "Diário" => (now.Date.AddDays(1) - now).Add(TimeSpan.FromHours(1)), // next day + 1h
                "Semanal" => GetNextWeekday(now, DayOfWeek.Sunday).Subtract(now),
                "Mensal" => GetNextMonth(now).Subtract(now),
                _ => TimeSpan.FromDays(7),
            };
        }

        private static DateTime GetNextWeekday(DateTime from, DayOfWeek day)
        {
            int start = (int)from.DayOfWeek;
            int target = (int)day;
            int daysToAdd = ((target - start) + 7) % 7;
            if (daysToAdd == 0) daysToAdd = 7; // next week
            return from.Date.AddDays(daysToAdd).AddHours(2); // schedule at 2 AM
        }

        private static DateTime GetNextMonth(DateTime from)
        {
            var firstOfNextMonth = new DateTime(from.Year, from.Month, 1).AddMonths(1);
            return firstOfNextMonth.AddHours(3); // 3 AM
        }

        public async Task PerformBackupAsync()
        {
            // Ensure only one backup runs at a time
            if (!Monitor.TryEnter(_lock)) return;
            try
            {
                string destBase = Properties.Settings.Default.BackupPath;
                if (string.IsNullOrWhiteSpace(destBase))
                    destBase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "QuittaBackups");

                Directory.CreateDirectory(destBase);

                // Use DataService's data folder as source (real app data)
                var ds = new DataService();
                string source = ds.GetDataFolder();
                if (!Directory.Exists(source))
                {
                    // Ensure created by DataService constructor, but double-check
                    Directory.CreateDirectory(source);
                }

                string fileName = $"backup_{DateTime.Now:yyyyMMdd_HHmmss}.zip";
                string dest = Path.Combine(destBase, fileName);

                // Create zip using a temp file to avoid partial files
                string temp = dest + ".tmp";
                if (File.Exists(temp)) File.Delete(temp);

                ZipFile.CreateFromDirectory(source, temp, CompressionLevel.Optimal, true);
                File.Move(temp, dest);

                // Update settings
                Properties.Settings.Default.LastBackupUtc = DateTime.UtcNow;
                Properties.Settings.Default.Save();

                // Cleanup old backups
                CleanupOldBackups(destBase, Properties.Settings.Default.KeepLastBackups);
            }
            finally
            {
                Monitor.Exit(_lock);
            }

            await Task.CompletedTask;
        }

        public void Stop()
        {
            lock (_lock)
            {
                _timer?.Dispose();
                _timer = null;
                _running = false;
            }
        }

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

        public void Dispose()
        {
            Stop();
        }
    }
}
