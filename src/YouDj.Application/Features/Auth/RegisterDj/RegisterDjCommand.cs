using MediatR;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.Auth.RegisterDj;

public sealed record RegisterDjCommand : IRequest<Result<AuthResult>>
{
    public required string Email { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required DateOnly BirthDate { get; init; }
}