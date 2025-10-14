using Quitta.Forms;

namespace Quitta
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Mostrar tela de login primeiro
            var loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // Se login OK, mostrar o MainForm
                Application.Run(new MainForm());
            }
        }
    }
}