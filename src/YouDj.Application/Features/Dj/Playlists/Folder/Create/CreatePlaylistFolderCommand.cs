using MediatR;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.Dj.Playlists.Folder.Create;

public sealed record CreatePlaylistFolderCommand : IRequest<Result<Guid>>
{
    public required Guid DjId { get; init; }
    public required Guid PlaylistId { get; init; }

    public required string Name { get; init; }
}