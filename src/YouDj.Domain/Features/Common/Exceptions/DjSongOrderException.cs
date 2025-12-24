using YouDj.Domain.Features.Common.Exceptions;

namespace YouDj.Domain.SongOrders.Exceptions;

public sealed class DjSongOrderException : DomainException
{
    private DjSongOrderException(string message) : base(message) { }

    public static DjSongOrderException InvalidDj()
        => new("DJ inválido.");

    public static DjSongOrderException InvalidGuest()
        => new("Guest inválido.");

    public static DjSongOrderException InvalidPrice()
        => new("Preço em créditos inválido.");

    public static DjSongOrderException ExternalIdRequired()
        => new("ExternalId é obrigatório.");

    public static DjSongOrderException TitleRequired()
        => new("Título é obrigatório.");

    public static DjSongOrderException SourceRequired()
        => new("Fonte é obrigatória.");

    public static DjSongOrderException ThumbnailRequired()
        => new("Thumbnail é obrigatória.");

    public static DjSongOrderException NotPending()
        => new("Pedido não está pendente.");
}