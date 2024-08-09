using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Starter.Infrastructure.Data.Interceptors;
using Starter.Infrastructure.Multitenancy;
using Starter.Infrastructure.Multitenancy.Extensions;
using Starter.Infrastructure.Multitenancy.Services;
using Starter.Infrastructure.Multitenancy.Services.Implementations;

namespace Starter.Infrastructure.Data
{
    internal static class DependencyInjection
    {
        internal static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services);
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddScoped<ICurrentTenantService, CurrentTenantService>();

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorNumbersToAdd: null);
                });
            });

            services.AddDbContext<TenantDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddAndMigrateTenantDatabases(configuration);
            services.AddTransient<IApplicationDbContext, ApplicationDbContext>();
            services.AddTransient<ITenantService, TenantService>();

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            return services;
        }
    }
}
