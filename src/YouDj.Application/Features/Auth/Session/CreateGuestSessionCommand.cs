using MediatR;
using YouDj.Application.Features.Auth.Session;

public sealed record CreateGuestSessionCommand(string Authorization)
    : IRequest<GuestSessionResult>;

