using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using YouDj.Application.Abstractions.Extension;
using YouDj.Application.Features.Dj.Extension.Common;

namespace YouDj.Infrastructure.Extension.Tokens;

public sealed class DjExtensionTokenService : IDjExtensionTokenService
{
    private const string Secret = "DJ_EXTENSION_SECRET_CHANGE_ME";

    public string Generate(Guid djId, DateTime expiresAt)
    {
        var payload = new DjExtensionTokenPayload(djId, expiresAt);

        var json = JsonSerializer.Serialize(payload);
        var signature = Sign(json);

        return $"{ToBase64(json)}.{signature}";
    }

    public bool TryValidate(string token, out DjExtensionTokenPayload payload)
    {
        payload = default!;

        if (string.IsNullOrWhiteSpace(token))
            return false;

        var parts = token.Split('.');
        if (parts.Length != 2)
            return false;

        var json = FromBase64(parts[0]);
        var signature = parts[1];

        if (Sign(json) != signature)
            return false;

        payload = JsonSerializer.Deserialize<DjExtensionTokenPayload>(json)!;

        return payload.ExpiresAt >= DateTime.UtcNow;
    }

    private static string Sign(string value)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(Secret));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(value));
        return Convert.ToBase64String(hash);
    }

    private static string ToBase64(string value)
        => Convert.ToBase64String(Encoding.UTF8.GetBytes(value));

    private static string FromBase64(string value)
        => Encoding.UTF8.GetString(Convert.FromBase64String(value));
}