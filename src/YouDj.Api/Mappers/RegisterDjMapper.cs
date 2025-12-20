using YouDj.Api.Requests;
using YouDj.Application.Features.Auth.RegisterDj;

namespace YouDj.Api.Mappers;

public static class RegisterDjMapper
{
    public static RegisterDjCommand ToCommand(this RegisterDjRequest req)
        => new()
        {
            Email = req.Email,
            Username = req.Username,
            Password = req.Password,
            BirthDate = req.BirthDate
        };
}