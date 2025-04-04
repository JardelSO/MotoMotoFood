using System;
using System.Linq;
using MotoMotoFood.Menu;
using MotoMotoFood.Menus.Submenus;
using MotoMotoFood.Models;
using MotoMotoFood.Repositorios;

namespace MotoMotoFood.Menus
{
    public static class MenuCliente
    {
        public static void Exibir(Cliente cliente)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"--- Bem-vindo, {cliente.Nome}! ---");
                Console.WriteLine("1 - Adicionar produtos");
                Console.WriteLine("2 - Meus Pedidos");
                Console.WriteLine("3 - Meu Carrinho");
                Console.WriteLine("4 - Gerenciar conta");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "1":
                        VerRestaurantes(cliente);
                        break;
                    case "2":
                        VerPedidos(cliente);
                        break;
                    case "3":
                        MenuMeuCarrinho.VisualizarCarrinho(cliente);
                        break;
                    case "4":
                        MenuConta.GerenciarConta(cliente);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Pressione ENTER para continuar...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private static void VerRestaurantes(Cliente cliente)
        {
            Console.Clear();
            Console.WriteLine("--- Restaurantes Disponíveis ---");
            List<Restaurante> restaurantes = BancoDeDadosFake.GetRestaurantes();
            if (!restaurantes.Any())
            {
                Console.WriteLine("Nenhum restaurante disponível.");
                Console.ReadLine();
                return;
            }

            for (int i = 0; i < restaurantes.Count; i++)
                Console.WriteLine($"{i + 1} - {restaurantes[i].NomeRestaurante}");

            Console.Write("Escolha um restaurante ou 0 para voltar: ");
            if (int.TryParse(Console.ReadLine(), out int escolha) && escolha > 0 && escolha <= restaurantes.Count)
            {
                MenuFazerPedido.CriarPedido(cliente, restaurantes[escolha - 1]);
            }
        }

        private static void VerPedidos(Cliente cliente)
        {
            Console.Clear();
            Console.WriteLine("--- Meus Pedidos ---");
            List<Pedido> pedidos = BancoDeDadosFake.FindPedidosByClient(cliente);

            if (!pedidos.Any())
            {
                Console.WriteLine("Você não tem pedidos.");
                Console.ReadLine();
                return;
            }

            foreach (var pedido in pedidos)
                Console.WriteLine($"Pedido #{pedido.Id} - {pedido.Status} - R${pedido.ValorTotal}");

            Console.WriteLine("Pressione ENTER para voltar...");
            Console.ReadLine();
        }
    }
}
