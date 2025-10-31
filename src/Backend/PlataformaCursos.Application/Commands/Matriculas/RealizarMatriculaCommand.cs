using MediatR;
using PlataformaCursos.Application.Common.Models;
using PlataformaCursos.Application.DTOs;

namespace PlataformaCursos.Application.Commands.Matriculas;

public record RealizarMatriculaCommand(Guid EstudanteId, Guid CursoId) : IRequest<Result<MatriculaDto>>;

