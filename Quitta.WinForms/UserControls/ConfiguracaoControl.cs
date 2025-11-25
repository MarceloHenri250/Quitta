using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitta.UserControls
{
    public partial class ConfiguracaoControl : UserControl
    {
        #region Construtor / Eventos de ciclo de vida
        public ConfiguracaoControl()
        {
            InitializeComponent();
        }

        // Evento Load do controle: inicializa controles e serviços
        private void ConfiguracaoControl_Load(object sender, EventArgs e)
        {
            // Inicializar combos e valores
            cmbBackupFrequency.SelectedIndex = 1; // Semanal como padrão
            LoadSettingsToControls();
            UpdateLastBackupLabel();
            ToggleCustomYearsControls();

            // Inicializa gerenciador de backup a partir das configurações
            Quitta.Services.BackupManager.Instance.InitializeFromSettings();
        }
        #endregion

        #region Leitura de configurações -> UI
        // Carrega as configurações salvas para os controles da UI
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

                // Budget (orçamento)
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

                // Notifications (notificações)
                chkEnableNotifications.Checked = Properties.Settings.Default.EnableNotifications;
                nudNotificationAdvanceDays.Value = Math.Max(nudNotificationAdvanceDays.Minimum, Math.Min(nudNotificationAdvanceDays.Maximum, Properties.Settings.Default.NotificationAdvanceDays));
                chkNotifyDesktop.Checked = Properties.Settings.Default.NotifyDesktop;
            }
            catch
            {
                // ignorar erros na leitura das configurações para não travar a UI
            }
        }
        #endregion

        #region Gravação de configurações (UI -> Settings)
        // Persiste as configurações selecionadas nos controles para Properties.Settings
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

            // Reiniciar serviço de backup para aplicar novas configurações
            Quitta.Services.BackupManager.Instance.Restart();

            // Reiniciar gerenciador de notificações para aplicar novas configurações
            Quitta.Services.NotificationManager.Instance.InitializeFromSettings();

            // Notificar MainForm para recarregar dados (caso necessário para orçamento)
            try
            {
                var main = Application.OpenForms.OfType<Quitta.Forms.MainForm>().FirstOrDefault();
                main?.LoadData();
            }
            catch
            {
                // ignorar erros ao notificar MainForm
            }
        }
        #endregion

        #region Ações de Backup (selecionar pasta / executar agora)
        // Abrir diálogo para escolher pasta de backup
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

        // Executa backup imediato (assíncrono) e atualiza label
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
        #endregion

        #region Helpers de UI
        // Atualiza label que mostra a hora do último backup realizado
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

        // Handler para alternar controles de ano customizado quando opção for marcada
        private void RdoCustomYears_CheckedChanged(object sender, EventArgs e)
        {
            ToggleCustomYearsControls();
        }

        // Ativa/desativa os nud referentes ao intervalo customizado de anos
        private void ToggleCustomYearsControls()
        {
            bool enable = rdoCustomYears.Checked;
            nudCustomStartYear.Enabled = enable;
            nudCustomEndYear.Enabled = enable;
        }

        // Salvar todas as configurações quando usuário clicar em Salvar
        private void BtnSaveAll_Click(object sender, EventArgs e)
        {
            SaveControlsToSettings();
            MessageBox.Show("Configurações salvas.", "Salvar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Restaurar padrões (mantendo algumas configurações de backup preservadas)
        private void BtnRestoreDefaults_Click(object sender, EventArgs e)
        {
            // preservar configurações de backup para não sobrescrever pasta/arquivos do usuário
            var preservedBackupPath = Properties.Settings.Default.BackupPath;
            var preservedKeepLast = Properties.Settings.Default.KeepLastBackups;

            // Restaurar padrões simples
            Properties.Settings.Default.Reset();

            // Restaurar valores de backup preservados
            Properties.Settings.Default.BackupPath = preservedBackupPath;
            Properties.Settings.Default.KeepLastBackups = preservedKeepLast;

            // Garantir que as configurações foram salvas e atualizar UI
            Properties.Settings.Default.Save();

            LoadSettingsToControls();
            UpdateLastBackupLabel();

            // Reiniciar serviços para aplicar padrões
            Quitta.Services.BackupManager.Instance.Restart();
            Quitta.Services.NotificationManager.Instance.InitializeFromSettings();

            MessageBox.Show("Padrões restaurados (configurações de backup preservadas).", "Restaurar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
    }
}
