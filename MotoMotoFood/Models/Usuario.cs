using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotoMotoFood.entitdades;

namespace MotoMotoFood.Models
{
    public class Usuario
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public Endereco Endereco { get; set; }
        public Conta Conta { get; set; } = new Conta();

        public void AdicionarSaldo(decimal valor)
        {
            Conta.Depositar(valor);
        }

        public bool Debitar(decimal valor)
        {
            if (valor <= Conta.Saldo)
            {
                Conta.Sacar(valor);
                return true;
            }
            return false;
        }

    }
}

