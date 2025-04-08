using Clientes.Dominio.Entidades;

namespace Clientes.Dominio.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente[]> GetAllClientesAsync();
        Task<Cliente[]> GetClientesByNomeAsync(string nome);
        Task<Cliente> GetClienteByIdAsync(Guid clienteId);
        Task<Cliente> GetClienteByDocumentoAsync(string documento);
        Task<Cliente> GetClienteByEmailAsync(string email);
        Task<bool> ExisteClienteComDocumentoAsync(string documento);
        Task<bool> ExisteClienteComEmailAsync(string email);
        Task AdicionarAsync(Cliente cliente);
        Task AtualizarAsync(Cliente cliente);
    }
}
