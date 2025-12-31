using YouDj.Application.Features.Dj.Extension.Common;

namespace YouDj.Application.Abstractions.Extension;

public interface IDjExtensionTokenService
{
    string Generate(Guid djId, DateTime expiresAt);
    bool TryValidate(string token, out DjExtensionTokenPayload payload);
}