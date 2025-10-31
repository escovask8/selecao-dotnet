namespace PlataformaCursos.Domain.DomainEvents;

public record MatriculaRealizadaEvent(
    Guid MatriculaId,
    Guid EstudanteId,
    Guid CursoId,
    string NomeEstudante,
    string EmailEstudante,
    string TituloCurso
) : IDomainEvent
{
    public DateTime OcorreuEm { get; init; } = DateTime.UtcNow;
}

