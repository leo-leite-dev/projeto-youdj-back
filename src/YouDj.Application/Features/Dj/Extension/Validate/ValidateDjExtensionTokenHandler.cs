using MediatR;
using YouDj.Application.Abstractions.Extension;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.Dj.Extension.Validate;

public sealed class ValidateDjExtensionTokenHandler
    : IRequestHandler<ValidateDjExtensionTokenQuery, Result<ValidateDjExtensionTokenResult>>
{
    private readonly IDjExtensionTokenService _tokenService;

    public ValidateDjExtensionTokenHandler(
        IDjExtensionTokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public Task<Result<ValidateDjExtensionTokenResult>> Handle(
        ValidateDjExtensionTokenQuery request,
        CancellationToken ct)
    {
        if (!_tokenService.TryValidate(
            request.Token,
            out var payload))
        {
            return Task.FromResult(
                Result<ValidateDjExtensionTokenResult>
                    .Unauthorized("Token inválido.")
            );
        }

        if (payload.ExpiresAt < DateTime.UtcNow)
        {
            return Task.FromResult(
                Result<ValidateDjExtensionTokenResult>
                    .Unauthorized("Token expirado.")
            );
        }

        return Task.FromResult(
            Result<ValidateDjExtensionTokenResult>.Ok(
                new ValidateDjExtensionTokenResult
                {
                    DjId = payload.DjId
                }
            )
        );
    }
}