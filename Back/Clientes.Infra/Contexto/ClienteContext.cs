using Clientes.Dominio.Entidades;
using Clientes.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Infra.Contexto;

public class ClienteContext(DbContextOptions<ClienteContext> dbOptions) : BaseDbContext<ClienteContext>(dbOptions), IClienteContext
{
    public DbSet<Cliente> Clientes { get; set; }
}
