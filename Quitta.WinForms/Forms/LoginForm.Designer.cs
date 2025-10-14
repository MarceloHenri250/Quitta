namespace Quitta.Forms
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelLogin = new Panel();
            btnEntrar = new Button();
            txtSenha = new TextBox();
            lblSenha = new Label();
            txtUsuario = new TextBox();
            lblUsuario = new Label();
            lblTitulo = new Label();
            panelLogin.SuspendLayout();
            SuspendLayout();
            // 
            // panelLogin
            // 
            panelLogin.BorderStyle = BorderStyle.FixedSingle;
            panelLogin.Controls.Add(btnEntrar);
            panelLogin.Controls.Add(txtSenha);
            panelLogin.Controls.Add(lblSenha);
            panelLogin.Controls.Add(txtUsuario);
            panelLogin.Controls.Add(lblUsuario);
            panelLogin.Controls.Add(lblTitulo);
            panelLogin.Location = new Point(50, 35);
            panelLogin.Name = "panelLogin";
            panelLogin.Size = new Size(380, 280);
            panelLogin.TabIndex = 0;
            // 
            // btnEntrar
            // 
            btnEntrar.BackColor = Color.FromArgb(3, 2, 19);
            btnEntrar.FlatStyle = FlatStyle.Flat;
            btnEntrar.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEntrar.ForeColor = Color.White;
            btnEntrar.Location = new Point(40, 220);
            btnEntrar.Name = "btnEntrar";
            btnEntrar.Size = new Size(300, 40);
            btnEntrar.TabIndex = 2;
            btnEntrar.Text = "Entrar";
            btnEntrar.UseVisualStyleBackColor = false;
            btnEntrar.Click += btnEntrar_Click;
            // 
            // txtSenha
            // 
            txtSenha.Font = new Font("Segoe UI", 10F);
            txtSenha.Location = new Point(40, 170);
            txtSenha.Name = "txtSenha";
            txtSenha.PasswordChar = '*';
            txtSenha.Size = new Size(300, 30);
            txtSenha.TabIndex = 1;
            txtSenha.KeyDown += TextBox_KeyDown_Submit;
            // 
            // lblSenha
            // 
            lblSenha.AutoSize = true;
            lblSenha.Font = new Font("Segoe UI", 10F);
            lblSenha.Location = new Point(40, 145);
            lblSenha.Name = "lblSenha";
            lblSenha.Size = new Size(61, 23);
            lblSenha.TabIndex = 3;
            lblSenha.Text = "Senha:";
            // 
            // txtUsuario
            // 
            txtUsuario.Font = new Font("Segoe UI", 10F);
            txtUsuario.Location = new Point(40, 105);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(300, 30);
            txtUsuario.TabIndex = 0;
            txtUsuario.KeyDown += TextBox_KeyDown_Submit;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI", 10F);
            lblUsuario.Location = new Point(40, 80);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(72, 23);
            lblUsuario.TabIndex = 4;
            lblUsuario.Text = "Usuário:";
            // 
            // lblTitulo
            // 
            lblTitulo.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(20, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(340, 41);
            lblTitulo.TabIndex = 5;
            lblTitulo.Text = "Quitta";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(476, 343);
            Controls.Add(panelLogin);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login - Quitta";
            panelLogin.ResumeLayout(false);
            panelLogin.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelLogin;
        private Label lblUsuario;
        private Label lblTitulo;
        private TextBox txtSenha;
        private Label lblSenha;
        private TextBox txtUsuario;
        private Button btnEntrar;
    }
}