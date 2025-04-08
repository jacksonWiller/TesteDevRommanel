using Ardalis.Result;
using Clientes.Dominio.Entidades;

namespace Clientes.Aplicacao.Queries;

public class GetAllClientesQueryResponse()
{
    public PagedInfo PagedInfo { get; set; }
    public List<Cliente> Clientes { get; set; }
}
