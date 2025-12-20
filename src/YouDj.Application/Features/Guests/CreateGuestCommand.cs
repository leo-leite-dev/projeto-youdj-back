using MediatR;
using YouDj.Application.Common.Results;
using YouDj.Domain.Features.Guests;

namespace YouDj.Application.Features.Guests;

public sealed record CreateGuestCommand
    : IRequest<Result<Guest>>
{
    public required string DisplayName { get; init; }
}
