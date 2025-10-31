using MediatR;
using PlataformaCursos.Application.DTOs;

namespace PlataformaCursos.Application.Queries.Cursos;

public record ObterTodosCursosQuery() : IRequest<IEnumerable<CursoDto>>;

