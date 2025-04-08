using Clientes.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Infra.Contexto
{
    public class ClienteContext : DbContext
    {
        public ClienteContext(DbContextOptions<ClienteContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClienteContext).Assembly);

            // Exemplo de configuração para Cliente
            modelBuilder.Entity<Cliente>(e =>
            {
                e.HasKey(c => c.Id);
                e.OwnsOne(c => c.Email, email =>
                {
                    email.Property(e => e.Endereco).HasColumnName("Email");
                });
                e.OwnsOne(c => c.Documento, doc =>
                {
                    doc.Property(d => d.Numero).HasColumnName("Documento");
                    doc.Property(d => d.Tipo).HasColumnName("TipoDocumento");
                });
             
            });
        }

    }
}
