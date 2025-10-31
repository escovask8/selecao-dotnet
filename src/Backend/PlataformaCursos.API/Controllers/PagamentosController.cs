using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlataformaCursos.Application.DTOs;
using PlataformaCursos.Application.Queries.Pagamentos;

namespace PlataformaCursos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class PagamentosController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<PagamentosController> _logger;

    public PagamentosController(IMediator mediator, ILogger<PagamentosController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Lista todos os pagamentos de um estudante
    /// </summary>
    /// <param name="estudanteId">ID do estudante</param>
    /// <returns>Lista de pagamentos</returns>
    /// <response code="200">Lista de pagamentos retornada com sucesso</response>
    [HttpGet("estudante/{estudanteId}")]
    [ProducesResponseType(typeof(IEnumerable<PagamentoDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<PagamentoDto>>> ObterPorEstudante([FromRoute] Guid estudanteId)
    {
        _logger.LogInformation("Listando pagamentos do estudante: {EstudanteId}", estudanteId);

        var pagamentos = await _mediator.Send(new ObterPagamentosEstudanteQuery(estudanteId));
        return Ok(pagamentos);
    }
}

