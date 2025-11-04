using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitta.UserControls
{
    public partial class ConfiguracaoControl : UserControl
    {
        public ConfiguracaoControl()
        {
            InitializeComponent();
        }

        private void ConfiguracaoControl_Load(object sender, EventArgs e)
        {
            // Inicializar combos e valores
            cmbBackupFrequency.SelectedIndex = 1; // Semanal como padrão
            LoadSettingsToControls();
            UpdateLastBackupLabel();
            ToggleCustomYearsControls();

            // Inicializa gerenciador de backup
            Quitta.Services.BackupManager.Instance.InitializeFromSettings();
        }

        private void LoadSettingsToControls()
        {
            try
            {
                // Backup
                chkAutoBackup.Checked = Properties.Settings.Default.AutoBackup;
                txtBackupPath.Text = Properties.Settings.Default.BackupPath ?? string.Empty;
                var freq = Properties.Settings.Default.BackupFrequency ?? "Semanal";
                if (cmbBackupFrequency.Items.Contains(freq))
                    cmbBackupFrequency.SelectedItem = freq;
                nudKeepLastBackups.Value = Math.Max(nudKeepLastBackups.Minimum, Math.Min(nudKeepLastBackups.Maximum, Properties.Settings.Default.KeepLastBackups));

                // Budget
                var budgetMode = Properties.Settings.Default.BudgetMode ?? "CurrentYear";
                switch (budgetMode)
                {
                    case "CurrentYear": rdoCurrentYear.Checked = true; break;
                    case "CurrentPlusNext": rdoCurrentPlusNext.Checked = true; break;
                    case "Last2": rdoLast2Years.Checked = true; break;
                    case "Last3": rdoLast3Years.Checked = true; break;
                    case "Custom": rdoCustomYears.Checked = true; break;
                }
                nudCustomStartYear.Value = Properties.Settings.Default.BudgetCustomStartYear == 0 ? nudCustomStartYear.Value : Properties.Settings.Default.BudgetCustomStartYear;
                nudCustomEndYear.Value = Properties.Settings.Default.BudgetCustomEndYear == 0 ? nudCustomEndYear.Value : Properties.Settings.Default.BudgetCustomEndYear;

                // Notifications
                chkEnableNotifications.Checked = Properties.Settings.Default.EnableNotifications;
                nudNotificationAdvanceDays.Value = Math.Max(nudNotificationAdvanceDays.Minimum, Math.Min(nudNotificationAdvanceDays.Maximum, Properties.Settings.Default.NotificationAdvanceDays));
                chkNotifyDesktop.Checked = Properties.Settings.Default.NotifyDesktop;
            }
            catch
            {
                // ignorar
            }
        }

        private void SaveControlsToSettings()
        {
            Properties.Settings.Default.AutoBackup = chkAutoBackup.Checked;
            Properties.Settings.Default.BackupPath = txtBackupPath.Text ?? string.Empty;
            Properties.Settings.Default.BackupFrequency = cmbBackupFrequency.SelectedItem?.ToString() ?? "Semanal";
            Properties.Settings.Default.KeepLastBackups = (int)nudKeepLastBackups.Value;

            string mode = "CurrentYear";
            if (rdoCurrentYear.Checked) mode = "CurrentYear";
            if (rdoCurrentPlusNext.Checked) mode = "CurrentPlusNext";
            if (rdoLast2Years.Checked) mode = "Last2";
            if (rdoLast3Years.Checked) mode = "Last3";
            if (rdoCustomYears.Checked) mode = "Custom";
            Properties.Settings.Default.BudgetMode = mode;
            Properties.Settings.Default.BudgetCustomStartYear = (int)nudCustomStartYear.Value;
            Properties.Settings.Default.BudgetCustomEndYear = (int)nudCustomEndYear.Value;

            Properties.Settings.Default.EnableNotifications = chkEnableNotifications.Checked;
            Properties.Settings.Default.NotificationAdvanceDays = (int)nudNotificationAdvanceDays.Value;
            Properties.Settings.Default.NotifyDesktop = chkNotifyDesktop.Checked;

            Properties.Settings.Default.Save();

            // Reiniciar serviço de backup
            Quitta.Services.BackupManager.Instance.Restart();

            // Restart notification manager to apply new settings
            Quitta.Services.NotificationManager.Instance.InitializeFromSettings();

            // Notify main form to reload data so budget settings take effect immediately
            try
            {
                var main = Application.OpenForms.OfType<Quitta.Forms.MainForm>().FirstOrDefault();
                main?.LoadData();
            }
            catch
            {
                // ignore
            }
        }

        private async void BtnChooseBackupPath_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Escolha a pasta para salvar backups";
                if (Directory.Exists(txtBackupPath.Text))
                    dlg.SelectedPath = txtBackupPath.Text;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtBackupPath.Text = dlg.SelectedPath;
                }
            }
        }

        private async void BtnBackupNow_Click(object sender, EventArgs e)
        {
            try
            {
                await Quitta.Services.BackupManager.Instance.PerformBackupAsync();
                UpdateLastBackupLabel();
                MessageBox.Show("Backup concluído.", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao executar backup: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateLastBackupLabel()
        {
            var dt = Properties.Settings.Default.LastBackupUtc;
            if (dt == DateTime.MinValue)
            {
                lblLastBackup.Text = "Último backup: —";
            }
            else
            {
                lblLastBackup.Text = $"Último backup: {dt.ToLocalTime():dd/MM/yyyy HH:mm}";
            }
        }

        private void RdoCustomYears_CheckedChanged(object sender, EventArgs e)
        {
            ToggleCustomYearsControls();
        }

        private void ToggleCustomYearsControls()
        {
            bool enable = rdoCustomYears.Checked;
            nudCustomStartYear.Enabled = enable;
            nudCustomEndYear.Enabled = enable;
        }

        private void BtnSaveAll_Click(object sender, EventArgs e)
        {
            SaveControlsToSettings();
            MessageBox.Show("Configurações salvas.", "Salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnRestoreDefaults_Click(object sender, EventArgs e)
        {
            // Restaurar padrões simples
            Properties.Settings.Default.Reset();
            LoadSettingsToControls();
            UpdateLastBackupLabel();
            Quitta.Services.BackupManager.Instance.Restart();
            // Restart notification manager
            Quitta.Services.NotificationManager.Instance.InitializeFromSettings();
            MessageBox.Show("Padrões restaurados.", "Restaurar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
