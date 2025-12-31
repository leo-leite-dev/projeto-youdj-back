using YouDj.Application.Features.Dj.Auth.Identity;
using YouDj.Domain.Features.Dj.Entities.User;

namespace YouDj.Application.Abstractions.Identity;

public interface IDjIdentityService
{
    DjIdentity Create(UserDj dj);
}