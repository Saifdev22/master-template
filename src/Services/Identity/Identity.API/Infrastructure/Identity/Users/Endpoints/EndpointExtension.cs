namespace Identity.API.Infrastructure.Identity.Users.Endpoints
{
    public static class EndpointExtension
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGetAllUsersEndpoint();
            app.MapGetUserByIdEndpoint();
            app.MapCreateUserEndpoint();
            app.MapUpdateUserEndpoint();
            app.MapDeleteUserEndpoint();

            return app;
        }
    }
}
