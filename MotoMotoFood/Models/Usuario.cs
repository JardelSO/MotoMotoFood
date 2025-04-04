using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MotoMotoFood.entitdades;

namespace MotoMotoFood.Models
{
    public class Usuario
    {
        public string Email { get; set; }

        private byte[] senhaHash;

        private byte[] senhaSalt;
        public string Nome { get; set; }
        public Endereco Endereco { get; set; }
        public Conta Conta { get; set; } = new Conta();

        public void GerarHashSenha(string senha)
        {
            using (HMACSHA256 hmac = new HMACSHA256())
            {
                senhaSalt = hmac.Key;
                senhaHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));
            }
        }

        public bool VerificarSenha(string senhaTentativa)
        {
            using (HMACSHA256 hmac = new HMACSHA256(senhaSalt))
            {
                byte[] hashTentativa = hmac.ComputeHash(Encoding.UTF8.GetBytes(senhaTentativa));
                return StructuralComparisons.StructuralEqualityComparer.Equals(hashTentativa, senhaHash);
            }
        }

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

