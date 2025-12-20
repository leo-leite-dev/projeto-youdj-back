namespace YouDj.Application.Features.Auth.Identity;

public sealed record UserIdentity(
    IReadOnlyCollection<string> Roles,
    IReadOnlyDictionary<string, string> Claims
);