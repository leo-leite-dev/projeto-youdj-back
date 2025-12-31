using MediatR;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.Auth.Login.Guest;

public sealed record GuestLoginCommand : IRequest<Result<GuestLoginDto>>
{
    public required string DisplayName { get; init; }
}