using System.Security.Claims;

namespace Starter.Application.Identity.Users.Abstractions
{
    public interface ICurrentUserInitializer
    {
        void SetCurrentUser(ClaimsPrincipal user);

        void SetCurrentUserId(string userId);
    }
}
