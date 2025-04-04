using System;
using MotoMotoFood.Util.Enums;

namespace MotoMotoFood.Models
{
    public class Entregador : Usuario
    {
        public int Id { get; set; }
        public StatusEntregador StatusEntregador { get;  set; } 
        public string Cpf { get;  set; }
        public string Habilitacao { get;  set; }
        public TipoTransporte TipoTransporte { get; set; }

        public Conta Conta { get; set; }

        public Entregador(int id, string nome, string email, string senha, string endereco, string cpf, string habilitacao, TipoTransporte tipoTransporte)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Senha = senha;
            Endereco = endereco;
            Cpf = cpf;
            Habilitacao = habilitacao;
            TipoTransporte = tipoTransporte;
            StatusEntregador = StatusEntregador.Disponivel;
            Conta = new Conta();
        }

        public bool AtribuirPedido(Pedido pedido)
        {
            if (StatusEntregador == StatusEntregador.Off || StatusEntregador == StatusEntregador.Ocupado) return false;
            StatusEntregador = StatusEntregador.Ocupado;
            pedido.AtribuirEntregador(this);
            return true;
        }
    }
}
