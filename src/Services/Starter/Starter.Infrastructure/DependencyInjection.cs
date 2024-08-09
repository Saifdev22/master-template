using Microsoft.AspNetCore.Builder;
using Starter.Infrastructure.Auth;
using Starter.Infrastructure.Identity;

namespace Starter.Infrastructure
{
    public static class DependencyInjection
    {
        public static WebApplicationBuilder AddInfrastructureServices(this WebApplicationBuilder builder)
        {

            builder.Services.ConfigureIdentity(builder.Configuration);
            builder.Services.ConfigureDatabase(builder.Configuration);

            return builder;
        }
        public static WebApplication UseInfrastructureServices(this WebApplication app)
        {
            app.UseMiddleware<UserMiddleware>();
            app.UseMiddleware<TenantMiddleware>();

            return app;
        }
    }
}