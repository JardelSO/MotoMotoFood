using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoMotoFood.Models
{
    public class Conta
    {
        public decimal Saldo { get; private set; }

        public Conta()
        {
            Saldo = 0;
        }

        public bool Depositar(decimal valor)
        {
            Saldo += valor;
            return true;
        }

        public bool Sacar(decimal valor)
        {
            if (Saldo >= valor)
            {
                Saldo -= valor;
                return true;
            }
            return false;
        }

        public string GetSaldo()
        {
            return Saldo.ToString("C");
        }
    }
}
