namespace YouDj.Application.Abstractions.Auth;

public interface IGuestSessionReader
{
    GuestSessionData Read(string sessionToken);
}