using FluentValidation;

namespace PlataformaCursos.Application.Commands.Matriculas;

public class RealizarMatriculaCommandValidator : AbstractValidator<RealizarMatriculaCommand>
{
    public RealizarMatriculaCommandValidator()
    {
        RuleFor(x => x.EstudanteId)
            .NotEmpty().WithMessage("EstudanteId é obrigatório");

        RuleFor(x => x.CursoId)
            .NotEmpty().WithMessage("CursoId é obrigatório");
    }
}

