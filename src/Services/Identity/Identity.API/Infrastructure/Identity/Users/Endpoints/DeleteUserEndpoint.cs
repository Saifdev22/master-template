using BuildingBlocksClient.Application.Identity.Interfaces;

namespace Identity.API.Infrastructure.Identity.Users.Endpoints
{
    public static class DeleteUserEndpoint
    {
        public static RouteHandlerBuilder MapDeleteUserEndpoint(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapDelete("/{id:guid}", async (string id, IUserService _userService) =>
            {
                await _userService.DeleteUserById(id);
            })
            .WithName(nameof(DeleteUserEndpoint))
            .WithSummary("Delete a user by ID.")
            .WithDescription("Remove a user from the system by its ID.");
        }
    }
}
