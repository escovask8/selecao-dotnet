using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlataformaCursos.Application.Commands.Cartoes;
using PlataformaCursos.Application.Commands.Estudantes;
using PlataformaCursos.Application.Common.Models;
using PlataformaCursos.Application.DTOs;

namespace PlataformaCursos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class EstudantesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<EstudantesController> _logger;

    public EstudantesController(IMediator mediator, ILogger<EstudantesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Cadastra um novo estudante na plataforma
    /// </summary>
    /// <param name="command">Dados do estudante</param>
    /// <returns>Estudante cadastrado</returns>
    /// <response code="200">Estudante cadastrado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    [HttpPost("cadastro")]
    [ProducesResponseType(typeof(EstudanteDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EstudanteDto>> Cadastrar([FromBody] CadastrarEstudanteCommand command)
    {
        _logger.LogInformation("Tentativa de cadastro de estudante: {Email}", command.Email);

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error, errors = result.Errors });

        return Ok(result.Value);
    }

    /// <summary>
    /// Adiciona um cartão de crédito ao estudante
    /// </summary>
    /// <param name="id">ID do estudante</param>
    /// <param name="dto">Dados do cartão</param>
    /// <returns>Cartão cadastrado</returns>
    /// <response code="200">Cartão cadastrado com sucesso</response>
    /// <response code="400">Dados inválidos</response>
    /// <response code="404">Estudante não encontrado</response>
    [HttpPost("{id}/cartoes")]
    [ProducesResponseType(typeof(CartaoCreditoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CartaoCreditoDto>> AdicionarCartao(
        [FromRoute] Guid id,
        [FromBody] CadastrarCartaoCreditoDto dto)
    {
        var command = new AdicionarCartaoCreditoCommand(
            id,
            dto.Numero,
            dto.NomeTitular,
            dto.Validade,
            dto.CVV);

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error, errors = result.Errors });

        return Ok(result.Value);
    }
}

