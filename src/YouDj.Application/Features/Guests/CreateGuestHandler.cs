using MediatR;
using YouDj.Application.Abstractions.Persistences;
using YouDj.Application.Common.Results;
using YouDj.Application.Features.Repositories;
using YouDj.Domain.Features.Guests;

namespace YouDj.Application.Features.Guests;

public sealed class CreateGuestHandler
    : IRequestHandler<CreateGuestCommand, Result<Guest>>
{
    private readonly IGuestRepository _guestRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateGuestHandler(
        IGuestRepository guestRepository,
        IUnitOfWork unitOfWork)
    {
        _guestRepository = guestRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guest>> Handle(
        CreateGuestCommand command,
        CancellationToken ct)
    {
        var guest = new Guest(command.DisplayName);

        await _guestRepository.AddAsync(guest, ct);

        await _unitOfWork.CommitAsync(ct);

        return Result<Guest>.Ok(guest);
    }
}