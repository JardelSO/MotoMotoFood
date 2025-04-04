using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoMotoFood.Models
{
    public class Restaurante : Usuario
    {
        public string CNPJ { get; set; }
        public string NomeRestaurante { get; set; }
        public string HorarioFuncionamento { get; set; }
        public string TempoEntrega { get; set; }
    }
}

