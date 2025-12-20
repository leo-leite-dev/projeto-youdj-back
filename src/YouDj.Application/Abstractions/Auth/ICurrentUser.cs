
using YouDj.Domain.Features.Common.ValueObjects;

namespace YouDj.Application.Abstractions.Auth;

public interface ICurrentUser
{
    bool IsAuthenticated { get; }
    Guid UserId { get; }
    Username Username { get; }
    string? Email { get; }
}