using YouDj.Domain.Features.Common;
using YouDj.Domain.Features.Common.ValueObjects;
using YouDj.Domain.Features.Common.Exceptions;
using YouDj.Domain.Features.Users.Validators;
using YouDj.Domain.Features.Users.ValueObjects;

namespace YouDj.Domain.Features.Users.Entities;

public sealed class Dj : EntityBase
{
    public Username Username { get; private set; }
    public Email Email { get; private set; }
    public DateOfBirth BirthDate { get; private set; }
    public string PasswordHash { get; private set; } = string.Empty;
    public bool IsDj { get; private set; }
    
    public Guid? ActivePlaylistId { get; private set; }

    public string? PasswordResetToken { get; private set; }
    public DateTime? PasswordResetTokenExpiresAt { get; private set; }
    public int TokenVersion { get; private set; } = 0;

    private Dj() { }

    private const int MinimumDjAge = 18;

    public static Dj Create(Email email, Username Username, string rawPassword,
        DateOfBirth birthDate, Func<string, string> hash)
    {
        if (!birthDate.IsAdult(MinimumDjAge))
            throw new DjException("Idade mínima não permitida.");

        var Dj = new Dj
        {
            Email = email,
            Username = Username,
            BirthDate = birthDate,
        };

        Dj.SetPassword(rawPassword, hash);
        return Dj;
    }

    public void SetActivePlaylist(Guid playlistId)
    {
        if (playlistId == Guid.Empty)
            throw new DjException("Playlist inválida.");

        ActivePlaylistId = playlistId;
        Touch();
    }

    public bool ChangeEmail(Email newEmail)
    {
        if (Email.Equals(newEmail))
            return false;

        Email = newEmail;
        Touch();
        IncrementTokenVersion();
        return true;
    }

    public bool RenameDjName(Username newUsername)
    {
        if (Username.Equals(newUsername))
            return false;

        Username = newUsername;
        Touch();
        IncrementTokenVersion();
        return true;
    }

    public void SetPassword(string rawPassword, Func<string, string> hashFunction)
    {
        if (hashFunction is null)
            throw new ArgumentNullException(nameof(hashFunction));

        if (string.IsNullOrWhiteSpace(rawPassword))
            throw new DjException("Senha é obrigatória.");

        PasswordValidator.EnsureStrength(rawPassword);

        PasswordHash = hashFunction(rawPassword);

        PasswordResetToken = null;
        PasswordResetTokenExpiresAt = null;
    }

    public void ChangePassword(string currentRawPassword, string newRawPassword,
        Func<string, bool> verifyPassword, Func<string, string> hashFunction)
    {
        if (verifyPassword is null)
            throw new ArgumentNullException(nameof(verifyPassword));

        if (hashFunction is null)
            throw new ArgumentNullException(nameof(hashFunction));

        if (!IsActive)
            throw new DjException("Usuário inativo não pode alterar senha.");

        if (string.IsNullOrWhiteSpace(currentRawPassword))
            throw new DjException("Senha atual é obrigatória.");
        if (!verifyPassword(currentRawPassword))
            throw new DjException("A senha atual está incorreta.");

        if (string.IsNullOrWhiteSpace(newRawPassword))
            throw new DjException("Nova senha é obrigatória.");

        PasswordValidator.EnsureStrength(newRawPassword);

        var newHash = hashFunction(newRawPassword);
        if (newHash == PasswordHash)
            throw new DjException("A nova senha deve ser diferente da atual.");

        PasswordHash = newHash;

        PasswordResetToken = null;
        PasswordResetTokenExpiresAt = null;

        Touch();
        IncrementTokenVersion();
    }

    public void GeneratePasswordResetToken(Func<string> tokenGenerator, TimeSpan duration)
    {
        if (tokenGenerator is null)
            throw new ArgumentNullException(nameof(tokenGenerator));

        if (duration <= TimeSpan.Zero)
            throw new DjException("Duração inválida para o token.");

        PasswordResetToken = tokenGenerator();
        PasswordResetTokenExpiresAt = DateTime.UtcNow.Add(duration);
    }

    public bool HasActiveResetRequest(DateTime? now = null)
    {
        var t = now ?? DateTime.UtcNow;
        return PasswordResetToken is not null
            && PasswordResetTokenExpiresAt is not null
            && PasswordResetTokenExpiresAt >= t;
    }

    public void ResetPasswordWithToken(string token, string newRawPassword, Func<string, string> hashFunction)
    {
        if (hashFunction is null)
            throw new ArgumentNullException(nameof(hashFunction));

        if (PasswordResetToken is null || PasswordResetTokenExpiresAt is null)
            throw new DjException("Nenhuma solicitação de recuperação de senha foi feita.");

        if (PasswordResetTokenExpiresAt < DateTime.UtcNow)
            throw new DjException("Token expirado.");

        if (!string.Equals(PasswordResetToken, token, StringComparison.Ordinal))
            throw new DjException("Token inválido.");

        SetPassword(newRawPassword, hashFunction);

        Touch();
        IncrementTokenVersion();
    }

    public bool IncrementTokenVersion()
    {
        var before = TokenVersion;
        TokenVersion++;
        return TokenVersion != before;
    }
}