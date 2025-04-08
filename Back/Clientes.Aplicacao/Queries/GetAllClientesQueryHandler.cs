using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Clientes.Dominio.Dtos;
using Clientes.Dominio.Entidades;
using Clientes.Dominio.Interfaces;
using FluentValidation;
using Fop;
using Fop.FopExpression;
using MediatR;

namespace Clientes.Aplicacao.Queries;

public class GetAllClientesQueryHandler : IRequestHandler<GetAllClientesQuery, Result<GetAllClientesQueryResponse>>
{
    public readonly IClienteContext _context;
    private readonly IValidator<GetAllClientesQuery> _validator;

    public GetAllClientesQueryHandler(IClienteContext context, IValidator<GetAllClientesQuery> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<Result<GetAllClientesQueryResponse>> Handle(GetAllClientesQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result<GetAllClientesQueryResponse>.Invalid(validationResult.AsErrors());
        }

        var fopRequest = FopExpressionBuilder<Cliente>.Build(request.Filter, request.Order, request.PageNumber, request.PageSize);

        var (filteredClientes, totalCount) = _context.Set<Cliente>().ApplyFop(fopRequest);

        var pagedInfo = new PagedInfo(
                                        request.PageNumber,
                                        request.PageSize,
                                        (int)Math.Ceiling((double)totalCount / request.PageSize),
                                        totalCount
                                        );

        var response = new GetAllClientesQueryResponse
        {
            PagedInfo = pagedInfo,
            Clientes = filteredClientes?.Select(c => new ClienteDto
            {
                Id = c.Id,
                Nome = c.Nome,
                //Documento = c.Documento.Numero,
                //TipoDocumento = c.Documento.Tipo,
                //DataNascimento = c.DataNascimento,
                //Telefone = c.Telefone.Numero,
                //Email = c.Email.Endereco,

                //Cep = c.Endereco.Cep,
                //Logradouro = c.Endereco.Logradouro,
                //Numero = c.Endereco.Numero,
                //Bairro = c.Endereco.Bairro,
                //Cidade = c.Endereco.Cidade,
                //Estado = c.Endereco.Estado,

                InscricaoEstadual = c.InscricaoEstadual,
                Isento = c.Isento
            }).ToList() ?? []
        };

        return Result<GetAllClientesQueryResponse>.Success(response, "Clientes retrieved successfully.");
    }
}


