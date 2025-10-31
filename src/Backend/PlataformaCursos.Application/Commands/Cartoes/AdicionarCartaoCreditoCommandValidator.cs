using FluentValidation;

namespace PlataformaCursos.Application.Commands.Cartoes;

public class AdicionarCartaoCreditoCommandValidator : AbstractValidator<AdicionarCartaoCreditoCommand>
{
    public AdicionarCartaoCreditoCommandValidator()
    {
        RuleFor(x => x.EstudanteId)
            .NotEmpty().WithMessage("EstudanteId é obrigatório");

        RuleFor(x => x.Numero)
            .NotEmpty().WithMessage("Número do cartão é obrigatório")
            .Matches(@"^\d{13,19}$").WithMessage("Número do cartão inválido");

        RuleFor(x => x.NomeTitular)
            .NotEmpty().WithMessage("Nome do titular é obrigatório")
            .MinimumLength(3).WithMessage("Nome do titular deve ter no mínimo 3 caracteres");

        RuleFor(x => x.Validade)
            .NotEmpty().WithMessage("Validade é obrigatória")
            .Must(validade => validade >= DateTime.UtcNow.Date)
            .WithMessage("Cartão de crédito expirado");

        RuleFor(x => x.CVV)
            .NotEmpty().WithMessage("CVV é obrigatório")
            .Matches(@"^\d{3,4}$").WithMessage("CVV inválido");
    }
}

