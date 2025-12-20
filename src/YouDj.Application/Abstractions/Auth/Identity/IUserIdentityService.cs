using YouDj.Application.Features.Auth.Identity;
using YouDj.Domain.Features.Users.Entities;

namespace YouDj.Application.Abstractions.Identity;

public interface IUserIdentityService
{
    UserIdentity Create(User user);
}
