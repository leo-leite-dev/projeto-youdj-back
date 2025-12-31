using FluentValidation;

namespace YouDj.Application.Features.Dj.Playlists.Folder.Create;

public sealed class CreatePlaylistFolderValidator
    : AbstractValidator<CreatePlaylistFolderCommand>
{
    public CreatePlaylistFolderValidator()
    {
        RuleFor(x => x.DjId)
            .NotEmpty();

        RuleFor(x => x.PlaylistId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(80);
    }
}