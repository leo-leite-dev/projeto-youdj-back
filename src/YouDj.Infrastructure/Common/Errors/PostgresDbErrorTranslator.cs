using System.Text.RegularExpressions;
using YouDj.Application.Common.Errors;
using YouDj.Application.Common.Results;

namespace YouDj.Infrastructure.Common.Errors;

public sealed class PostgresDbErrorTranslator : IDbErrorTranslator
{
    private static readonly IReadOnlyDictionary<string, string> Explicit =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            ["ux_users_email"] = "E-mail já cadastrado.",
            ["ux_users_username"] = "Username já em uso."
        };

    public string? TryTranslateUniqueViolation(
        string? constraintName,
        string? detail = null)
    {
        if (!string.IsNullOrWhiteSpace(constraintName))
        {
            if (Explicit.TryGetValue(constraintName, out var message))
                return message;

            if (constraintName.StartsWith("ux_", StringComparison.OrdinalIgnoreCase))
            {
                var parts = constraintName.Split(
                    '_',
                    StringSplitOptions.RemoveEmptyEntries
                );

                if (parts.Length >= 3)
                {
                    var friendlyColumns = string.Join(
                        ", ",
                        parts.Skip(2).Select(Humanize)
                    );

                    return $"Já existe registro com {friendlyColumns} informado(s).";
                }
            }
        }

        return TryFromDetail(detail) ?? "Registro duplicado.";
    }

    public DbConflictInfo TranslateUniqueViolation(string? constraintName)
    {
        return constraintName?.ToLowerInvariant() switch
        {
            "ux_users_email" => new DbConflictInfo(
                "E-mail já cadastrado.",
                ResultCodes.Conflict.Email,
                "email"
            ),

            "ux_users_username" => new DbConflictInfo(
                "Username já em uso.",
                ResultCodes.Conflict.Username,
                "username"
            ),

            _ => new DbConflictInfo(
                "Registro duplicado.",
                ResultCodes.Conflict.Generic,
                null
            )
        };
    }

    private static string? TryFromDetail(string? detail)
    {
        if (string.IsNullOrWhiteSpace(detail))
            return null;

        var match = Regex.Match(
            detail,
            @"Key\s+\((?<cols>[^)]+)\)\s*=\s*\((?<vals>[^)]*)\)"
        );

        if (!match.Success)
            return null;

        var columns = match.Groups["cols"].Value
            .Split(',', StringSplitOptions.TrimEntries);

        var friendly = string.Join(", ", columns.Select(Humanize));

        return $"Já existe registro com {friendly} informado(s).";
    }

    private static string Humanize(string token)
    {
        var parts = token
            .Replace("\"", "")
            .Replace("'", "")
            .ToLowerInvariant()
            .Split(new[] { '_', ' ' }, StringSplitOptions.RemoveEmptyEntries);

        return string.Join(
            ' ',
            parts.Select(p => char.ToUpperInvariant(p[0]) + p[1..])
        );
    }
}