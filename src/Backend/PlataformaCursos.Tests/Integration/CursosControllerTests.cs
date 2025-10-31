using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using PlataformaCursos.API;
using PlataformaCursos.Application.DTOs;
using Xunit;

namespace PlataformaCursos.Tests.Integration;

public class CursosControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CursosControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ObterTodos_DeveRetornarListaDeCursos()
    {
        // Act
        var response = await _client.GetAsync("/api/cursos");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var cursos = await response.Content.ReadFromJsonAsync<IEnumerable<CursoDto>>();
        cursos.Should().NotBeNull();
        cursos.Should().NotBeEmpty();
    }
}

