using BuildingBlocksClient.Identity.Interfaces;

namespace Identity.API.Infrastructure.Identity.Roles.Endpoints
{
    public static class DeleteRoleEndpoint
    {
        public static RouteHandlerBuilder MapDeleteRoleEndpoint(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapDelete("/{id:guid}", async (string id, IRoleService roleService) =>
            {
                await roleService.DeleteRole(id);
            })
            .WithName(nameof(DeleteRoleEndpoint))
            .WithSummary("Delete role by Id.")
            .WithDescription("Remove a role from the system by its ID.");
        }
    }
}
