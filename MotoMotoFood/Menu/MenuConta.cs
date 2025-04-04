using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotoMotoFood.Models;
using MotoMotoFood.Repositorios;
using MotoMotoFood.Util;
using static System.Net.Mime.MediaTypeNames;

namespace MotoMotoFood.Menu
{
    public class MenuConta
    {
        public static void GerenciarConta(Usuario usuario)
        {
            switch (usuario) {
                case Restaurante:
                case Entregador:
                    MenuComercial(usuario);
                    break;
                case Cliente:
                    MenuCliente(usuario);
                    break;
            }
        }

        private static void MenuCliente(Usuario usuario)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Conta ===");
                Console.WriteLine("1. Visualizar saldo");
                Console.WriteLine("2. Depositar");
                Console.WriteLine("3. Sair");
                string opcao = Helpers.LerString("Escolha uma opção: ");

                switch (opcao)
                {
                    case "1":
                        MenuVisualizarSaldo(usuario.Conta);
                        break;
                    case "2":
                        MenuDepositar(usuario.Conta);
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }
        }

        private static void MenuComercial(Usuario usuario)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Conta ===");
                Console.WriteLine("1. Visualizar saldo");
                Console.WriteLine("2. Sacar");
                Console.WriteLine("3. Sair");
                string opcao = Helpers.LerString("Escolha uma opção: ");

                switch (opcao)
                {
                    case "1":
                        MenuVisualizarSaldo(usuario.Conta);
                        break;
                    case "2":
                        MenuSacar(usuario.Conta);
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }

        }

        private static void MenuSacar(Conta conta)
        {
            Helpers.LerValorParaSaque("Informe o valor para saque: ", conta);
        }

        private static void MenuDepositar(Conta conta)
        {
            Helpers.LerValorParaDeposito("Informe o valor para deposito ou 0 para voltar: ", conta);
        }

        private static void MenuVisualizarSaldo(Conta conta)
        {
            Console.WriteLine("=== Saldo Conta ===");
            Console.WriteLine(conta.GetSaldo());
            Helpers.LerOpcaoSair();
        }


    }
}
