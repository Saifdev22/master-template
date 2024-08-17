namespace Identity.API.Infrastructure.Identity.Roles.Endpoints
{
    public static class CreateOrUpdateRoleEndpoint
    {
        public static RouteHandlerBuilder MapCreateOrUpdateRoleEndpoint(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapPost("/", async (RoleDTO request, IRoleService roleService) =>
            {
                return await roleService.CreateOrUpdateRole(request);
            })
            .WithName(nameof(CreateOrUpdateRoleEndpoint))
            .WithSummary("Create or update a role.")
            .WithDescription("Create a new role or update an existing role.");
        }
    }
}
