using Clientes.Dominio.Entidade;

namespace Clientes.Dominio.Eventos
{
    public abstract class ClienteBaseEvent
    {
        public Guid ClienteId { get; private set; }
        public DateTime DataOcorrencia { get; private set; }
        public Cliente Cliente { get; private set; }

        public ClienteBaseEvent(Cliente cliente)
        {
            ClienteId = cliente.Id;
            DataOcorrencia = DateTime.Now;
            Cliente = cliente;
        }
    }
}
