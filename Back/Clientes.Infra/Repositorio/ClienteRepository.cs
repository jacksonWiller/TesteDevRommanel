using Clientes.Dominio.Dtos;
using Clientes.Dominio.Entidades;
using Clientes.Dominio.Interfaces;
using Clientes.Infra.Contexto;
using Fop;
using Fop.FopExpression;
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

        public async Task<(List<ClienteDto>, int)> GetAllClientesAsync(
            string filter = null,
            string order = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var fopRequest = FopExpressionBuilder<Cliente>.Build(filter, order, pageNumber, pageSize);

            var (filteredClientes, totalRecords) = _dataContext.Clientes
                .Include(c => c.Endereco)
                .Include(c => c.Documento)
                .Include(c => c.Email)
                .Include(c => c.Telefone)
                .AsNoTracking()
                .ApplyFop(fopRequest);

            var clientesLista = await filteredClientes.ToListAsync();

            var clientesListaDto = new List<ClienteDto>();

            clientesListaDto = clientesLista?.Select(c => new ClienteDto
            {
                Id = c.Id,
                Nome = c.Nome,
                Documento = c.Documento.Numero,
                TipoDocumento = c.Documento.Tipo,
                DataNascimento = c.DataNascimento,
                Telefone = c.Telefone.Numero,
                Email = c.Email.Endereco,
                Cep = c.Endereco.Cep,
                Logradouro = c.Endereco.Logradouro,
                Numero = c.Endereco.Numero,
                Bairro = c.Endereco.Bairro,
                Cidade = c.Endereco.Cidade,
                Estado = c.Endereco.Estado,
                InscricaoEstadual = c.InscricaoEstadual,
                Isento = c.Isento
            }).ToList();

            return (clientesListaDto, totalRecords);
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

        public async Task AdicionarAsync(Cliente cliente)
        {
            _dataContext.Clientes.Add(cliente);
            await _dataContext.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Cliente cliente)
        {
            _dataContext.Clientes.Update(cliente);
            await _dataContext.SaveChangesAsync();
        }

        public Task<Cliente[]> GetAllClientesAsync()
        {
            throw new NotImplementedException();
        }
    }
}