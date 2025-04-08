using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using Clientes.Dominio.Dtos;
using Clientes.Dominio.Interfaces;
using FluentValidation;
using MediatR;

namespace Clientes.Aplicacao.Queries;

public class GetAllClientesQueryHandler : IRequestHandler<GetAllClientesQuery, Result<GetAllClientesQueryResponse>>
{
    private readonly IValidator<GetAllClientesQuery> _validator;
    private readonly IClienteRepository _clienteRepository;

    public GetAllClientesQueryHandler(IClienteRepository clienteRepository, IValidator<GetAllClientesQuery> validator)
    {
        _clienteRepository = clienteRepository;
        _validator = validator;
    }

    public async Task<Result<GetAllClientesQueryResponse>> Handle(GetAllClientesQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result<GetAllClientesQueryResponse>.Invalid(validationResult.AsErrors());
        }

        var clientes = await _clienteRepository.GetAllClientesAsync(
            request.Filter,
            request.Order,
            request.PageNumber,
            request.PageSize
        );
       
        var pagedInfo = new PagedInfo(
                                        request.PageNumber,
                                        request.PageSize,
                                        (int)Math.Ceiling((double)clientes.Count / request.PageSize),
                                        clientes.Count
                                        );

        var response = new GetAllClientesQueryResponse
        {
            PagedInfo = pagedInfo,
            Clientes = clientes?.Select(c => new ClienteDto
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
            }).ToList() ?? []
        };

        return Result<GetAllClientesQueryResponse>.Success(response, "Clientes retrieved successfully.");
    }
}


