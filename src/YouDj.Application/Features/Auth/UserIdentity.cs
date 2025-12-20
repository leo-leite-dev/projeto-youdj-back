namespace YouDj.Application.Auth;

public sealed class UserIdentity
{
    public IEnumerable<string> Roles { get; init; } = Array.Empty<string>();
    public IDictionary<string, string> Claims { get; init; }
        = new Dictionary<string, string>();
}