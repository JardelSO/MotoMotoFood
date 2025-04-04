using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotoMotoFood.Models;
using MotoMotoFood.Repositorios;
using MotoMotoFood.Util;

namespace MotoMotoFood.Menu
{
    public class MenuMeuCarrinho
    {
        public static void VisualizarCarrinho(Cliente cliente)
        {
            List<Pedido> pendentes = BancoDeDadosFake.GetPedidoPendentePagamento(cliente);
            if (pendentes == null || pendentes.Count ==0)
            {
                Console.WriteLine("Seu carrinho está vazio!");
                Helpers.LerOpcaoSair();
                return;
            }
            Console.Clear();
            MenuCarrinho(pendentes[0]);
        }

        private static void MenuCarrinho(Pedido pedido)
        {
            Helpers.ExibirPedido(pedido);

            Console.WriteLine("=== Menu Carrinho ===");

            Console.WriteLine("1. Alterar quantidade do produto");
            Console.WriteLine("2. Remover produto");
            Console.WriteLine("3. Limpar Carrinho");
            Console.WriteLine("4. Finalizar Pedido");
            Console.WriteLine("0. Voltar");
            var escolha = Helpers.LerString("Selecione uma opção: ");

            switch (escolha)
            {
                case "1":
                    MenuAlterarProduto(pedido);
                    break;
                case "2":
                    MenuRemoverProduto(pedido);
                    break;
                case "3":
                    LimparCarrinho(pedido);
                    break;
                case "4":
                    MenuFinalizarPedido(pedido);
                    break;
                case "0":
                    return;
            }
        }

        private static void MenuFinalizarPedido(Pedido pedido)
        {
            if (pedido.Cliente.Conta.Saldo < pedido.ValorTotal)
            {
                Console.WriteLine("Saldo insuficiente!");
                return;
            }
            List<Entregador> entregadoresDisponivel;
            int contador = 0;
            while (true)
            {
                entregadoresDisponivel = BancoDeDadosFake.GetEntregadorDisponivel();
                if (entregadoresDisponivel != null && entregadoresDisponivel.Count > 0)
                {
                    break;
                }
                if(contador == 10)
                {
                    Console.Write("Desculpe! Não foi possível encontrar um entregador! Tente novamente mais tarde.");
                    Console.WriteLine("Seu pedido será cancelado");
                    BancoDeDadosFake.Pedidos.Remove(pedido);
                    return;
                }
                Console.WriteLine("Procurando entregador ...");
                Thread.Sleep(5000);
                contador++;
            }
            pedido.AtribuirEntregador(entregadoresDisponivel[0]);
            pedido.RealizarPagamento();
            pedido.Status = Util.Enums.StatusPedido.PREPARANDO;
        }



        private static void LimparCarrinho(Pedido pedido)
        {
            BancoDeDadosFake.Pedidos.Remove(pedido);
        }

        private static void MenuAlterarProduto(Pedido pedido)
        {
            Console.Clear();
            Helpers.ExibirPedido(pedido);

            int index = Helpers.LerInteiroComValorMaximo(
                "Selecione o número do produto que deseja alterar (0 para voltar): ",
                pedido.Itens.Count);

            if (index == -1) return;

            ItemPedido item = pedido.Itens[index];

            Console.WriteLine($"\nProduto selecionado: {item.Produto.Nome}");
            Console.WriteLine($"Quantidade atual: {item.Quantidade}");
            Console.WriteLine($"Quantidade disponível em estoque: {item.Produto.QuantidadeDisponivel}");

            int novaQuantidade = Helpers.LerInteiroComValorMaximo(
                $"Digite a nova quantidade: ",
                item.Produto.QuantidadeDisponivel);
            item.Quantidade = novaQuantidade;
            Console.WriteLine("Quantidade atualizada com sucesso!");
            Helpers.LerOpcaoSair();
        }


        private static void MenuRemoverProduto(Pedido pedido)
        {
            Helpers.ExibirPedido(pedido);
            int index = Helpers.LerInteiroComValorMaximo(
                "Selecione o número do produto que deseja remover (0 para voltar): ",
                pedido.Itens.Count);
            if (index == -1) return;
            pedido.Itens.RemoveAt(index);
            Console.WriteLine("Pedido removido com sucesso!");
            Helpers.LerOpcaoSair();
        }


    }
}
