using MediatR;
using YouDj.Application.Common.Results;
using YouDj.Application.Abstractions.Auth;
using YouDj.Application.Abstractions.Repositories;
using YouDj.Domain.Features.Users.Entities;
using YouDj.Domain.Features.Common.ValueObjects;
using YouDj.Domain.Features.Common.Exceptions;
using YouDj.Domain.Features.Uasers.ValueObjects;

namespace YouDj.Application.Features.Auth.RegisterDj;

public sealed class RegisterDjHandler
    : IRequestHandler<RegisterDjCommand, Result<AuthResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly IJwtTokenService _tokenService;

    public RegisterDjHandler(
        IUserRepository userRepository,
        IPasswordService passwordService,
        IJwtTokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _tokenService = tokenService;
    }

    public async Task<Result<AuthResult>> Handle(
        RegisterDjCommand command, CancellationToken ct)
    {
        if (!Email.TryParse(command.Email, out var email))
            return Result<AuthResult>.BadRequest("Email inválido.");

        if (!Username.TryParse(command.Username, out var username))
            return Result<AuthResult>.BadRequest("Username inválido.");

        var birthDate = DateOfBirth.Parse(command.BirthDate);

        User user;

        try
        {
            user = User.Create(
                email: email,
                username: username,
                rawPassword: command.Password,
                birthDate: birthDate,
                hash: _passwordService.Hash
            );
        }
        catch (UserException ex)
        {
            return Result<AuthResult>.BadRequest(ex.Message);
        }

        await _userRepository.AddAsync(user, ct);

        var authResult = await _tokenService.IssueAsync(
            userId: user.Id,
            username: user.Username,
            roles: new[] { "dj" },
            ct: ct
        );

        return Result<AuthResult>.Ok(authResult);
    }
}