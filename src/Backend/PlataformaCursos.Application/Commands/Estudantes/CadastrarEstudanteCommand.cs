using MediatR;
using PlataformaCursos.Application.Common.Models;
using PlataformaCursos.Application.DTOs;

namespace PlataformaCursos.Application.Commands.Estudantes;

public record CadastrarEstudanteCommand(string Nome, string Email) : IRequest<Result<EstudanteDto>>;

