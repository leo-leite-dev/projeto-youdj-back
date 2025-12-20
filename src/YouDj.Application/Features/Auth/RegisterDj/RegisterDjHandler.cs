using MediatR;
using YouDj.Application.Abstractions.Auth;
using YouDj.Application.Abstractions.Identity;
using YouDj.Application.Abstractions.Repositories;
using YouDj.Application.Abstractions.Persistences;
using YouDj.Application.Common.Results;
using YouDj.Domain.Features.Common.Exceptions;
using YouDj.Domain.Features.Common.ValueObjects;
using YouDj.Domain.Features.Uasers.ValueObjects;
using YouDj.Domain.Features.Users.Entities;
using YouDj.Domain.Features.Playlists;

namespace YouDj.Application.Features.Auth.RegisterDj;

public sealed class RegisterDjHandler
    : IRequestHandler<RegisterDjCommand, Result<TokenResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPlaylistRepository _playlistRepository;
    private readonly IPasswordService _passwordService;
    private readonly IJwtTokenService _tokenService;
    private readonly IUserIdentityService _identityService;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterDjHandler(
        IUserRepository userRepository,
        IPlaylistRepository playlistRepository,
        IPasswordService passwordService,
        IJwtTokenService tokenService,
        IUserIdentityService identityService,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
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

        User user;
        try
        {
            user = User.Create(
                email,
                username,
                command.Password,
                birthDate,
                _passwordService.Hash);
        }
        catch (UserException ex)
        {
            return Result<TokenResult>.BadRequest(ex.Message);
        }

        await _userRepository.AddAsync(user, ct);

        var playlist = Playlist.Create(
            djId: user.Id,
            djUsername: user.Username.Value
        );

        await _playlistRepository.AddAsync(playlist, ct);

        user.SetActivePlaylist(playlist.Id);
        await _userRepository.UpdateAsync(user, ct);

        await _unitOfWork.CommitAsync(ct);

        var identity = _identityService.Create(user);

        var tokenResult = await _tokenService.IssueAsync(
            user.Id,
            user.Username,
            identity.Roles,
            identity.Claims,
            ct
        );

        return Result<TokenResult>.Ok(tokenResult);
    }
}