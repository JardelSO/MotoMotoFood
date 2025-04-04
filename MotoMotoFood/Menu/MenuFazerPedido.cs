using System;
using System.Collections.Generic;
using System.Linq;
using MotoMotoFood.Models;
using MotoMotoFood.Repositorios;

namespace MotoMotoFood.Menus.Submenus
{
    public static class MenuFazerPedido
    {
        public static void CriarPedido(Cliente cliente, Restaurante restaurante)
        {
            Console.Clear();
            Console.WriteLine($"--- Produtos de {restaurante.NomeRestaurante} ---");
            var produtos = BancoDeDadosFake.Produtos.Where(p => p.RestauranteDono == restaurante).ToList();

            if (!produtos.Any())
            {
                Console.WriteLine("Nenhum produto disponível.");
                Console.WriteLine("Pressione ENTER para voltar...");
                Console.ReadLine();
                return;
            }

            List<ItemPedido> itens = new List<ItemPedido>();
            while (true)
            {
                for (int i = 0; i < produtos.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {produtos[i].Nome} - R${produtos[i].Preco} ({produtos[i].QuantidadeDisponivel} disponíveis)");
                }

                Console.Write("Escolha um produto (0 para finalizar): ");
                if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 0 || opcao > produtos.Count)
                {
                    Console.WriteLine("Opção inválida!");
                    continue;
                }
                if (opcao == 0) break;

                Produto produtoEscolhido = produtos[opcao - 1];
                Console.Write("Quantidade: ");
                if (!int.TryParse(Console.ReadLine(), out int quantidade) || quantidade < 1 || quantidade > produtoEscolhido.QuantidadeDisponivel)
                {
                    Console.WriteLine("Quantidade inválida!");
                    continue;
                }

                itens.Add(new ItemPedido(produtoEscolhido, quantidade));
                produtoEscolhido.QuantidadeDisponivel -= quantidade;
                Console.WriteLine("Item adicionado ao pedido!");
            }

            if (!itens.Any())
            {
                Console.WriteLine("Pedido cancelado.");
                return;
            }

             Pedido novoPedido = new Pedido(BancoDeDadosFake.Pedidos.Count + 1, cliente, restaurante, itens, null);
             BancoDeDadosFake.Pedidos.Add(novoPedido);
             Console.WriteLine($"Pedido #{novoPedido.Id} adicionado ao carrinho com sucesso!");
             Console.WriteLine("Pressione ENTER para continuar...");
             Console.ReadLine();
        }
    }
}
