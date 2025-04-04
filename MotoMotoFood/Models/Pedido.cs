using System;
using System.Collections.Generic;
using System.Linq;
using MotoMotoFood.Util.Enums;

namespace MotoMotoFood.Models
{
    public class Pedido
    {
        public int Id { get; private set; }
        public Cliente Cliente { get; private set; }
        public Restaurante Restaurante { get; private set; }
        public List<ItemPedido> Itens { get; private set; }
        public decimal ValorTotal => Itens.Sum(item => item.Produto.Preco * item.Quantidade);
        public StatusPedido Status { get; set; }
        public Entregador Entregador { get; set; }

        public Pedido(int id, Cliente cliente, Restaurante restaurante, List<ItemPedido> itens, Entregador? entregador)
        {
            Id = id;
            Cliente = cliente;
            Restaurante = restaurante;
            Itens = itens;
            Status = StatusPedido.PENDENTE_PAGAMENTO;
            Entregador = entregador;
        }

        public void AtribuirEntregador(Entregador entregador)
        {
            Entregador = entregador;
            entregador.StatusEntregador = StatusEntregador.Ocupado;
        }

        public void AdicionarEmRota()
        {
            Status = StatusPedido.EM_ROTA;
        }

        public void ConcluirEntrega()
        {
            Status = StatusPedido.CONCLUIDO;
        }

        internal void RealizarPagamento()
        {
            Restaurante.AdicionarSaldo(ValorTotal);
            Cliente.Debitar(ValorTotal);
        }
    }
}
