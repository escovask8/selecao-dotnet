using MediatR;
using Microsoft.AspNetCore.Mvc;
using PlataformaCursos.Application.Commands.Matriculas;
using PlataformaCursos.Application.Common.Models;
using PlataformaCursos.Application.DTOs;

namespace PlataformaCursos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class MatriculasController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MatriculasController> _logger;

    public MatriculasController(IMediator mediator, ILogger<MatriculasController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Realiza a matrícula de um estudante em um curso
    /// </summary>
    /// <param name="command">Dados da matrícula</param>
    /// <returns>Matrícula realizada</returns>
    /// <response code="200">Matrícula realizada com sucesso</response>
    /// <response code="400">Dados inválidos ou regra de negócio violada</response>
    [HttpPost]
    [ProducesResponseType(typeof(MatriculaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MatriculaDto>> RealizarMatricula([FromBody] RealizarMatriculaCommand command)
    {
        _logger.LogInformation(
            "Tentativa de matrícula - Estudante: {EstudanteId}, Curso: {CursoId}",
            command.EstudanteId,
            command.CursoId);

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error, errors = result.Errors });

        return Ok(result.Value);
    }
}

