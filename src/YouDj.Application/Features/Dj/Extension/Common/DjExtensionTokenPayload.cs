namespace YouDj.Application.Features.Dj.Extension.Common;

public sealed record DjExtensionTokenPayload(
    Guid DjId,
    DateTime ExpiresAt
);
