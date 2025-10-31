using MediatR;
using Microsoft.Extensions.Logging;
using PlataformaCursos.Application.Notifications;

namespace PlataformaCursos.Application.Handlers;

public class MatriculaRealizadaEventHandler : INotificationHandler<MatriculaRealizadaNotification>
{
    private readonly IEmailService _emailService;
    private readonly ILogger<MatriculaRealizadaEventHandler> _logger;

    public MatriculaRealizadaEventHandler(
        IEmailService emailService,
        ILogger<MatriculaRealizadaEventHandler> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    public async Task Handle(MatriculaRealizadaNotification notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Processando evento de matrícula: {MatriculaId} para estudante {EstudanteId} no curso {CursoId}",
            notification.MatriculaId,
            notification.EstudanteId,
            notification.CursoId);

        await _emailService.EnviarEmailConfirmacaoMatricula(
            notification.EmailEstudante,
            notification.NomeEstudante,
            notification.TituloCurso);
    }
}

public interface IEmailService
{
    Task EnviarEmailConfirmacaoMatricula(string email, string nomeEstudante, string tituloCurso);
}

