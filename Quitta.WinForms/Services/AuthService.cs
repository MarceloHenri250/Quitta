using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitta.Services
{
    /// <summary>
    /// Serviço simples de autenticação para o sistema Quitta.
    /// Observação: atualmente usa uma verificação local/placeholder; substituir por integração real quando necessário.
    /// </summary>
    public class AuthService
    {
        public AuthService()
        {
            // Pode-se inicializar conexões, leitores de configuração, ou dependências aqui
        }

        /// <summary>
        /// Autentica usuário/senha.
        /// Retorna true se as credenciais estiverem corretas.
        /// Implementação atual: placeholder (admin/admin123).
        /// </summary>
        public bool Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return false;

            // Placeholder: substituir por verificação real (banco, API, etc.)
            return username == "admin" && password == "admin123";
        }
    }
}
