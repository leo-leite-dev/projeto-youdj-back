using MediatR;
using YouDj.Application.Common.Results;

namespace YouDj.Application.Features.Dj.Extension.Generate;

public sealed record GenerateDjExtensionQrCodeCommand
    : IRequest<Result<GenerateDjExtensionQrCodeResult>>;