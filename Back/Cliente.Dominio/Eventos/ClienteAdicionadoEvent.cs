using Clientes.Dominio.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clientes.Dominio.Eventos
{
    public class AdicionandoClienteEvent(Cliente cliente) : ClienteBaseEvent(cliente)
    {
    }
}
