using Identity.API.Infrastructure.Identity;
using Identity.API.Infrastructure.Jwt;

namespace Identity.API.Infrastructure
{
    public static class DependencyInjection
    {
        public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
        {
            builder.Services.ConfigureJwt(builder.Configuration);
            builder.Services.ConfigureIdentity(builder.Configuration);
            builder.Services.AddAntiforgery();
            return builder;
        }

        public static WebApplication UseInfrastructure(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAntiforgery();
            app.MapIdentityEndpoints();

            return app;
        }
    }
}
