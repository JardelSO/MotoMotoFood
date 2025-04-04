using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MotoMotoFood.Models;
using MotoMotoFood.Repositorios;

namespace MotoMotoFood.Services
{
    public static class AutenticacaoService
    {
        public static Usuario? LoginCliente(string email, string senha)
        {
            Usuario? user = BancoDeDadosFake.Usuarios.FirstOrDefault(c => c.Email == email && c.Senha == senha);
            return user;
        }

        public static bool EmailUsuarioExiste(string email)
        {
            return BancoDeDadosFake.Usuarios.Any(c => c.Email == email);
        }
    }
}
