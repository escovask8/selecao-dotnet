using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using PlataformaCursos.API;
using PlataformaCursos.Application.DTOs;
using Xunit;

namespace PlataformaCursos.Tests.Integration;

public class EstudantesControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public EstudantesControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CadastrarEstudante_DeveRetornarSucesso_QuandoDadosValidos()
    {
        // Arrange
        var command = new
        {
            nome = "Teste Integração",
            email = "teste.integracao@example.com"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/estudantes/cadastro", command);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var estudante = await response.Content.ReadFromJsonAsync<EstudanteDto>();
        estudante.Should().NotBeNull();
        estudante!.Nome.Should().Be("Teste Integração");
        estudante.Email.Should().Be("teste.integracao@example.com");
    }

    [Fact]
    public async Task CadastrarEstudante_DeveRetornarBadRequest_QuandoEmailInvalido()
    {
        // Arrange
        var command = new
        {
            nome = "Teste",
            email = "email-invalido"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/estudantes/cadastro", command);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}

