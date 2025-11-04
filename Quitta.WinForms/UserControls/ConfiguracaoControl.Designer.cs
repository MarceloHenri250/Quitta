namespace Quitta.UserControls
{
    partial class ConfiguracaoControl
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controles principais
        private System.Windows.Forms.TabControl tabControl;

        // Backup
        private System.Windows.Forms.TabPage tabBackup;
        private System.Windows.Forms.CheckBox chkAutoBackup;
        private System.Windows.Forms.TextBox txtBackupPath;
        private System.Windows.Forms.Button btnChooseBackupPath;
        private System.Windows.Forms.ComboBox cmbBackupFrequency;
        private System.Windows.Forms.NumericUpDown nudKeepLastBackups;
        private System.Windows.Forms.Label lblLastBackup;
        private System.Windows.Forms.Button btnBackupNow;

        // Budget
        private System.Windows.Forms.TabPage tabBudget;
        private System.Windows.Forms.RadioButton rdoCurrentYear;
        private System.Windows.Forms.RadioButton rdoCurrentPlusNext;
        private System.Windows.Forms.RadioButton rdoLast2Years;
        private System.Windows.Forms.RadioButton rdoLast3Years;
        private System.Windows.Forms.RadioButton rdoCustomYears;
        private System.Windows.Forms.NumericUpDown nudCustomStartYear;
        private System.Windows.Forms.NumericUpDown nudCustomEndYear;

        // Reports
        private System.Windows.Forms.TabPage tabReports;
        private System.Windows.Forms.Label lblReportsSoon;

        // Notifications
        private System.Windows.Forms.TabPage tabNotifications;
        private System.Windows.Forms.CheckBox chkEnableNotifications;
        private System.Windows.Forms.NumericUpDown nudNotificationAdvanceDays;
        private System.Windows.Forms.CheckBox chkNotifyDesktop;

        // Bottom buttons
        private System.Windows.Forms.Button btnSaveAll;
        private System.Windows.Forms.Button btnRestoreDefaults;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl = new TabControl();
            tabBackup = new TabPage();
            chkAutoBackup = new CheckBox();
            lblPath = new Label();
            txtBackupPath = new TextBox();
            btnChooseBackupPath = new Button();
            lblFreq = new Label();
            cmbBackupFrequency = new ComboBox();
            lblKeep = new Label();
            nudKeepLastBackups = new NumericUpDown();
            lblLastBackup = new Label();
            btnBackupNow = new Button();
            tabBudget = new TabPage();
            lblBudget = new Label();
            rdoCurrentYear = new RadioButton();
            rdoCurrentPlusNext = new RadioButton();
            rdoLast2Years = new RadioButton();
            rdoLast3Years = new RadioButton();
            rdoCustomYears = new RadioButton();
            lblStart = new Label();
            nudCustomStartYear = new NumericUpDown();
            lblEnd = new Label();
            nudCustomEndYear = new NumericUpDown();
            tabReports = new TabPage();
            lblReportsSoon = new Label();
            tabNotifications = new TabPage();
            chkEnableNotifications = new CheckBox();
            lblAdv = new Label();
            nudNotificationAdvanceDays = new NumericUpDown();
            chkNotifyDesktop = new CheckBox();
            btnSaveAll = new Button();
            btnRestoreDefaults = new Button();
            tabControl.SuspendLayout();
            tabBackup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudKeepLastBackups).BeginInit();
            tabBudget.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCustomStartYear).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCustomEndYear).BeginInit();
            tabReports.SuspendLayout();
            tabNotifications.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudNotificationAdvanceDays).BeginInit();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabBackup);
            tabControl.Controls.Add(tabBudget);
            tabControl.Controls.Add(tabReports);
            tabControl.Controls.Add(tabNotifications);
            tabControl.Location = new Point(10, 10);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(680, 440);
            tabControl.TabIndex = 0;
            // 
            // tabBackup
            // 
            tabBackup.Controls.Add(chkAutoBackup);
            tabBackup.Controls.Add(lblPath);
            tabBackup.Controls.Add(txtBackupPath);
            tabBackup.Controls.Add(btnChooseBackupPath);
            tabBackup.Controls.Add(lblFreq);
            tabBackup.Controls.Add(cmbBackupFrequency);
            tabBackup.Controls.Add(lblKeep);
            tabBackup.Controls.Add(nudKeepLastBackups);
            tabBackup.Controls.Add(lblLastBackup);
            tabBackup.Controls.Add(btnBackupNow);
            tabBackup.Location = new Point(4, 29);
            tabBackup.Name = "tabBackup";
            tabBackup.Size = new Size(672, 407);
            tabBackup.TabIndex = 0;
            tabBackup.Text = "Backup";
            // 
            // chkAutoBackup
            // 
            chkAutoBackup.AutoSize = true;
            chkAutoBackup.Location = new Point(15, 15);
            chkAutoBackup.Name = "chkAutoBackup";
            chkAutoBackup.Size = new Size(204, 24);
            chkAutoBackup.TabIndex = 0;
            chkAutoBackup.Text = "Ativar Backup Automático";
            // 
            // lblPath
            // 
            lblPath.AutoSize = true;
            lblPath.Location = new Point(15, 50);
            lblPath.Name = "lblPath";
            lblPath.Size = new Size(145, 20);
            lblPath.TabIndex = 1;
            lblPath.Text = "Caminho do Backup:";
            // 
            // txtBackupPath
            // 
            txtBackupPath.Location = new Point(15, 70);
            txtBackupPath.Name = "txtBackupPath";
            txtBackupPath.Size = new Size(520, 27);
            txtBackupPath.TabIndex = 2;
            // 
            // btnChooseBackupPath
            // 
            btnChooseBackupPath.Location = new Point(545, 68);
            btnChooseBackupPath.Name = "btnChooseBackupPath";
            btnChooseBackupPath.Size = new Size(100, 29);
            btnChooseBackupPath.TabIndex = 3;
            btnChooseBackupPath.Text = "Escolher";
            btnChooseBackupPath.Click += BtnChooseBackupPath_Click;
            // 
            // lblFreq
            // 
            lblFreq.AutoSize = true;
            lblFreq.Location = new Point(15, 110);
            lblFreq.Name = "lblFreq";
            lblFreq.Size = new Size(84, 20);
            lblFreq.TabIndex = 4;
            lblFreq.Text = "Frequência:";
            // 
            // cmbBackupFrequency
            // 
            cmbBackupFrequency.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBackupFrequency.Items.AddRange(new object[] { "Diário", "Semanal", "Mensal", "Desligado" });
            cmbBackupFrequency.Location = new Point(105, 106);
            cmbBackupFrequency.Name = "cmbBackupFrequency";
            cmbBackupFrequency.Size = new Size(120, 28);
            cmbBackupFrequency.TabIndex = 5;
            // 
            // lblKeep
            // 
            lblKeep.AutoSize = true;
            lblKeep.Location = new Point(240, 110);
            lblKeep.Name = "lblKeep";
            lblKeep.Size = new Size(172, 20);
            lblKeep.TabIndex = 6;
            lblKeep.Text = "Manter Últimos Backups:";
            // 
            // nudKeepLastBackups
            // 
            nudKeepLastBackups.Location = new Point(418, 107);
            nudKeepLastBackups.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudKeepLastBackups.Name = "nudKeepLastBackups";
            nudKeepLastBackups.Size = new Size(60, 27);
            nudKeepLastBackups.TabIndex = 7;
            nudKeepLastBackups.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // lblLastBackup
            // 
            lblLastBackup.AutoSize = true;
            lblLastBackup.Location = new Point(15, 150);
            lblLastBackup.Name = "lblLastBackup";
            lblLastBackup.Size = new Size(128, 20);
            lblLastBackup.TabIndex = 8;
            lblLastBackup.Text = "Último backup: —";
            // 
            // btnBackupNow
            // 
            btnBackupNow.Location = new Point(15, 185);
            btnBackupNow.Name = "btnBackupNow";
            btnBackupNow.Size = new Size(220, 30);
            btnBackupNow.TabIndex = 9;
            btnBackupNow.Text = "Fazer Backup Manual Agora";
            btnBackupNow.Click += BtnBackupNow_Click;
            // 
            // tabBudget
            // 
            tabBudget.Controls.Add(lblBudget);
            tabBudget.Controls.Add(rdoCurrentYear);
            tabBudget.Controls.Add(rdoCurrentPlusNext);
            tabBudget.Controls.Add(rdoLast2Years);
            tabBudget.Controls.Add(rdoLast3Years);
            tabBudget.Controls.Add(rdoCustomYears);
            tabBudget.Controls.Add(lblStart);
            tabBudget.Controls.Add(nudCustomStartYear);
            tabBudget.Controls.Add(lblEnd);
            tabBudget.Controls.Add(nudCustomEndYear);
            tabBudget.Location = new Point(4, 29);
            tabBudget.Name = "tabBudget";
            tabBudget.Size = new Size(672, 407);
            tabBudget.TabIndex = 1;
            tabBudget.Text = "Budget Anual";
            // 
            // lblBudget
            // 
            lblBudget.AutoSize = true;
            lblBudget.Location = new Point(15, 15);
            lblBudget.Name = "lblBudget";
            lblBudget.Size = new Size(207, 20);
            lblBudget.TabIndex = 0;
            lblBudget.Text = "Visualização do Budget Anual";
            // 
            // rdoCurrentYear
            // 
            rdoCurrentYear.AutoSize = true;
            rdoCurrentYear.Location = new Point(15, 45);
            rdoCurrentYear.Name = "rdoCurrentYear";
            rdoCurrentYear.Size = new Size(96, 24);
            rdoCurrentYear.TabIndex = 1;
            rdoCurrentYear.Text = "Ano Atual";
            // 
            // rdoCurrentPlusNext
            // 
            rdoCurrentPlusNext.AutoSize = true;
            rdoCurrentPlusNext.Location = new Point(15, 70);
            rdoCurrentPlusNext.Name = "rdoCurrentPlusNext";
            rdoCurrentPlusNext.Size = new Size(169, 24);
            rdoCurrentPlusNext.TabIndex = 2;
            rdoCurrentPlusNext.Text = "Ano Atual + Próximo";
            // 
            // rdoLast2Years
            // 
            rdoLast2Years.AutoSize = true;
            rdoLast2Years.Location = new Point(15, 95);
            rdoLast2Years.Name = "rdoLast2Years";
            rdoLast2Years.Size = new Size(130, 24);
            rdoLast2Years.TabIndex = 3;
            rdoLast2Years.Text = "Últimos 2 Anos";
            // 
            // rdoLast3Years
            // 
            rdoLast3Years.AutoSize = true;
            rdoLast3Years.Location = new Point(15, 120);
            rdoLast3Years.Name = "rdoLast3Years";
            rdoLast3Years.Size = new Size(130, 24);
            rdoLast3Years.TabIndex = 4;
            rdoLast3Years.Text = "Últimos 3 Anos";
            // 
            // rdoCustomYears
            // 
            rdoCustomYears.AutoSize = true;
            rdoCustomYears.Location = new Point(15, 145);
            rdoCustomYears.Name = "rdoCustomYears";
            rdoCustomYears.Size = new Size(122, 24);
            rdoCustomYears.TabIndex = 5;
            rdoCustomYears.Text = "Personalizado";
            rdoCustomYears.CheckedChanged += RdoCustomYears_CheckedChanged;
            // 
            // lblStart
            // 
            lblStart.AutoSize = true;
            lblStart.Location = new Point(35, 175);
            lblStart.Name = "lblStart";
            lblStart.Size = new Size(82, 20);
            lblStart.TabIndex = 6;
            lblStart.Text = "Ano Inicial:";
            // 
            // nudCustomStartYear
            // 
            nudCustomStartYear.Location = new Point(123, 172);
            nudCustomStartYear.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            nudCustomStartYear.Minimum = new decimal(new int[] { 1900, 0, 0, 0 });
            nudCustomStartYear.Name = "nudCustomStartYear";
            nudCustomStartYear.Size = new Size(80, 27);
            nudCustomStartYear.TabIndex = 7;
            nudCustomStartYear.Value = new decimal(new int[] { 2025, 0, 0, 0 });
            // 
            // lblEnd
            // 
            lblEnd.AutoSize = true;
            lblEnd.Location = new Point(209, 175);
            lblEnd.Name = "lblEnd";
            lblEnd.Size = new Size(74, 20);
            lblEnd.TabIndex = 8;
            lblEnd.Text = "Ano Final:";
            // 
            // nudCustomEndYear
            // 
            nudCustomEndYear.Location = new Point(289, 173);
            nudCustomEndYear.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            nudCustomEndYear.Minimum = new decimal(new int[] { 1900, 0, 0, 0 });
            nudCustomEndYear.Name = "nudCustomEndYear";
            nudCustomEndYear.Size = new Size(80, 27);
            nudCustomEndYear.TabIndex = 9;
            nudCustomEndYear.Value = new decimal(new int[] { 2025, 0, 0, 0 });
            // 
            // tabReports
            // 
            tabReports.Controls.Add(lblReportsSoon);
            tabReports.Enabled = false;
            tabReports.Location = new Point(4, 29);
            tabReports.Name = "tabReports";
            tabReports.Size = new Size(672, 407);
            tabReports.TabIndex = 2;
            tabReports.Text = "Relatórios";
            // 
            // lblReportsSoon
            // 
            lblReportsSoon.AutoSize = true;
            lblReportsSoon.Location = new Point(15, 15);
            lblReportsSoon.Name = "lblReportsSoon";
            lblReportsSoon.Size = new Size(80, 20);
            lblReportsSoon.TabIndex = 0;
            lblReportsSoon.Text = "Em breve...";
            // 
            // tabNotifications
            // 
            tabNotifications.Controls.Add(chkEnableNotifications);
            tabNotifications.Controls.Add(lblAdv);
            tabNotifications.Controls.Add(nudNotificationAdvanceDays);
            tabNotifications.Controls.Add(chkNotifyDesktop);
            tabNotifications.Location = new Point(4, 29);
            tabNotifications.Name = "tabNotifications";
            tabNotifications.Size = new Size(672, 407);
            tabNotifications.TabIndex = 3;
            tabNotifications.Text = "Notificações";
            // 
            // chkEnableNotifications
            // 
            chkEnableNotifications.AutoSize = true;
            chkEnableNotifications.Location = new Point(15, 15);
            chkEnableNotifications.Name = "chkEnableNotifications";
            chkEnableNotifications.Size = new Size(157, 24);
            chkEnableNotifications.TabIndex = 0;
            chkEnableNotifications.Text = "Ativar Notificações";
            // 
            // lblAdv
            // 
            lblAdv.AutoSize = true;
            lblAdv.Location = new Point(15, 50);
            lblAdv.Name = "lblAdv";
            lblAdv.Size = new Size(156, 20);
            lblAdv.TabIndex = 1;
            lblAdv.Text = "Dias de Antecedência:";
            // 
            // nudNotificationAdvanceDays
            // 
            nudNotificationAdvanceDays.Location = new Point(186, 48);
            nudNotificationAdvanceDays.Maximum = new decimal(new int[] { 365, 0, 0, 0 });
            nudNotificationAdvanceDays.Name = "nudNotificationAdvanceDays";
            nudNotificationAdvanceDays.Size = new Size(60, 27);
            nudNotificationAdvanceDays.TabIndex = 2;
            nudNotificationAdvanceDays.Value = new decimal(new int[] { 7, 0, 0, 0 });
            // 
            // chkNotifyDesktop
            // 
            chkNotifyDesktop.AutoSize = true;
            chkNotifyDesktop.Location = new Point(15, 85);
            chkNotifyDesktop.Name = "chkNotifyDesktop";
            chkNotifyDesktop.Size = new Size(261, 24);
            chkNotifyDesktop.TabIndex = 3;
            chkNotifyDesktop.Text = "Notificações do Sistema (Desktop)";
            // 
            // btnSaveAll
            // 
            btnSaveAll.Location = new Point(440, 465);
            btnSaveAll.Name = "btnSaveAll";
            btnSaveAll.RightToLeft = RightToLeft.No;
            btnSaveAll.Size = new Size(200, 32);
            btnSaveAll.TabIndex = 1;
            btnSaveAll.Text = "Salvar Configurações";
            btnSaveAll.Click += BtnSaveAll_Click;
            // 
            // btnRestoreDefaults
            // 
            btnRestoreDefaults.Location = new Point(200, 465);
            btnRestoreDefaults.Name = "btnRestoreDefaults";
            btnRestoreDefaults.Size = new Size(150, 32);
            btnRestoreDefaults.TabIndex = 2;
            btnRestoreDefaults.Text = "Restaurar Padrões";
            btnRestoreDefaults.Click += BtnRestoreDefaults_Click;
            // 
            // ConfiguracaoControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabControl);
            Controls.Add(btnSaveAll);
            Controls.Add(btnRestoreDefaults);
            Name = "ConfiguracaoControl";
            Size = new Size(700, 520);
            Load += ConfiguracaoControl_Load;
            tabControl.ResumeLayout(false);
            tabBackup.ResumeLayout(false);
            tabBackup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudKeepLastBackups).EndInit();
            tabBudget.ResumeLayout(false);
            tabBudget.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCustomStartYear).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCustomEndYear).EndInit();
            tabReports.ResumeLayout(false);
            tabReports.PerformLayout();
            tabNotifications.ResumeLayout(false);
            tabNotifications.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudNotificationAdvanceDays).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblPath;
        private Label lblFreq;
        private Label lblKeep;
        private Label lblBudget;
        private Label lblStart;
        private Label lblEnd;
        private Label lblAdv;
    }
}
