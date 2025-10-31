using AutoMapper;
using MediatR;
using PlataformaCursos.Application.Common.Models;
using PlataformaCursos.Application.DTOs;
using PlataformaCursos.Domain.Entities;
using PlataformaCursos.Domain.Interfaces;

namespace PlataformaCursos.Application.Commands.Cartoes;

public class AdicionarCartaoCreditoCommandHandler : IRequestHandler<AdicionarCartaoCreditoCommand, Result<CartaoCreditoDto>>
{
    private readonly IRepository<Estudante> _estudanteRepository;
    private readonly IRepository<CartaoCredito> _cartaoRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AdicionarCartaoCreditoCommandHandler(
        IRepository<Estudante> estudanteRepository,
        IRepository<CartaoCredito> cartaoRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _estudanteRepository = estudanteRepository;
        _cartaoRepository = cartaoRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CartaoCreditoDto>> Handle(AdicionarCartaoCreditoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var estudante = await _estudanteRepository.GetByIdAsync(request.EstudanteId, cancellationToken);
            if (estudante == null)
                return Result<CartaoCreditoDto>.Failure("Estudante não encontrado");

            var cartao = new CartaoCredito(
                request.EstudanteId,
                request.Numero,
                request.NomeTitular,
                request.Validade,
                request.CVV);

            estudante.AdicionarCartao(cartao);
            await _cartaoRepository.AddAsync(cartao, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<CartaoCreditoDto>(cartao);
            return Result<CartaoCreditoDto>.Success(dto);
        }
        catch (Exception ex)
        {
            return Result<CartaoCreditoDto>.Failure(ex.Message);
        }
    }
}

