using Ardalis.GuardClauses;
using PlataformaCursos.Domain.ValueObjects;

namespace PlataformaCursos.Domain.Entities;

public class Estudante
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public Email Email { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public StatusEstudante Status { get; private set; }
    
    private readonly List<CartaoCredito> _cartoes = new();
    public IReadOnlyCollection<CartaoCredito> Cartoes => _cartoes.AsReadOnly();
    
    private readonly List<Matricula> _matriculas = new();
    public IReadOnlyCollection<Matricula> Matriculas => _matriculas.AsReadOnly();
    
    private readonly List<Pagamento> _pagamentos = new();
    public IReadOnlyCollection<Pagamento> Pagamentos => _pagamentos.AsReadOnly();

    private Estudante() 
    {
        // EF Core constructor
        Nome = null!;
        Email = null!;
    }

    public Estudante(string nome, Email email)
    {
        Id = Guid.NewGuid();
        Nome = Guard.Against.NullOrWhiteSpace(nome, nameof(nome));
        Email = Guard.Against.Null(email, nameof(email));
        DataCadastro = DateTime.UtcNow;
        Status = StatusEstudante.Ativo;
    }

    public void AdicionarCartao(CartaoCredito cartao)
    {
        Guard.Against.Null(cartao, nameof(cartao));
        _cartoes.Add(cartao);
    }

    public void AdicionarMatricula(Matricula matricula)
    {
        Guard.Against.Null(matricula, nameof(matricula));
        _matriculas.Add(matricula);
    }

    public void AdicionarPagamento(Pagamento pagamento)
    {
        Guard.Against.Null(pagamento, nameof(pagamento));
        _pagamentos.Add(pagamento);
    }

    public bool TemPagamentosAprovados()
    {
        return _pagamentos.Any(p => p.Status == StatusPagamento.Aprovado);
    }

    public void Inativar()
    {
        Status = StatusEstudante.Inativo;
    }
}

public enum StatusEstudante
{
    Ativo = 1,
    Inativo = 2
}

