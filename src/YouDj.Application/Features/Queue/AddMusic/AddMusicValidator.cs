using FluentValidation;

namespace YouDj.Application.Features.Queue.AddMusic;

public sealed class AddMusicValidator
    : AbstractValidator<AddMusicCommand>
{
    public AddMusicValidator()
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