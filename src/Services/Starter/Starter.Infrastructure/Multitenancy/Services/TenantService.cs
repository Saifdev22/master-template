using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Starter.Infrastructure.Multitenancy.Dtos;
using Starter.Infrastructure.Multitenancy.Services.Implementations;

namespace Starter.Infrastructure.Multitenancy.Services
{
    public class TenantService : ITenantService
    {

        private readonly TenantDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public TenantService(TenantDbContext context, IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _context = context;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }

        public Tenant CreateTenant(CreateTenantRequest request)
        {

            string newConnectionString = null!;
            if (request.Isolated == true)
            {
                // generate a connection string for new tenant database
                string defaultDbName = _configuration.GetConnectionString("DatabaseName")!;
                string dbName = defaultDbName + "-" + request.Id;
                string defaultConnectionString = _configuration.GetConnectionString("DefaultConnection")!;
                newConnectionString = defaultConnectionString.Replace(defaultDbName, dbName);

                // create a new tenant database and bring current with any pending migrations from ApplicationDbContext
                try
                {
                    using IServiceScope scopeTenant = _serviceProvider.CreateScope();
                    ApplicationDbContext dbContext = scopeTenant.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    dbContext.Database.SetConnectionString(newConnectionString);
                    if (dbContext.Database.GetPendingMigrations().Any())
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"Applying ApplicationDB Migrations for New '{request.Id}' tenant.");
                        Console.ResetColor();
                        dbContext.Database.Migrate();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }


            Tenant tenant = new() // create a new tenant entity
            {
                Id = request.Id!,
                Name = request.Name,
                ConnectionString = newConnectionString,
            };

            _context.Add(tenant);
            _context.SaveChanges();

            return tenant;
        }
    }
}
