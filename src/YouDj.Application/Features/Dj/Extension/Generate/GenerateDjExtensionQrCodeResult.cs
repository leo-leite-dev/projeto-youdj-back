namespace YouDj.Application.Features.Dj.Extension.Generate;

public sealed record GenerateDjExtensionQrCodeResult
{
    public string Url { get; init; } = string.Empty;
}