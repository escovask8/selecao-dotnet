using Ardalis.GuardClauses;

namespace PlataformaCursos.Domain.Entities;

public class Matricula
{
    public Guid Id { get; private set; }
    public Guid EstudanteId { get; private set; }
    public Guid CursoId { get; private set; }
    public DateTime DataMatricula { get; private set; }
    public StatusMatricula Status { get; private set; }
    
    public Estudante? Estudante { get; private set; }
    public Curso? Curso { get; private set; }

    private Matricula() { } // EF Core

    public Matricula(Guid estudanteId, Guid cursoId)
    {
        Id = Guid.NewGuid();
        EstudanteId = Guard.Against.Default(estudanteId, nameof(estudanteId));
        CursoId = Guard.Against.Default(cursoId, nameof(cursoId));
        DataMatricula = DateTime.UtcNow;
        Status = StatusMatricula.Ativa;
    }

    public void Cancelar()
    {
        Status = StatusMatricula.Cancelada;
    }

    public void Concluir()
    {
        Status = StatusMatricula.Concluida;
    }
}

public enum StatusMatricula
{
    Ativa = 1,
    Cancelada = 2,
    Concluida = 3
}

