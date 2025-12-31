using MediatR;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.Dj.Extension.Validate;

public sealed record ValidateDjExtensionTokenQuery(string Token)
    : IRequest<Result<ValidateDjExtensionTokenResult>>;