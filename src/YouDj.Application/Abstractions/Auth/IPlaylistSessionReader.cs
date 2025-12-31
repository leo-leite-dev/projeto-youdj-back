using YouDj.Application.Features.Dj.Auth.Session;

public interface IPlaylistSessionReader
{
    PlaylistSessionData Read(string token);
}