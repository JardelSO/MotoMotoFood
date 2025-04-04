using System;
using System.Collections.Generic;
using System.Linq;
using MotoMotoFood.Models;
using MotoMotoFood.Repositorios;
using MotoMotoFood.Util;

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

                if (!int.TryParse(Helpers.LerString("Escolha um produto (0 para finalizar): "), out int opcao) || opcao < 0 || opcao > produtos.Count)
                {
                    Console.WriteLine("Opção inválida!");
                    continue;
                }
                if (opcao == 0) break;

                Produto produtoEscolhido = produtos[opcao - 1];
                if (!int.TryParse(Helpers.LerString("Quantidade: "), out int quantidade) || quantidade < 1 || quantidade > produtoEscolhido.QuantidadeDisponivel)
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
             List<Pedido> pedidoCarrinho = BancoDeDadosFake.GetPedidoPendentePagamento(cliente);
            if (pedidoCarrinho != null && pedidoCarrinho.Count > 0)
            {
                if (pedidoCarrinho[0].Restaurante != restaurante)
                {
                    Console.WriteLine("Não é possível adicionar produtos no carrinho de restaurantes diferentes!");
                    Console.WriteLine("Pressione ENTER para voltar...");
                    Console.ReadLine();
                    return;
                }
                else
                {
                    pedidoCarrinho[0].Itens.AddRange(itens);
                    Console.WriteLine($"Pedido adicionado ao carrinho com sucesso!");
                    Console.WriteLine("Pressione ENTER para continuar...");
                }
            }
            else
            {
                Pedido novoPedido = new Pedido(BancoDeDadosFake.Pedidos.Count + 1, cliente, restaurante, itens, null);
                BancoDeDadosFake.Pedidos.Add(novoPedido);
                Console.WriteLine($"Pedido #{novoPedido.Id} adicionado ao carrinho com sucesso!");
                Console.WriteLine("Pressione ENTER para continuar...");
                Console.ReadLine();
            }
             
        }
    }
}
