using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MotoMotoFood.Models;
using MotoMotoFood.Repositorios;
using MotoMotoFood.Util;

namespace MotoMotoFood.Menu
{
    class MenuRestaurante
    {
        public static void ExibirMenuRestaurante(Restaurante restaurante)
        {
            Console.Clear();
            Console.WriteLine("Menu Restaurante");
            Console.WriteLine("1 - Gerenciar Produto");
            Console.WriteLine("2 - Gerenciar Conta");
            Console.WriteLine("3 - Gerenciar pedidos");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1": GerenciarProdutos(restaurante); break;
                case "2": MenuConta.GerenciarConta(restaurante); break;
                case "3": MenuGerenciarPedidos(restaurante); break;
                case "0": return;
                default: Console.WriteLine("Opção inválida! Pressione Enter..."); Console.ReadLine(); break;
            }
        }

        public static void GerenciarProdutos(Restaurante restaurante)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Gerenciar Produtos");
                Console.WriteLine("1 - Adicionar");
                Console.WriteLine("2 - Remover");
                Console.WriteLine("3 - Editar");
                Console.WriteLine("4 - Visualizar");
                Console.WriteLine("0 - Voltar");
                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        MenuCadastrarProduto(restaurante);
                        break;
                    case "2":
                        MenuRemoverProduto(restaurante);
                        break;
                    case "3":
                        MenuEditarProduto(restaurante);
                        break;
                    case "4":
                        MenuVisualizarProdutos(restaurante);
                        break;
                    case "0": return;
                }
            }
        }

        private static void MenuGerenciarPedidos(Restaurante restaurante)
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Gerenciar Pedidos");
                Console.WriteLine("1 - Visualizar pedidos em andamento");
                Console.WriteLine("2 - Finalizar pedido");
                Console.WriteLine("3 - Visualizar Historico Pedidos");
                Console.WriteLine("0 - Voltar");
                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        VisualizarPedidosEmAndamento(restaurante);
                        break;
                    case "2":
                        AlterarStatusPedido(restaurante);
                        break;
                    case "3":
                        VisualizarHistoricoPedidos(restaurante);
                        break;
                    case "0": return;
                }
                Console.WriteLine("Pressione Enter...");
                Console.ReadLine();
            }

        }

        private static void VisualizarHistoricoPedidos(Restaurante restaurante)
        {
            List<Pedido> pedidosCompleto = BancoDeDadosFake.FindPedidosCompletosByRestaurante(restaurante);
            Helpers.ExibirPedidosRestaurente(pedidosCompleto);
            Helpers.LerOpcaoSair();
        }

        private static void AlterarStatusPedido(Restaurante restaurante)
        {
            List<Pedido> pedidosPendente = BancoDeDadosFake.FindPedidosAtivosByRestaurante(restaurante);
            Helpers.ExibirPedidosRestaurente(pedidosPendente);
            Helpers.LerInteiroComValorMaximo("Selecione o pedido para finalizar (Digite 0 para sair): ", pedidosPendente.Count);
            Console.WriteLine("Pedido finalizado com sucesso!");
            Helpers.LerOpcaoSair();
        }

        public static void VisualizarPedidosEmAndamento(Restaurante restaurante)
        {
            List<Pedido> pedidosPendente = BancoDeDadosFake.FindPedidosAtivosByRestaurante(restaurante);
            Helpers.ExibirPedidosRestaurente(pedidosPendente);
            Helpers.LerOpcaoSair();
        }

        private static void MenuVisualizarProdutos(Restaurante restaurante)
        {
            List<Produto> produtosRestaurante = BancoDeDadosFake.FindProdutosByRestaurente(restaurante);
            Helpers.ExibirProduto(produtosRestaurante);
            Helpers.LerOpcaoSair();
            
        }

        private static void MenuEditarProduto(Restaurante restaurante)
        {
            List<Produto> produtosRestaurante = BancoDeDadosFake.FindProdutosByRestaurente(restaurante);
            if (!produtosRestaurante.Any())
            {
                Console.Clear();
                Console.WriteLine("Nenhum produto cadastrado.");
                Helpers.LerOpcaoSair();
                return;
            }
            Produto produtoEditado = produtosRestaurante[Helpers.LerOpcaoListaProdutos(produtosRestaurante, "Escolha um dos produtos para editar:\n")];
            produtoEditado.Nome = Helpers.LerString("Nome do produto: ");
            produtoEditado.Descricao = Helpers.LerString("Descricao do produto: ");
            produtoEditado.Preco = Helpers.LerDecimal("Preço: ");
            produtoEditado.QuantidadeDisponivel = Helpers.LerInteiro("Quantidade: ");
            Console.WriteLine("Produto editado com sucesso!");
            Helpers.LerOpcaoSair();
        }

        private static void MenuRemoverProduto(Restaurante restaurante)
        {
            List<Produto> produtosRestaurante = BancoDeDadosFake.FindProdutosByRestaurente(restaurante);
            if (!produtosRestaurante.Any())
            {
                Console.Clear();
                Console.WriteLine("Nenhum produto cadastrado.");
                Helpers.LerOpcaoSair();
                return;
            }
            Produto podutoRemovido = produtosRestaurante[Helpers.LerOpcaoListaProdutos(produtosRestaurante, "Escolha um dos produtos para remover:\n")];
            BancoDeDadosFake.Produtos.Remove(podutoRemovido);
            Console.WriteLine("Produto removido com sucesso!");
            Helpers.LerOpcaoSair();
        }

        private static void MenuCadastrarProduto(Restaurante restaurante)
        {
            Produto result = new Produto();
            result.Nome = Helpers.LerString("Nome do produto: ");
            result.Descricao = Helpers.LerString("Descricao do produto: ");
            result.Preco = Helpers.LerDecimal("Preço: ");
            result.QuantidadeDisponivel = Helpers.LerInteiro("Quantidade: ");
            result.RestauranteDono = restaurante;
            BancoDeDadosFake.Produtos.Add(result);
            Console.WriteLine("Produto cadastrado com sucesso!");
            Helpers.LerOpcaoSair();
        }
    }
}
