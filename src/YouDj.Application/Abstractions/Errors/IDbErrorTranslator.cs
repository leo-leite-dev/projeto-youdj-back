using YouDj.Infrastructure.Common.Errors;

namespace YouDj.Application.Common.Errors;

public interface IDbErrorTranslator
{
    DbConflictInfo? TranslateUniqueViolation(string? constraintName);
}