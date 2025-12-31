using MediatR;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.Dj.Auth.Login;

public sealed record LoginCommand : IRequest<Result<LoginDto>>
{
    public required string Identify { get; init; }
    public required string Password { get; init; }
}