using MediatR;
using YouDj.Application.Common.Results;
using YouDj.Application.Features.Auth;

namespace YouDj.Application.Features.Dj.Auth.RegisterDj;

public sealed record RegisterDjCommand : IRequest<Result<TokenResult>>
{
    public required string Email { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required DateOnly BirthDate { get; init; }
}