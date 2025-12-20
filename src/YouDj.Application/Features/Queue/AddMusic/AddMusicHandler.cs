using MediatR;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.Queue.AddMusic;

public sealed class AddMusicHandler : IRequestHandler<AddMusicCommand, Result>
{
    private readonly AddMusicUseCase _useCase;

    public AddMusicHandler(AddMusicUseCase useCase)
    {
        _useCase = useCase;
    }

    public Task<Result> Handle(
        AddMusicCommand request, CancellationToken ct)
        => _useCase.ExecuteAsync(request, ct);
}