using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoMotoFood.Models
{
    public class Produto
    {
        public Produto(string nome, string descricao, decimal preco, int quantidadeDisponivel, Restaurante restauranteDono)
        {
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            QuantidadeDisponivel = quantidadeDisponivel;
            RestauranteDono = restauranteDono;
        }

        public Produto() { }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public Restaurante RestauranteDono { get; set; }


    }
}

