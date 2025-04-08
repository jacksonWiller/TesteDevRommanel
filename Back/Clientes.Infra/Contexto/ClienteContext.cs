using Clientes.Dominio.Entidades;
using Clientes.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Infra.Contexto
{
    public class ClienteContext : DbContext, IClienteContext
    {
        public ClienteContext(DbContextOptions<ClienteContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClienteContext).Assembly);

            modelBuilder.Entity<Cliente>(e =>
            {
                e.HasKey(c => c.Id);

                e.Property(c => c.Nome)
                    .IsRequired()
                    .HasMaxLength(200);

                e.Property(c => c.DataNascimento)
                    .IsRequired();

                e.Property(c => c.InscricaoEstadual)
                    .HasMaxLength(20);

                e.Property(c => c.Isento)
                    .IsRequired();


                e.OwnsOne(c => c.Email, email =>
                {
                    email.Property(e => e.Endereco)
                        .HasColumnName("Email")
                        .IsRequired()
                        .HasMaxLength(256);
                });

                e.OwnsOne(c => c.Documento, doc =>
                {
                    doc.Property(d => d.Numero)
                        .HasColumnName("Documento")
                        .IsRequired()
                        .HasMaxLength(14);

                    doc.Property(d => d.Tipo)
                        .HasColumnName("TipoDocumento")
                        .IsRequired();
                });

                e.OwnsOne(c => c.Telefone, tel =>
                {
                    tel.Property(t => t.Numero)
                        .HasColumnName("Telefone")
                        .IsRequired()
                        .HasMaxLength(11);
                });

                e.OwnsOne(c => c.Endereco, end =>
                {
                    end.Property(e => e.Cep)
                        .HasColumnName("Cep")
                        .IsRequired()
                        .HasMaxLength(8);

                    end.Property(e => e.Logradouro)
                        .HasColumnName("Logradouro")
                        .IsRequired()
                        .HasMaxLength(200);

                    end.Property(e => e.Numero)
                        .HasColumnName("Numero")
                        .IsRequired()
                        .HasMaxLength(10);

                    end.Property(e => e.Bairro)
                        .HasColumnName("Bairro")
                        .IsRequired()
                        .HasMaxLength(100);

                    end.Property(e => e.Cidade)
                        .HasColumnName("Cidade")
                        .IsRequired()
                        .HasMaxLength(100);

                    end.Property(e => e.Estado)
                        .HasColumnName("Estado")
                        .IsRequired()
                        .HasMaxLength(2);
                });


                e.HasIndex("Documento").IsUnique();

                e.HasIndex("Email").IsUnique();
            });
        }

    }
}
