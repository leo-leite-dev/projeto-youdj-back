using MediatR;
using YouDj.Application.Abstractions.Auth;
using YouDj.Application.Abstractions.Identity;
using YouDj.Application.Abstractions.Repositories;
using YouDj.Application.Common.Results;
using YouDj.Domain.Features.Uasers.ValueObjects;

namespace YouDj.Application.Features.Auth.Login.Dj;

public sealed class LoginHandler
    : IRequestHandler<LoginCommand, Result<LoginResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly IJwtTokenService _tokenService;
    private readonly IUserIdentityService _identityService;

    public LoginHandler(
        IUserRepository userRepository,
        IPasswordService passwordService,
        IJwtTokenService tokenService,
        IUserIdentityService identityService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _tokenService = tokenService;
        _identityService = identityService;
    }

    public async Task<Result<LoginResult>> Handle(
        LoginCommand command, CancellationToken ct)
    {
        if (!Email.TryParse(command.Email, out var email))
            return Result<LoginResult>.Unauthorized("Credenciais inválidas.");

        var user = await _userRepository.GetByEmailAsync(email, ct);

        if (user is null ||
            !_passwordService.Verify(command.Password, user.PasswordHash))
            return Result<LoginResult>.Unauthorized("Credenciais inválidas.");

        var identity = _identityService.Create(user);

        var token = await _tokenService.IssueAsync(
            user.Id,
            user.Username,
            identity.Roles,
            identity.Claims,
            ct
        );

        return Result<LoginResult>.Ok(new LoginResult
        {
            AccessToken = token.AccessToken,
            ExpiresAtUtc = token.ExpiresAtUtc,
            IsDj = identity.Claims["is_dj"] == "true"
        });
    }
}