using Clientes.Dominio.Entidades;
using Clientes.Dominio.Interfaces;
using Clientes.Infra.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Infra.Repositorios
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClienteContext _dataContext;

        public ClienteRepository(ClienteContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Cliente[]> GetAllClientesAsync()
        {
            IQueryable<Cliente> query = _dataContext.Clientes
                .Include(c => c.Endereco);

            query = query.AsNoTracking().OrderBy(c => c.Nome);

            return await query.ToArrayAsync();
        }

        public async Task<Cliente[]> GetClientesByNomeAsync(string nome)
        {
            IQueryable<Cliente> query = _dataContext.Clientes
                .Include(c => c.Endereco);

            query = query.AsNoTracking().OrderBy(c => c.Nome)
                        .Where(c => c.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Cliente> GetClienteByIdAsync(Guid clienteId)
        {
            IQueryable<Cliente> query = _dataContext.Clientes
                .Include(c => c.Endereco);

            query = query.AsNoTracking()
                        .Where(c => c.Id == clienteId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Cliente> GetClienteByDocumentoAsync(string documento)
        {
            IQueryable<Cliente> query = _dataContext.Clientes;

            query = query.AsNoTracking()
                        .Where(c => c.Documento.Numero == documento);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Cliente> GetClienteByEmailAsync(string email)
        {
            IQueryable<Cliente> query = _dataContext.Clientes;

            query = query.AsNoTracking()
                        .Where(c => c.Email.Endereco == email);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExisteClienteComDocumentoAsync(string documento)
        {
            return await _dataContext.Clientes
                .AnyAsync(c => c.Documento.Numero == documento);
        }

        public async Task<bool> ExisteClienteComEmailAsync(string email)
        {
            return await _dataContext.Clientes
                .AnyAsync(c => c.Email.Endereco == email);
        }
    }
}