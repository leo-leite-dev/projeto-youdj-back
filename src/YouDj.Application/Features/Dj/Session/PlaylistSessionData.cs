namespace YouDj.Application.Features.Dj.Auth.Session;

public sealed record PlaylistSessionData(
    string DisplayName,
    Guid DjId,
    Guid PlaylistId
);