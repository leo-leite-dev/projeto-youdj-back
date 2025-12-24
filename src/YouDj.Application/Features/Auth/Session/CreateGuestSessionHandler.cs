using MediatR;
using YouDj.Application.Abstractions.Auth;
using YouDj.Application.Features.Auth.Session;

public sealed class CreateGuestSessionHandler
    : IRequestHandler<CreateGuestSessionCommand, GuestSessionResult>
{
    private readonly IPlaylistSessionReader _playlistReader;
    private readonly IGuestTokenService _guestTokenService;

    public CreateGuestSessionHandler(
        IPlaylistSessionReader playlistReader,
        IGuestTokenService guestTokenService)
    {
        _playlistReader = playlistReader;
        _guestTokenService = guestTokenService;
    }

    public Task<GuestSessionResult> Handle(
        CreateGuestSessionCommand command,
        CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(command.Authorization)
            || !command.Authorization.StartsWith("Bearer "))
            throw new UnauthorizedAccessException();

        var playlistToken =
            command.Authorization["Bearer ".Length..];

        var playlistSession =
            _playlistReader.Read(playlistToken);

        var guestId = Guid.NewGuid();

        var tokenResult =
            _guestTokenService.Issue(guestId);

        var result = new GuestSessionResult
        {
            DisplayName = playlistSession.DisplayName,
            DjId = playlistSession.DjId,
            AccessToken = tokenResult.AccessToken,
            ExpiresAtUtc = tokenResult.ExpiresAtUtc
        };

        return Task.FromResult(result);
    }
}