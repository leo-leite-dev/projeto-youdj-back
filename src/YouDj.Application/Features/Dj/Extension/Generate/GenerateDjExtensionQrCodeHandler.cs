using MediatR;
using YouDj.Application.Abstractions.Auth;
using YouDj.Application.Abstractions.Extension;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.Dj.Extension.Generate;

public sealed class GenerateDjExtensionQrCodeHandler
    : IRequestHandler<GenerateDjExtensionQrCodeCommand, Result<GenerateDjExtensionQrCodeResult>>
{
    private readonly ICurrentDj _currentDj;
    private readonly IDjExtensionTokenService _tokenService;

    public GenerateDjExtensionQrCodeHandler(
        ICurrentDj currentDj,
        IDjExtensionTokenService tokenService)
    {
        _currentDj = currentDj;
        _tokenService = tokenService;
    }

    public Task<Result<GenerateDjExtensionQrCodeResult>> Handle(
        GenerateDjExtensionQrCodeCommand _,
        CancellationToken ct)
    {
        var djId = _currentDj.DjId;

        var token = _tokenService.Generate(
            djId,
            DateTime.UtcNow.AddHours(2)
        );

        var url = $"https://app.youdj.com/dj-ext/{token}";

        return Task.FromResult(
            Result<GenerateDjExtensionQrCodeResult>.Ok(
                new GenerateDjExtensionQrCodeResult
                {
                    Url = url
                }
            )
        );
    }
}