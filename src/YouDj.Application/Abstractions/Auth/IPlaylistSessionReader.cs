using YouDj.Application.Features.Auth.Session;

public interface IPlaylistSessionReader
{
    PlaylistSessionData Read(string token);
}