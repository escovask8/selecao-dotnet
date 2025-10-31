using FluentAssertions;
using Moq;
using PlataformaCursos.Application.Commands.Estudantes;
using PlataformaCursos.Application.Common.Models;
using PlataformaCursos.Domain.Entities;
using PlataformaCursos.Domain.Interfaces;
using PlataformaCursos.Domain.ValueObjects;
using Xunit;
using AutoMapper;
using AutoMapper.Configuration;

namespace PlataformaCursos.Tests.Unit.Commands;

public class CadastrarEstudanteCommandHandlerTests
{
    private readonly Mock<IRepository<Estudante>> _repositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly IMapper _mapper;

    public CadastrarEstudanteCommandHandlerTests()
    {
        _repositoryMock = new Mock<IRepository<Estudante>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<Application.Common.Mappings.MappingProfile>();
        });
        _mapper = config.CreateMapper();
    }

    [Fact]
    public async Task Handle_DeveCadastrarEstudante_QuandoDadosValidos()
    {
        // Arrange
        var command = new CadastrarEstudanteCommand("João Silva", "joao@example.com");
        
        _repositoryMock
            .Setup(r => r.ExistsAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Estudante, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _repositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Estudante>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Estudante e, CancellationToken ct) => e);

        _unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new CadastrarEstudanteCommandHandler(
            _repositoryMock.Object,
            _mapper,
            _unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Nome.Should().Be("João Silva");
        result.Value.Email.Should().Be("joao@example.com");

        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Estudante>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_DeveRetornarErro_QuandoEmailJaExiste()
    {
        // Arrange
        var command = new CadastrarEstudanteCommand("João Silva", "joao@example.com");
        
        _repositoryMock
            .Setup(r => r.ExistsAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Estudante, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var handler = new CadastrarEstudanteCommandHandler(
            _repositoryMock.Object,
            _mapper,
            _unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Contain("já cadastrado");
    }
}

