using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Starter.Infrastructure.Multitenancy;
using Starter.Infrastructure.Multitenancy.Extensions;
using Starter.Infrastructure.Multitenancy.Services;
using Starter.Infrastructure.Multitenancy.Services.Implementations;

namespace Starter.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Current tenant service with scoped lifetime (created per each request)
            services.AddScoped<ICurrentTenantService, CurrentTenantService>();

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString);
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
