namespace Clientes.Aplicacao.Queries
{
    public class GetAllClientesQueryValidator
    {
        public string Filter { get; set; } = string.Empty;
        public string Order { get; set; } = string.Empty;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
