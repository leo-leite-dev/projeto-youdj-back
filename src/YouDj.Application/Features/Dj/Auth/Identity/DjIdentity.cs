namespace YouDj.Application.Features.Dj.Auth.Identity;

public sealed record DjIdentity(
    IReadOnlyCollection<string> Roles,
    IReadOnlyDictionary<string, string> Claims
);