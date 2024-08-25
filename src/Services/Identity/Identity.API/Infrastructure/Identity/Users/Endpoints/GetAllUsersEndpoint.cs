using BuildingBlocksClient.Identity.Interfaces;

namespace Identity.API.Infrastructure.Identity.Users.Endpoints
{
    public static class GetAllUsersEndpoint
    {
        public static RouteHandlerBuilder MapGetAllUsersEndpoint(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapGet("/", async (IUserService _userService) =>
            {
                return await _userService.GetAllUsers();
            })
            .WithName(nameof(GetAllUsersEndpoint))
            .WithSummary("Get a list of all users.")
            .WithDescription("Retrieve a list of all users available in the system.");
        }
    }
}
