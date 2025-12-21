using MediatR;
using YouDj.Application.Common.Results;
using YouDj.Application.Abstractions.Auth;
using YouDj.Application.Abstractions.Persistences;
using YouDj.Application.Features.Repositories;

namespace YouDj.Application.Features.Auth.Login.Guest;

public sealed class GuestLoginHandler
    : IRequestHandler<GuestLoginCommand, Result<GuestLoginResult>>
{
    private readonly IGuestRepository _guestRepository;
    private readonly IGuestTokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public GuestLoginHandler(
        IGuestRepository guestRepository,
        IGuestTokenService tokenService,
        IUnitOfWork unitOfWork)
    {
        _guestRepository = guestRepository;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GuestLoginResult>> Handle(
        GuestLoginCommand command,
        CancellationToken ct)
    {
        var guest = new Domain.Features.Guests.Guest(command.DisplayName);

        await _guestRepository.AddAsync(guest, ct);
        await _unitOfWork.CommitAsync(ct);

        var token = _tokenService.Issue(guest.Id);

        return Result<GuestLoginResult>.Ok(new GuestLoginResult
        {
            GuestId = guest.Id,
            DisplayName = guest.DisplayName,
            Credits = guest.Credits,
            AccessToken = token.AccessToken,
            ExpiresAtUtc = token.ExpiresAtUtc
        });
    }
}