using MediatR;
using PlataformaCursos.Application.DTOs;

namespace PlataformaCursos.Application.Queries.Pagamentos;

public record ObterPagamentosEstudanteQuery(Guid EstudanteId) : IRequest<IEnumerable<PagamentoDto>>;

