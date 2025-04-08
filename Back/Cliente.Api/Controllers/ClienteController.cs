using Ardalis.Result;
using Clientes.Api.Extensions;
using Clientes.Api.Models;
using Clientes.Aplicacao.Commands.CreateCliente;
using Clientes.Aplicacao.Commands.DeleteCliente;
using Clientes.Aplicacao.Commands.UpadateCliente;
using Clientes.Aplicacao.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace Clientes.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClientesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    ////////////////////////
    // POST: /api/clientes
    ////////////////////////

    /// <summary>
    /// Cadastra um novo cliente.
    /// </summary>
    /// <response code="200">Retorna o Id do novo cliente.</response>
    /// <response code="400">Retorna lista de erros se a requisição for inválida.</response>
    /// <response code="500">Quando ocorre um erro interno inesperado no servidor.</response>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse<CreateClienteResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody][Required] CreateClienteCommand command) =>
        (await _mediator.Send(command)).ToActionResult();

    //////////////////////
    // PUT: /api/clientes
    //////////////////////

    /// <summary>
    /// Atualiza um cliente existente.
    /// </summary>
    /// <response code="200">Retorna a resposta com a mensagem de sucesso.</response>
    /// <response code="400">Retorna lista de erros se a requisição for inválida.</response>
    /// <response code="404">Quando nenhum cliente é encontrado pelo Id informado.</response>
    /// <response code="500">Quando ocorre um erro interno inesperado no servidor.</response>
    [HttpPut]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody][Required] UpdateClienteCommand command) =>
        (await _mediator.Send(command)).ToActionResult();

    //////////////////////////////
    // DELETE: /api/clientes/{id}
    //////////////////////////////

    /// <summary>
    /// Exclui o cliente pelo Id.
    /// </summary>
    /// <response code="200">Retorna a resposta com a mensagem de sucesso.</response>
    /// <response code="400">Retorna lista de erros se a requisição for inválida.</response>
    /// <response code="404">Quando nenhum cliente é encontrado pelo Id informado.</response>
    /// <response code="500">Quando ocorre um erro interno inesperado no servidor.</response>
    [HttpDelete("{id:guid}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([Required] Guid id) =>
        (await _mediator.Send(new DeleteClienteCommand(id))).ToActionResult();

    //////////////////////
    // GET: /api/clientes
    //////////////////////

    /// <summary>
    /// Obtém uma lista de todos os clientes.
    /// </summary>
    /// <response code="200">Retorna a lista de clientes.</response>
    /// <response code="500">Quando ocorre um erro interno inesperado no servidor.</response>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ApiResponse<PagedResult<GetAllClientesQueryResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllClientesQuery query) =>
        (await _mediator.Send(query)).ToActionResult();
}
