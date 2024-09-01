using BuildingBlocksClient.Identity.Interfaces;

namespace Identity.API.Infrastructure.Identity.Roles.Endpoints
{
    public static class GetAllRolesEndpoint
    {
        public static RouteHandlerBuilder MapGetAllRolesEndpoint(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapGet("/", async (IRoleService roleService) =>
            {
                return await roleService.GetAllRoles();
            })
            .WithName(nameof(GetAllRolesEndpoint))
            .WithSummary("Get all dragon roles.")
            .WithDescription("Get a list of dragon z all roles.");
        }
    }
}
