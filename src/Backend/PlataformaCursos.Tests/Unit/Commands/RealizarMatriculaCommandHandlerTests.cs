using FluentAssertions;
using MediatR;
using Moq;
using PlataformaCursos.Application.Commands.Matriculas;
using PlataformaCursos.Application.Common.Models;
using PlataformaCursos.Domain.Entities;
using PlataformaCursos.Domain.Interfaces;
using PlataformaCursos.Domain.ValueObjects;
using Xunit;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.Extensions.Logging;

namespace PlataformaCursos.Tests.Unit.Commands;

public class RealizarMatriculaCommandHandlerTests
{
    private readonly Mock<IRepository<Estudante>> _estudanteRepositoryMock;
    private readonly Mock<IRepository<Curso>> _cursoRepositoryMock;
    private readonly Mock<IRepository<Matricula>> _matriculaRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<ILogger<RealizarMatriculaCommandHandler>> _loggerMock;
    private readonly IMapper _mapper;

    public RealizarMatriculaCommandHandlerTests()
    {
        _estudanteRepositoryMock = new Mock<IRepository<Estudante>>();
        _cursoRepositoryMock = new Mock<IRepository<Curso>>();
        _matriculaRepositoryMock = new Mock<IRepository<Matricula>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mediatorMock = new Mock<IMediator>();
        _loggerMock = new Mock<ILogger<RealizarMatriculaCommandHandler>>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<Application.Common.Mappings.MappingProfile>();
        });
        _mapper = config.CreateMapper();
    }

    [Fact]
    public async Task Handle_DeveRetornarErro_QuandoEstudanteNaoTemPagamentosAprovados()
    {
        // Arrange
        var estudante = new Estudante("João Silva", new Email("joao@example.com"));
        var curso = new Curso("Curso Teste", "Descrição", 100m, 10, "Professor");
        var command = new RealizarMatriculaCommand(estudante.Id, curso.Id);

        _estudanteRepositoryMock
            .Setup(r => r.GetByIdAsync(estudante.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(estudante);

        _cursoRepositoryMock
            .Setup(r => r.GetByIdAsync(curso.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(curso);

        var handler = new RealizarMatriculaCommandHandler(
            _estudanteRepositoryMock.Object,
            _cursoRepositoryMock.Object,
            _matriculaRepositoryMock.Object,
            _mapper,
            _unitOfWorkMock.Object,
            _mediatorMock.Object,
            _loggerMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Contain("pagamento aprovado");
    }
}

