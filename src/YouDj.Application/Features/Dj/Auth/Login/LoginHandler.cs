using MediatR;
using YouDj.Application.Abstractions.Auth;
using YouDj.Application.Abstractions.Identity;
using YouDj.Application.Abstractions.Repositories;
using YouDj.Application.Common.Results;
using YouDj.Application.Features.Dj.Auth.Login;
using YouDj.Domain.Features.Common.ValueObjects;
using YouDj.Domain.Features.Users.ValueObjects;
using YouDj.Domain.Features.Dj.Entities.User;
using YouDj.Application.Abstractions.Repositories.Dj.Playlists;
using YouDj.Domain.Features.Dj.Entities.Playlists;

namespace YouDj.Application.Features.Auth.Login;

public sealed class LoginHandler
    : IRequestHandler<LoginCommand, Result<LoginDto>>
{
    private readonly IDjRepository _djRepository;
    private readonly IPlaylistRepository _playlistRepository;
    private readonly IPasswordService _passwordService;
    private readonly IJwtTokenService _tokenService;
    private readonly IDjIdentityService _identityService;

    public LoginHandler(
        IDjRepository djRepository,
        IPlaylistRepository playlistRepository,
        IPasswordService passwordService,
        IJwtTokenService tokenService,
        IDjIdentityService identityService)
    {
        _djRepository = djRepository;
        _playlistRepository = playlistRepository;
        _passwordService = passwordService;
        _tokenService = tokenService;
        _identityService = identityService;
    }

    public async Task<Result<LoginDto>> Handle(
        LoginCommand command, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(command.Identify))
            return Result<LoginDto>.Unauthorized("Credenciais inválidas.");

        UserDj? dj;

        if (Email.TryParse(command.Identify, out var email))
            dj = await _djRepository.GetByEmailAsync(email, ct);
        else if (Username.TryParse(command.Identify, out var username))
            dj = await _djRepository.GetByUsernameAsync(username, ct);
        else
            return Result<LoginDto>.Unauthorized("Credenciais inválidas.");

        if (dj is null ||
            !_passwordService.Verify(command.Password, dj.PasswordHash))
            return Result<LoginDto>.Unauthorized("Credenciais inválidas.");

        if (!dj.IsActive)
            return Result<LoginDto>.Forbidden("Usuário desativado.");

        if (dj.ActivePlaylistId is null)
        {
            var playlist = Playlist.Create(
                dj.Id,
                dj.Username.Value
            );

            await _playlistRepository.AddAsync(playlist, ct);

            dj.SetActivePlaylist(playlist.Id);

            await _djRepository.UpdateAsync(dj, ct);
        }

        var identity = _identityService.Create(dj);

        var token = await _tokenService.IssueAsync(
            dj.Id,
            dj.Username,
            identity.Roles,
            identity.Claims,
            ct
        );

        return Result<LoginDto>.Ok(new LoginDto
        {
            DjId = dj.Id,
            PlaylistId = dj.ActivePlaylistId!.Value,
            AccessToken = token.AccessToken,
            ExpiresAtUtc = token.ExpiresAtUtc,
            IsDj = identity.Claims.TryGetValue("is_dj", out var v) && v == "true"
        });
    }
}