namespace Identity.API.Infrastructure.Identity.Roles.Endpoints
{
    internal static class EndpointExtension
    {
        public static IEndpointRouteBuilder MapRoleEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGetAllRolesEndpoint();
            app.MapGetRoleByIdEndpoint();
            app.MapCreateOrUpdateRoleEndpoint();
            app.MapDeleteRoleEndpoint();

            return app;
        }
    }
}
