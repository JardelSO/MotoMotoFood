using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotoMotoFood.Util.Enums;

namespace MotoMotoFood.entitdades
{
    public class Transporte
    {
        public TipoTransporte Tipo { get; set; }

        public int AnoFabricacao { get; set; }

        public string Modelo { get; set; }

        public string Placa { get; set; }

        public string Cor { get; set; }

        public Transporte(TipoTransporte tipo, int anoFabricacao, string modelo, string placa, string cor)
        {
            Tipo = tipo;
            AnoFabricacao = anoFabricacao;
            Modelo = modelo;
            Placa = placa;
            Cor = cor;
        }

        public Transporte() { }
    }
}
