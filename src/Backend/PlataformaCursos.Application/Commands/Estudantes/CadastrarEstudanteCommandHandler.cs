using AutoMapper;
using MediatR;
using PlataformaCursos.Application.Common.Models;
using PlataformaCursos.Application.DTOs;
using PlataformaCursos.Domain.Entities;
using PlataformaCursos.Domain.Interfaces;

namespace PlataformaCursos.Application.Commands.Estudantes;

public class CadastrarEstudanteCommandHandler : IRequestHandler<CadastrarEstudanteCommand, Result<EstudanteDto>>
{
    private readonly IRepository<Estudante> _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CadastrarEstudanteCommandHandler(
        IRepository<Estudante> repository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<EstudanteDto>> Handle(CadastrarEstudanteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var email = new Domain.ValueObjects.Email(request.Email);
            
            var emailExiste = await _repository.ExistsAsync(
                e => e.Email.Value == email.Value,
                cancellationToken);

            if (emailExiste)
                return Result<EstudanteDto>.Failure("Email já cadastrado");

            var estudante = new Estudante(request.Nome, email);
            await _repository.AddAsync(estudante, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<EstudanteDto>(estudante);
            return Result<EstudanteDto>.Success(dto);
        }
        catch (Exception ex)
        {
            return Result<EstudanteDto>.Failure(ex.Message);
        }
    }
}

