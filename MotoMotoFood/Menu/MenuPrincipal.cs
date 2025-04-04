using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MotoMotoFood.Services;
using MotoMotoFood.Models;
using MotoMotoFood.Util;
using MotoMotoFood.Menu;

namespace MotoMotoFood.Menus
{
    public static class MenuPrincipal
    {
        public static async Task ExibirAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Moto Moto Food ===");
                Console.WriteLine("1 - Login");
                Console.WriteLine("2 - Cadastrar");
                Console.WriteLine("0 - Sair");
                string opcao = Helpers.LerString("Escolha uma opção: ");

                switch (opcao)
                {
                    case "1":
                        FazerLogin();
                        break;
                    case "2":
                        await MenuCadastro.ExibirAsync();
                        break;
                    case "0":
                        Console.WriteLine("Saindo...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

                Helpers.LerOpcaoSair();
            }
        }

        private static void FazerLogin()
        {
            Console.Clear();
            Console.WriteLine("--- Login Cliente ---");
            string email = Helpers.LerEmail("Email: ");
            string senha = Helpers.LerSenha("Senha: ");

            Usuario user = AutenticacaoService.LoginCliente(email, senha);

            if (user != null)
            {
                Console.WriteLine($"\nBem-vindo(a), {user.Nome}!");
                switch (user)
                {
                    case Cliente cliente:
                        MenuCliente.Exibir(cliente);
                        break;
                    case Restaurante restaurante:
                        MenuRestaurante.ExibirMenuRestaurante(restaurante);
                        break;
                    case Entregador entregador:
                        MenuEntregador.ExibirMenuEntregador(entregador);
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nLogin inválido! Verifique seu e-mail e senha ou cadastre-se.");
            }
        }
    }
}

