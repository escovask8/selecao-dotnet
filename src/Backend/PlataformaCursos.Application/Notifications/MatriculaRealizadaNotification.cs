using MediatR;
using PlataformaCursos.Domain.DomainEvents;

namespace PlataformaCursos.Application.Notifications;

public record MatriculaRealizadaNotification(
    Guid MatriculaId,
    Guid EstudanteId,
    Guid CursoId,
    string NomeEstudante,
    string EmailEstudante,
    string TituloCurso
) : INotification
{
    public DateTime OcorreuEm { get; init; } = DateTime.UtcNow;
}

