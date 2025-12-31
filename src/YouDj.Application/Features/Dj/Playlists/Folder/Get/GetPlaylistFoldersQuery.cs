using MediatR;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.Dj.Playlists.Folder.Get;

public sealed record GetPlaylistFoldersQuery
    : IRequest<Result<IReadOnlyList<PlaylistFolderDto>>>
{
    public required Guid DjId { get; init; }
    public required Guid PlaylistId { get; init; }
}