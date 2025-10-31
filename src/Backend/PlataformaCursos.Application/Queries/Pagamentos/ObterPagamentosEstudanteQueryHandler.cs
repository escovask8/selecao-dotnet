using AutoMapper;
using MediatR;
using PlataformaCursos.Application.DTOs;
using PlataformaCursos.Domain.Entities;
using PlataformaCursos.Domain.Interfaces;

namespace PlataformaCursos.Application.Queries.Pagamentos;

public class ObterPagamentosEstudanteQueryHandler : IRequestHandler<ObterPagamentosEstudanteQuery, IEnumerable<PagamentoDto>>
{
    private readonly IRepository<Pagamento> _repository;
    private readonly IMapper _mapper;

    public ObterPagamentosEstudanteQueryHandler(IRepository<Pagamento> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PagamentoDto>> Handle(ObterPagamentosEstudanteQuery request, CancellationToken cancellationToken)
    {
        var pagamentos = await _repository.FindAsync(
            p => p.EstudanteId == request.EstudanteId,
            cancellationToken);

        return _mapper.Map<IEnumerable<PagamentoDto>>(pagamentos);
    }
}

