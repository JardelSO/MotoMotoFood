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
    public class MenuEntregador
    {
        public static void ExibirMenuEntregador(Entregador entregador)
        {
            while (true) { 
                Console.Clear();
                Console.WriteLine("Menu Entregador");
                Console.WriteLine("1 - Gerenciar Conta");
                Console.WriteLine("2 - Gerenciar pedidos");
                if (entregador.StatusEntregador == Util.Enums.StatusEntregador.Off)
                {
                    Console.WriteLine("3 - Alterar status para online");
                }
                else
                {
                    Console.WriteLine("3 - Alterar status para off");
                }
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1": MenuConta.GerenciarConta(entregador); break;
                    case "2": MenuGerenciarPedidos(entregador); break;
                    case "3": AlterarStatusEntregador(entregador); break;
                    case "0": return;
                    default: Console.WriteLine("Opção inválida! Pressione Enter..."); Console.ReadLine(); break;
                }
            }


        }

        private static void AlterarStatusEntregador(Entregador entregador)
        {
            entregador.StatusEntregador = Util.Enums.StatusEntregador.Disponivel;
        }

        private static void MenuGerenciarPedidos(Entregador entregador)
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
                        VisualizarPedidosEmAndamento(entregador);
                        break;
                    case "2":
                        AlterarStatusPedido(entregador);
                        break;
                    case "3":
                        VisualizarHistoricoPedidos(entregador);
                        break;
                    case "0": return;
                }
                Console.WriteLine("Pressione Enter...");
                Console.ReadLine();
            }

        }

        private static void VisualizarHistoricoPedidos(Entregador entregador)
        {
            List<Pedido> pedidosCompleto = BancoDeDadosFake.FindPedidosCompletosByEntregador(entregador);
            Helpers.ExibirPedidosRestaurente(pedidosCompleto);
            Helpers.LerOpcaoSair();
        }

        private static void AlterarStatusPedido(Entregador entregador)
        {
            List<Pedido> pedidosPendente = BancoDeDadosFake.FindPedidosAtivosByEntregador(entregador);
            Helpers.ExibirPedidosRestaurente(pedidosPendente);
            int index = Helpers.LerInteiroComValorMaximo("Selecione o pedido para finalizar (Digite 0 para sair): ", pedidosPendente.Count);
            pedidosPendente[index].Status = Util.Enums.StatusPedido.CONCLUIDO;
            Console.WriteLine("Pedido finalizado com sucesso!");
            Helpers.LerOpcaoSair();
        }

        public static void VisualizarPedidosEmAndamento(Entregador entregador)
        {
            List<Pedido> pedidosPendente = BancoDeDadosFake.FindPedidosAtivosByEntregador(entregador);
            Helpers.ExibirPedidosRestaurente(pedidosPendente);
            Helpers.LerOpcaoSair();
        }

    }
}
