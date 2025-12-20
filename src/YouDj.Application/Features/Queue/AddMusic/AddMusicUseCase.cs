using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.Queue.AddMusic;

public sealed class AddMusicUseCase
{
    public async Task<Result> ExecuteAsync(
        AddMusicCommand command, CancellationToken ct)
    {
        return Result.Ok();
    }
}