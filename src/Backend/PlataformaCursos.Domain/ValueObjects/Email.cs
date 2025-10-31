using System.Text.RegularExpressions;
using PlataformaCursos.Domain.Common;

namespace PlataformaCursos.Domain.ValueObjects;

public sealed record Email
{
    public string Value { get; private set; }

    private Email() 
    {
        // EF Core constructor
        Value = null!;
    }

    public Email(string value)
    {
        Value = ValidarEmail(value);
    }

    private static string ValidarEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("Email não pode ser vazio");

        var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
        if (!emailRegex.IsMatch(email))
            throw new DomainException("Email inválido");

        return email.Trim().ToLowerInvariant();
    }

    public static implicit operator string(Email email) => email.Value;
    public static implicit operator Email(string email) => new(email);
    
    public override string ToString() => Value;
}

