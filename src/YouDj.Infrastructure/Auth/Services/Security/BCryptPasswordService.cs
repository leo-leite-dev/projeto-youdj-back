using YouDj.Application.Abstractions.Auth;

namespace YouDj.Infrastructure.Auth.Services.Security;

public sealed class PasswordService : IPasswordService
{
    private const int WorkFactor = 12;

    public string Hash(string rawPassword)
        => BCrypt.Net.BCrypt.HashPassword(rawPassword, workFactor: WorkFactor);

    public bool Verify(string rawPassword, string passwordHash)
        => BCrypt.Net.BCrypt.Verify(rawPassword, passwordHash);
}