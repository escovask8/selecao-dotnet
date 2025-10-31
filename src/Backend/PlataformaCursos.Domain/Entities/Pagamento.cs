using Ardalis.GuardClauses;

namespace PlataformaCursos.Domain.Entities;

public class Pagamento
{
    public Guid Id { get; private set; }
    public Guid EstudanteId { get; private set; }
    public Guid? CartaoCreditoId { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime DataPagamento { get; private set; }
    public StatusPagamento Status { get; private set; }
    public string Descricao { get; private set; }
    
    public Estudante? Estudante { get; private set; }
    public CartaoCredito? CartaoCredito { get; private set; }

    private Pagamento() 
    {
        // EF Core constructor
        Descricao = null!;
    }

    public Pagamento(Guid estudanteId, decimal valor, string descricao, Guid? cartaoCreditoId = null)
    {
        Id = Guid.NewGuid();
        EstudanteId = Guard.Against.Default(estudanteId, nameof(estudanteId));
        Valor = Guard.Against.NegativeOrZero(valor, nameof(valor));
        Descricao = Guard.Against.NullOrWhiteSpace(descricao, nameof(descricao));
        CartaoCreditoId = cartaoCreditoId;
        DataPagamento = DateTime.UtcNow;
        Status = StatusPagamento.Pendente;
    }

    public void Aprovar()
    {
        Status = StatusPagamento.Aprovado;
    }

    public void Rejeitar(string? motivo = null)
    {
        Status = StatusPagamento.Rejeitado;
    }
}

public enum StatusPagamento
{
    Pendente = 1,
    Aprovado = 2,
    Rejeitado = 3
}

