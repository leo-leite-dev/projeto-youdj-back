namespace YouDj.Application.Features.Dj.Playlists.Folder.Get;

public sealed record PlaylistFolderDto(
    Guid Id,
    string Name,
    int Position
);
