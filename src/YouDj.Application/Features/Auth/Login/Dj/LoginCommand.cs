using MediatR;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.Auth.Login.Dj;

public sealed record LoginCommand : IRequest<Result<LoginResult>>
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}