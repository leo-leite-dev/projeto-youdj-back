using MediatR;
using YouDj.Application.Abstractions.Auth;
using YouDj.Application.Abstractions.Identity;
using YouDj.Application.Abstractions.Repositories;
using YouDj.Application.Common.Results;
using YouDj.Application.Features.Auth.Login.Dj;
using YouDj.Domain.Features.Common.ValueObjects;
using YouDj.Domain.Features.Users.Entities;
using YouDj.Domain.Features.Users.ValueObjects;

namespace YouDj.Domain.Features.Login.Djs;

public sealed class LoginHandler
    : IRequestHandler<LoginCommand, Result<LoginResult>>
{
    private readonly IDjRepository _djRepository;
    private readonly IPasswordService _passwordService;
    private readonly IJwtTokenService _tokenService;
    private readonly IDjIdentityService _identityService;

    public LoginHandler(
        IDjRepository djRepository,
        IPasswordService passwordService,
        IJwtTokenService tokenService,
        IDjIdentityService identityService)
    {
        _djRepository = djRepository;
        _passwordService = passwordService;
        _tokenService = tokenService;
        _identityService = identityService;
    }

    public async Task<Result<LoginResult>> Handle(
        LoginCommand command, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(command.Identify))
            return Result<LoginResult>.Unauthorized("Credenciais inválidas.");

        Dj? dj;

        if (Email.TryParse(command.Identify, out var email))
            dj = await _djRepository.GetByEmailAsync(email, ct);

        else if (Username.TryParse(command.Identify, out var username))
            dj = await _djRepository.GetByUsernameAsync(username, ct);

        else
            return Result<LoginResult>.Unauthorized("Credenciais inválidas.");

        if (dj is null ||
            !_passwordService.Verify(command.Password, dj.PasswordHash))
            return Result<LoginResult>.Unauthorized("Credenciais inválidas.");

        if (!dj.IsActive)
            return Result<LoginResult>.Forbidden("Usuário desativado.");

        var identity = _identityService.Create(dj);

        var token = await _tokenService.IssueAsync(
            dj.Id,
            dj.Username,
            identity.Roles,
            identity.Claims,
            ct
        );

        return Result<LoginResult>.Ok(new LoginResult
        {
            DjId = dj.Id,
            AccessToken = token.AccessToken,
            ExpiresAtUtc = token.ExpiresAtUtc,
            IsDj = identity.Claims.TryGetValue("is_dj", out var v) && v == "true"
        });
    }
}