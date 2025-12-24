namespace YouDj.Application.Features.Auth.Identity;

public sealed record DjIdentity(
    IReadOnlyCollection<string> Roles,
    IReadOnlyDictionary<string, string> Claims
);