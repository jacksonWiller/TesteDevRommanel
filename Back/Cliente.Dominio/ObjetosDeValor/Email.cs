using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Dominio.ObjetosDeValor
{
    public class Email
    {
        public string Endereco { get; private set; }

        protected Email() { }

        public Email(string endereco)
        {
            Endereco = endereco;
        }

        public override string ToString()
        {
            return Endereco;
        }
    }
}
