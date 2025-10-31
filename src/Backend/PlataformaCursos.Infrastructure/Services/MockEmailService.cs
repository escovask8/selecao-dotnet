using Microsoft.Extensions.Logging;
using PlataformaCursos.Application.Handlers;

namespace PlataformaCursos.Infrastructure.Services;

public class MockEmailService : IEmailService
{
    private readonly ILogger<MockEmailService> _logger;

    public MockEmailService(ILogger<MockEmailService> logger)
    {
        _logger = logger;
    }

    public Task EnviarEmailConfirmacaoMatricula(string email, string nomeEstudante, string tituloCurso)
    {
        _logger.LogInformation(
            "📧 [MOCK EMAIL] Enviando email de confirmação de matrícula\n" +
            "   Para: {Email}\n" +
            "   Assunto: Confirmação de Matrícula\n" +
            "   Mensagem: Olá {NomeEstudante}, sua matrícula no curso '{TituloCurso}' foi realizada com sucesso!",
            email,
            nomeEstudante,
            tituloCurso);

        // Em produção, aqui seria feita a integração com serviço real de email
        return Task.CompletedTask;
    }
}

