using FluentValidation;

namespace YouDj.Application.Features.Dj.Playlists.Add;

public sealed class AddMusicToPlaylistValidator
    : AbstractValidator<AddMusicToPlaylistCommand>
{
    public AddMusicToPlaylistValidator()
    {
        RuleFor(x => x.DjId)
            .NotEmpty();

        RuleFor(x => x.PlaylistId)
            .NotEmpty();

        RuleFor(x => x.ExternalId)
            .NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty();

        RuleFor(x => x.ThumbnailUrl)
            .NotEmpty();

        RuleFor(x => x.Source)
            .NotEmpty();
    }
}