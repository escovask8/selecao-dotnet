using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlataformaCursos.Application.DTOs;
using PlataformaCursos.Application.Queries.Cursos;

namespace PlataformaCursos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CursosController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CursosController> _logger;

    public CursosController(IMediator mediator, ILogger<CursosController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Lista todos os cursos ativos disponíveis
    /// </summary>
    /// <returns>Lista de cursos</returns>
    /// <response code="200">Lista de cursos retornada com sucesso</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CursoDto>), StatusCodes.Status200OK)]
    [ResponseCache(Duration = 300)] // Cache de 5 minutos
    public async Task<ActionResult<IEnumerable<CursoDto>>> ObterTodos()
    {
        _logger.LogInformation("Listando todos os cursos");

        var cursos = await _mediator.Send(new ObterTodosCursosQuery());
        return Ok(cursos);
    }
}

