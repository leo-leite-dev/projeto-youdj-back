using FluentValidation;

namespace YouDj.Application.Features.Player;

public sealed class PlayNowCommandValidator
    : AbstractValidator<PlayNowCommand>
{
    public PlayNowCommandValidator()
    {
        RuleFor(x => x.DjId)
            .NotEmpty().WithMessage("DJ é obrigatório.");

        RuleFor(x => x.ExternalId)
            .NotEmpty().WithMessage("ExternalId é obrigatório.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Título é obrigatório.");

        RuleFor(x => x.Source)
            .NotEmpty().WithMessage("Fonte é obrigatória.");

        RuleFor(x => x.ThumbnailUrl)
            .NotEmpty().WithMessage("Thumbnail é obrigatória.");
    }
}