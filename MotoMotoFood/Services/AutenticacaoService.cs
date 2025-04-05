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

        private static int codigoConfirmacao;

        private static int GerarCodigoConfirmacao() => new Random().Next(1000, 9999);

        public static Usuario? LoginCliente(string email, string senha)
        {
            Usuario? user = BancoDeDadosFake.Usuarios.FirstOrDefault(c => c.Email == email && c.VerificarSenha(senha));
            //Segundo Fator de autenticação
            AutenticacaoService.ValidarCodigoEmailConfirmacao(email);
            return user;
        }

        public static bool EmailUsuarioExiste(string email)
        {
            return BancoDeDadosFake.Usuarios.Any(c => c.Email == email);
        }

        private static void EnviarCodigoConfirmacao(string email)
        {
            codigoConfirmacao = GerarCodigoConfirmacao();
            SegundoFatorService.EnviarEmailConfirmacao(email, codigoConfirmacao);
        }

        public static void ValidarCodigoEmailConfirmacao(string email)
        {
            //Segundo Fator de autenticação
            EnviarCodigoConfirmacao(email);
            int tentativas = 0;
            while (true)
            {
                if (tentativas > 3)
                {
                    EnviarCodigoConfirmacao(email);
                    Console.WriteLine("Foi enviado um novo código de confirmação!");
                }

                Console.Write("Foi enviado um código de confirmação para o seu e-mail, caso não encontre na caixa de entrada verifique na caixa de spam. Informe o código: ");
                if (int.TryParse(Console.ReadLine(), out int codigoDigitado) && codigoDigitado == codigoConfirmacao)
                {
                    break;
                }

                Console.WriteLine("Código inválido!");
                tentativas++;

            }
        }


    }
}
