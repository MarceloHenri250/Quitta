using Quitta.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitta.Forms
{
    /// <summary>
    /// Formulário de Login da aplicação Quitta.
    /// Responsabilidades:
    /// - Receber usuário e senha
    /// - Validar campos básicos
    /// - Acionar autenticação via AuthService
    /// - Permitir envio com a tecla Enter
    /// </summary>

    public partial class LoginForm : Form
    {
        private readonly AuthService authService;

        public LoginForm() : this(new AuthService())
        {
        }

        // Construtor para permitir injeção de dependência em testes
        public LoginForm(AuthService authService)
        {
            InitializeComponent();
            this.authService = authService ?? throw new ArgumentNullException(nameof(authService));

            // Observação: os handlers de KeyDown são ligados no Designer
        }

        /// <summary>
        /// Tratador para a tecla Enter nos textboxes do formulário.
        /// Quando Enter for pressionado, o botão Entrar é acionado.
        /// </summary>
        private void TextBox_KeyDown_Submit(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // evita o som padrão do sistema
                btnEntrar.PerformClick();
            }
        }

        /// <summary>
        /// Clique do botão Entrar. Faz validações simples e usa AuthService
        /// para validar credenciais.
        /// </summary>
        private void btnEntrar_Click(object sender, EventArgs e)
        {
            // Validação básica dos campos
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("Por favor, digite o usuário.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MessageBox.Show("Por favor, digite a senha.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSenha.Focus();
                return;
            }

            // Usa o serviço de autenticação
            if (authService.Authenticate(txtUsuario.Text.Trim(), txtSenha.Text))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            MessageBox.Show("Usuário ou senha incorretos.", "Erro de Login",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtSenha.Clear();
            txtSenha.Focus();
        }
    }
}
