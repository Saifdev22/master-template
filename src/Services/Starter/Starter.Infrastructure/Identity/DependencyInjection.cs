using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Starter.Application.Identity.Users.Abstractions;
using Starter.Infrastructure.Identity.Users;
using Starter.Infrastructure.Identity.Users.Services;

namespace Starter.Infrastructure.Identity
{
    internal static class DependencyInjection
    {
        internal static IServiceCollection ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped(sp => (ICurrentUserInitializer)sp.GetRequiredService<ICurrentUserService>());
            services.AddScoped<UserMiddleware>();

            return services;
        }
    }
}
