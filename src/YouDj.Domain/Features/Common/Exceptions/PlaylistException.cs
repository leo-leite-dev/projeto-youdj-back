using YouDj.Domain.Features.Common.Exceptions;

namespace YouDj.Domain.Playlists.Exceptions;

public sealed class PlaylistException : DomainException
{
    private PlaylistException(string message) : base(message) { }

    public static PlaylistException NameRequired()
        => new("Nome da playlist é obrigatório.");

    public static PlaylistException NotEditable()
        => new("Playlist não pode ser editada neste estado.");

    public static PlaylistException ItemAlreadyAdded()
        => new("Item já existe na playlist.");

    public static PlaylistException ItemNotFound()
        => new("Item não encontrado na playlist.");

    public static PlaylistException InvalidState(string reason)
        => new($"Estado inválido da playlist: {reason}");
}
