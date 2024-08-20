using BuildingBlocksClient.Application.Identity.Interfaces;

namespace Identity.API.Infrastructure.Identity.Users.Endpoints
{
    public static class GetUserByIdEndpoint
    {
        public static RouteHandlerBuilder MapGetUserByIdEndpoint(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapGet("/{id:guid}", async (string id, IUserService _userService) =>
            {
                return await _userService.GetUserById(id);
            })
            .WithName(nameof(GetUserByIdEndpoint))
            .WithSummary("Get user by ID.")
            .WithDescription("Retrieve the details of a user by its ID.");
        }
    }
}
