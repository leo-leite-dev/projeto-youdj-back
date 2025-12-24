namespace YouDj.Domain.Features.Common.ValueObjects;

public sealed class TrackInfo : IEquatable<TrackInfo>
{
    public string ExternalId { get; }
    public string Title { get; }
    public string ThumbnailUrl { get; }
    public string Source { get; }

    private TrackInfo(string externalId, string title,
        string thumbnailUrl, string source)
    {
        ExternalId = externalId;
        Title = title;
        ThumbnailUrl = thumbnailUrl;
        Source = source;
    }

    public static TrackInfo Create(string externalId, string title,
        string thumbnailUrl, string source)
    {
        if (string.IsNullOrWhiteSpace(externalId))
            throw new ArgumentException("ExternalId é obrigatório.");

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Título é obrigatório.");

        if (string.IsNullOrWhiteSpace(thumbnailUrl))
            throw new ArgumentException("ThumbnailUrl é obrigatório.");

        if (string.IsNullOrWhiteSpace(source))
            throw new ArgumentException("Source é obrigatório.");

        return new TrackInfo(
            externalId.Trim(),
            title.Trim(),
            thumbnailUrl.Trim(),
            source.Trim()
        );
    }

    public bool Equals(TrackInfo? other)
    {
        if (other is null)
            return false;

        return ExternalId == other.ExternalId
            && Title == other.Title
            && ThumbnailUrl == other.ThumbnailUrl
            && Source == other.Source;
    }

    public override bool Equals(object? obj)
        => Equals(obj as TrackInfo);

    public override int GetHashCode()
        => HashCode.Combine(ExternalId, Title, ThumbnailUrl, Source);
}