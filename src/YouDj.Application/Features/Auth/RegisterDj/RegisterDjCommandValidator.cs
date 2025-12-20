using FluentValidation;

namespace YouDj.Application.Features.Auth.RegisterDj;

public sealed class RegisterDjValidator
    : AbstractValidator<RegisterDjCommand>
{
    public RegisterDjValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório.")
            .EmailAddress().WithMessage("Email inválido.");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username é obrigatório.")
            .MinimumLength(3).WithMessage("Username deve ter ao menos 3 caracteres.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Senha é obrigatória.")
            .MinimumLength(8).WithMessage("Senha deve ter no mínimo 8 caracteres.");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("Data de nascimento é obrigatória.");
    }
}