using BuildingBlocksClient.Starter.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Starter.Infrastructure.Multitenancy;
using Starter.Infrastructure.Multitenancy.Services;
using Starter.Infrastructure.Persistence.Configurations;
using Starter.Infrastructure.Persistence.Interceptors;

namespace Starter.Infrastructure.Persistence
{
    internal static class DependencyInjection
    {
        internal static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services);
            //services.AddHttpContextAccessor();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            //Services
            services.AddScoped<ICurrentTenantService, CurrentTenantService>();
            services.AddTransient<ITenantService, TenantService>();

            //Data Db Context
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

            //Tenant Db Context
            services.AddDbContext<TenantDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            //Database Migration
            services.AddAndMigrateTenantDatabases(configuration);

            //Db Context Interface
            services.AddTransient<IApplicationDbContext, ApplicationDbContext>();

            //EF Core Interceptors
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            return services;
        }
    }
}
