using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PlataformaCursos.Application.Common.Models;
using PlataformaCursos.Application.DTOs;
using PlataformaCursos.Application.Notifications;
using PlataformaCursos.Domain.DomainEvents;
using PlataformaCursos.Domain.Entities;
using PlataformaCursos.Domain.Interfaces;

namespace PlataformaCursos.Application.Commands.Matriculas;

public class RealizarMatriculaCommandHandler : IRequestHandler<RealizarMatriculaCommand, Result<MatriculaDto>>
{
    private readonly IRepository<Estudante> _estudanteRepository;
    private readonly IRepository<Curso> _cursoRepository;
    private readonly IRepository<Matricula> _matriculaRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;
    private readonly ILogger<RealizarMatriculaCommandHandler> _logger;

    public RealizarMatriculaCommandHandler(
        IRepository<Estudante> estudanteRepository,
        IRepository<Curso> cursoRepository,
        IRepository<Matricula> matriculaRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IMediator mediator,
        ILogger<RealizarMatriculaCommandHandler> logger)
    {
        _estudanteRepository = estudanteRepository;
        _cursoRepository = cursoRepository;
        _matriculaRepository = matriculaRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _mediator = mediator;
        _logger = logger;
    }

    public async Task<Result<MatriculaDto>> Handle(RealizarMatriculaCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var estudante = await _estudanteRepository.GetByIdAsync(request.EstudanteId, cancellationToken);
            if (estudante == null)
                return Result<MatriculaDto>.Failure("Estudante não encontrado");

            var curso = await _cursoRepository.GetByIdAsync(request.CursoId, cancellationToken);
            if (curso == null)
                return Result<MatriculaDto>.Failure("Curso não encontrado");

            if (!curso.Ativo)
                return Result<MatriculaDto>.Failure("Curso está inativo");

            if (!estudante.TemPagamentosAprovados())
                return Result<MatriculaDto>.Failure("É necessário ter pelo menos um pagamento aprovado para realizar matrícula");

            var matriculaExistente = await _matriculaRepository.ExistsAsync(
                m => m.EstudanteId == request.EstudanteId && 
                     m.CursoId == request.CursoId && 
                     m.Status == StatusMatricula.Ativa,
                cancellationToken);

            if (matriculaExistente)
                return Result<MatriculaDto>.Failure("Estudante já possui matrícula ativa neste curso");

            var matricula = new Matricula(request.EstudanteId, request.CursoId);
            estudante.AdicionarMatricula(matricula);
            curso.AdicionarMatricula(matricula);

            await _matriculaRepository.AddAsync(matricula, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var notification = new MatriculaRealizadaNotification(
                matricula.Id,
                estudante.Id,
                curso.Id,
                estudante.Nome,
                estudante.Email.Value,
                curso.Titulo);

            await _mediator.Publish(notification, cancellationToken);

            var dto = _mapper.Map<MatriculaDto>(matricula);
            dto.NomeEstudante = estudante.Nome;
            dto.TituloCurso = curso.Titulo;

            return Result<MatriculaDto>.Success(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao realizar matrícula");
            return Result<MatriculaDto>.Failure(ex.Message);
        }
    }
}

