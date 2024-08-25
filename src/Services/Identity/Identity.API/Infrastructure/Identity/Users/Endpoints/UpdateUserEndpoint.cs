using BuildingBlocksClient.Identity.DTOs;
using BuildingBlocksClient.Identity.Interfaces;

namespace Identity.API.Infrastructure.Identity.Users.Endpoints
{
    public static class UpdateUserEndpoint
    {
        public static RouteHandlerBuilder MapUpdateUserEndpoint(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapPut("/", async (GetUserDTO request, IUserService _userService) =>
            {
                return await _userService.UpdateUser(request);
            })
            .WithName(nameof(UpdateUserEndpoint))
            .WithSummary("Updates a user.")
            .WithDescription("Updates a user in the system.");
        }
    }
}
