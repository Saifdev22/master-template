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
            .WithSummary("Get a list of all roles")
            .WithDescription("Retrieve a list of all roles available in the system.");
        }
    }
}
