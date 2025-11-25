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
    public partial class LoginForm : Form
    {
        #region Campos privados
        // Serviço de autenticação (pode ser injetado para facilitar testes)
        private readonly AuthService authService;
        #endregion

        #region Construtores
        // Construtor padrão que cria um AuthService interno
        public LoginForm() : this(new AuthService())
        {
        }

        // Construtor que permite injeção de dependência (util para testes)
        public LoginForm(AuthService authService)
        {
            InitializeComponent();
            this.authService = authService ?? throw new ArgumentNullException(nameof(authService));

            // Observação: os handlers de KeyDown são ligados no Designer
        }
        #endregion

        #region Handlers de UI (teclado / botões)

        private void TextBox_KeyDown_Submit(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // evita o som padrão do sistema
                btnEntrar.PerformClick();
            }
        }

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
        #endregion
    }
}
