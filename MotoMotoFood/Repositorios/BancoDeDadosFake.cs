using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MotoMotoFood.Models;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using MotoMotoFood.Util.Enums;
using System.Net.NetworkInformation;

namespace MotoMotoFood.Repositorios
{
    public static class BancoDeDadosFake
    {
        public static List<Usuario> Usuarios = new List<Usuario>();
        public static List<Produto> Produtos = new List<Produto>();
        public static List<Pedido> Pedidos = new List<Pedido>();

        public static List<Pedido> FindPedidosCompletosByEntregador(Entregador entregador)
        {
            return Pedidos.Where(p => p.Entregador == entregador && p.Status == StatusPedido.CONCLUIDO).ToList();
        }

        public static List<Pedido> FindPedidosAtivosByEntregador(Entregador entregador)
        {
            return Pedidos.Where(p => p.Entregador == entregador && p.Status == StatusPedido.EM_ROTA || p.Status == StatusPedido.PREPARANDO).ToList();
        }

        public static List<Pedido> FindPedidosCompletosByRestaurante(Restaurante restaurante)
        {
            return Pedidos.Where(p => p.Restaurante == restaurante && p.Status == StatusPedido.CONCLUIDO).ToList();
        }


        public static List<Pedido> FindPedidosAtivosByRestaurante(Restaurante restaurante)
        {
            return Pedidos.Where(p => p.Restaurante == restaurante && p.Status == StatusPedido.PREPARANDO).ToList();
        }

        public static List<Produto> FindProdutosByRestaurente(Restaurante restaurante)
        {
            return Produtos.Where(p => p.RestauranteDono == restaurante).ToList();
        }

        public static List<Pedido> GetPedidoPendentePagamento(Cliente cliente)
        {
            return Pedidos.Where(p => p.Cliente == cliente && p.Status == StatusPedido.PENDENTE_PAGAMENTO).ToList();
        }

        public static List<Entregador> GetEntregadores()
        {
            return Usuarios.OfType<Entregador>().ToList();
        }

        public static List<Restaurante> GetRestaurantes()
        {
            return Usuarios.OfType<Restaurante>().ToList();
        }

        public static List<Cliente> GetClientees()
        {
            return Usuarios.OfType<Cliente>().ToList();
        }

        public static List<Pedido> GetPedidosAtivos()
        {
            return Pedidos.Where(p => p.Status != StatusPedido.CONCLUIDO).ToList();
        }

        public static List<Pedido> FindPedidosByClient(Cliente client)
        {
            return Pedidos.Where(p => p.Cliente == client).ToList();
        }

        public static List<Entregador> GetEntregadorDisponivel()
        {
            return Usuarios.OfType<Entregador>().Where(p => p.StatusEntregador == StatusEntregador.Disponivel).ToList();   
        }
    }
}

