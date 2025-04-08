using Clientes.Dominio.Entidade;

namespace Clientes.Dominio.Eventos
{
    public abstract class BaseEvent
    {
        public Guid ClienteId { get; private set; }
        public DateTime DataOcorrencia { get; private set; }
        public Cliente Cliente { get; private set; }

        public BaseEvent(EntidadeBase entidade)
        {
            ClienteId = entidade.Id;
            DataOcorrencia = DateTime.Now;
        }
    }
}
