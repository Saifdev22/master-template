using BuildingBlocksClient.Application.Identity.Interfaces;

namespace Identity.API.Infrastructure.Identity.Roles.Endpoints
{
    public static class GetRoleByIdEndpoint
    {
        public static RouteHandlerBuilder MapGetRoleByIdEndpoint(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapGet("/{id:guid}", async (string id, IRoleService roleService) =>
            {
                return await roleService.GetRoleById(id);
            })
            .WithName(nameof(GetRoleByIdEndpoint))
            .WithSummary("Get role by ID.")
            .WithDescription("Retrieve the details of a role by its ID.");
        }
    }
}
