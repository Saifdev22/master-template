﻿namespace Identity.API.Infrastructure.Identity.Roles.Endpoints
{
    internal static class EndpointExtension
    {
        public static IEndpointRouteBuilder MapRoleEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGetRoleByIdEndpoint();
            app.MapGetAllRolesEndpoint();
            app.MapCreateOrUpdateRoleEndpoint();
            app.MapDeleteRoleEndpoint();

            return app;
        }
    }
}
