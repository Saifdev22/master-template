using BuildingBlocksClient.Shared.Interfaces;
using Identity.API.Infrastructure.Identity;
using Identity.API.Infrastructure.Storage;

namespace Identity.API.Infrastructure
{
    public static class DependencyInjection
    {
        public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IFileService, FileService>();

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
