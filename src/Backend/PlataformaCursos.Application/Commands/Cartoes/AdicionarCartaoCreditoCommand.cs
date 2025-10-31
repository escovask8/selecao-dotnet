using MediatR;
using PlataformaCursos.Application.Common.Models;
using PlataformaCursos.Application.DTOs;

namespace PlataformaCursos.Application.Commands.Cartoes;

public record AdicionarCartaoCreditoCommand(
    Guid EstudanteId,
    string Numero,
    string NomeTitular,
    DateTime Validade,
    string CVV
) : IRequest<Result<CartaoCreditoDto>>;

