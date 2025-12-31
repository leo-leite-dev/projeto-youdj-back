using MediatR;
using YouDj.Application.Abstractions.Auth;
using YouDj.Application.Abstractions.Identity;
using YouDj.Application.Abstractions.Repositories;
using YouDj.Application.Abstractions.Persistences;
using YouDj.Application.Common.Results;
using YouDj.Domain.Features.Common.Exceptions;
using YouDj.Domain.Features.Common.ValueObjects;
using YouDj.Domain.Features.Users.ValueObjects;
using YouDj.Application.Features.Auth;
using YouDj.Domain.Features.Dj.Entities.User;
using YouDj.Application.Abstractions.Repositories.Dj.Playlists;
using YouDj.Domain.Features.Dj.Entities.Playlists;

namespace YouDj.Application.Features.Dj.Auth.RegisterDj;

public sealed class RegisterDjHandler
    : IRequestHandler<RegisterDjCommand, Result<TokenResult>>
{
    private readonly IDjRepository _djRepository;
    private readonly IPlaylistRepository _playlistRepository;
    private readonly IPasswordService _passwordService;
    private readonly IJwtTokenService _tokenService;
    private readonly IDjIdentityService _identityService;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterDjHandler(
        IDjRepository djRepository,
        IPlaylistRepository playlistRepository,
        IPasswordService passwordService,
        IJwtTokenService tokenService,
        IDjIdentityService identityService,
        IUnitOfWork unitOfWork)
    {
        _djRepository = djRepository;
        _playlistRepository = playlistRepository;
        _passwordService = passwordService;
        _tokenService = tokenService;
        _identityService = identityService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TokenResult>> Handle(
        RegisterDjCommand command, CancellationToken ct)
    {
        if (!Email.TryParse(command.Email, out var email))
            return Result<TokenResult>.BadRequest("Email inválido.");

        if (!Username.TryParse(command.Username, out var username))
            return Result<TokenResult>.BadRequest("Username inválido.");

        var birthDate = DateOfBirth.Parse(command.BirthDate);

        UserDj dj;
        try
        {
            dj = UserDj.Create(
                email,
                username,
                command.Password,
                birthDate,
                _passwordService.Hash);
        }
        catch (DjException ex)
        {
            return Result<TokenResult>.BadRequest(ex.Message);
        }

        await _djRepository.AddAsync(dj, ct);

        var playlist = Playlist.Create(
            djId: dj.Id,
            djUsername: dj.Username.Value
        );

        await _playlistRepository.AddAsync(playlist, ct);

        dj.SetActivePlaylist(playlist.Id);
        await _djRepository.UpdateAsync(dj, ct);

        await _unitOfWork.CommitAsync(ct);

        var identity = _identityService.Create(dj);

        var tokenResult = await _tokenService.IssueAsync(
            dj.Id,
            dj.Username,
            identity.Roles,
            identity.Claims,
            ct
        );

        return Result<TokenResult>.Ok(tokenResult);
    }
}