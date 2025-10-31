using Ardalis.GuardClauses;

namespace PlataformaCursos.Domain.Entities;

public class Curso
{
    public Guid Id { get; private set; }
    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public decimal Preco { get; private set; }
    public int Duracao { get; private set; } // em horas
    public string Professor { get; private set; }
    public bool Ativo { get; private set; }
    public DateTime DataCriacao { get; private set; }
    
    private readonly List<Matricula> _matriculas = new();
    public IReadOnlyCollection<Matricula> Matriculas => _matriculas.AsReadOnly();

    private Curso() 
    {
        // EF Core constructor
        Titulo = null!;
        Descricao = null!;
        Professor = null!;
    }

    public Curso(string titulo, string descricao, decimal preco, int duracao, string professor)
    {
        Id = Guid.NewGuid();
        Titulo = Guard.Against.NullOrWhiteSpace(titulo, nameof(titulo));
        Descricao = Guard.Against.NullOrWhiteSpace(descricao, nameof(descricao));
        Preco = Guard.Against.NegativeOrZero(preco, nameof(preco));
        Duracao = Guard.Against.NegativeOrZero(duracao, nameof(duracao));
        Professor = Guard.Against.NullOrWhiteSpace(professor, nameof(professor));
        Ativo = true;
        DataCriacao = DateTime.UtcNow;
    }

    public void Inativar()
    {
        Ativo = false;
    }

    public void Ativar()
    {
        Ativo = true;
    }

    public void AdicionarMatricula(Matricula matricula)
    {
        Guard.Against.Null(matricula, nameof(matricula));
        _matriculas.Add(matricula);
    }
}

