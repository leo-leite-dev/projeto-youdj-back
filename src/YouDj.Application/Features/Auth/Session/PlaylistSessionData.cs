namespace YouDj.Application.Features.Auth.Session;

public sealed record PlaylistSessionData(
    string DisplayName,
    Guid DjId,
    Guid PlaylistId
);