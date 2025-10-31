using FluentValidation;

namespace PlataformaCursos.Application.Commands.Estudantes;

public class CadastrarEstudanteCommandValidator : AbstractValidator<CadastrarEstudanteCommand>
{
    public CadastrarEstudanteCommandValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MinimumLength(3).WithMessage("Nome deve ter no mínimo 3 caracteres")
            .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório")
            .EmailAddress().WithMessage("Email inválido");
    }
}

