namespace YouDj.Application.Abstractions.Auth;

public sealed record GuestSessionData(
    string DisplayName,
    Guid DjId
);