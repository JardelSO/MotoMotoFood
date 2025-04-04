using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotoMotoFood.Util.Exceptions
{
    class CepInvalidoException : Exception
    {
        public CepInvalidoException() : base("Cep inválido!") { }

        public CepInvalidoException(string message) : base(message) { }

        public CepInvalidoException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
