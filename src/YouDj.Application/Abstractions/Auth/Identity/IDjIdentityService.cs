using YouDj.Application.Features.Auth.Identity;
using YouDj.Domain.Features.Users.Entities;

namespace YouDj.Application.Abstractions.Identity;

public interface IDjIdentityService
{
    DjIdentity Create(Dj dj);
}