namespace YouDj.Infrastructure.Common.Errors;

public sealed record DbConflictInfo(
    string Message,
    string Code,
    string Field
);