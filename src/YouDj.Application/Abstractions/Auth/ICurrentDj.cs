
using YouDj.Domain.Features.Common.ValueObjects;

namespace YouDj.Application.Abstractions.Auth;

public interface ICurrentDj
{
    bool IsAuthenticated { get; }
    Guid DjId { get; }
    Guid PlaylistId { get; }
    Username Username { get; }
    string? Email { get; }
}