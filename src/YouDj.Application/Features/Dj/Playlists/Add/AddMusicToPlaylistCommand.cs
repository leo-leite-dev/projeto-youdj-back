using MediatR;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.Dj.Playlists.Add;

public sealed record AddMusicToPlaylistCommand : IRequest<Result<Guid>>
{
    public required Guid DjId { get; init; }
    public required Guid PlaylistId { get; init; }
    public Guid? GuestId { get; init; }

    public Guid? FolderId { get; init; }

    public required string ExternalId { get; init; }
    public required string Title { get; init; }
    public required string ThumbnailUrl { get; init; }
    public required string Source { get; init; }
}