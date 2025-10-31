using Ardalis.GuardClauses;
using PlataformaCursos.Domain.Common;

namespace PlataformaCursos.Domain.Entities;

public class CartaoCredito
{
    public Guid Id { get; private set; }
    public Guid EstudanteId { get; private set; }
    public string Numero { get; private set; }
    public string NomeTitular { get; private set; }
    public DateTime Validade { get; private set; }
    public string CVV { get; private set; }
    public DateTime DataCadastro { get; private set; }
    
    public Estudante? Estudante { get; private set; }

    private CartaoCredito() 
    {
        // EF Core constructor
        Numero = null!;
        NomeTitular = null!;
        CVV = null!;
    }

    public CartaoCredito(Guid estudanteId, string numero, string nomeTitular, DateTime validade, string cvv)
    {
        Id = Guid.NewGuid();
        EstudanteId = Guard.Against.Default(estudanteId, nameof(estudanteId));
        Numero = Guard.Against.NullOrWhiteSpace(numero, nameof(numero));
        NomeTitular = Guard.Against.NullOrWhiteSpace(nomeTitular, nameof(nomeTitular));
        Validade = Guard.Against.Default(validade, nameof(validade));
        CVV = Guard.Against.NullOrWhiteSpace(cvv, nameof(cvv));
        DataCadastro = DateTime.UtcNow;
        
        Validar();
    }

    private void Validar()
    {
        if (Validade < DateTime.UtcNow.Date)
            throw new DomainException("Cartão de crédito expirado");

        if (CVV.Length != 3 && CVV.Length != 4)
            throw new DomainException("CVV inválido");

        if (Numero.Length < 13 || Numero.Length > 19)
            throw new DomainException("Número do cartão inválido");
    }

    public bool EstaValido()
    {
        return Validade >= DateTime.UtcNow.Date;
    }
}

