using BuildingBlocksClient.Identity.DTOs;
using BuildingBlocksClient.Starter.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Starter.Domain.Models;
using static BuildingBlocksClient.Starter.DTOs.TenantDTO;

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

        public async Task<GetAllTenants> CreateTenant(CreateTenant request)
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


            Tenant tenant = new()
            {
                Id = request.Id,
                Name = request.Name,
                ConnectionString = newConnectionString,
            };

            await _context.AddAsync(tenant);
            await _context.SaveChangesAsync();

            GetAllTenants tenantobj = new GetAllTenants
            {
                Id = tenant.Id,
                SubscriptionLevel = tenant.SubscriptionLevel,
                ConnectionString = tenant.ConnectionString,
                Name = tenant.Name
            };

            return tenantobj;
        }

        public async Task<List<GetAllTenants>> GetAllTenants()
        {
            var tenants = await _context.Tenants.ToListAsync();

            var tenantObj = tenants.Select(c => new GetAllTenants
            {
                Id = c.Id,
                Name = c.Name,
                SubscriptionLevel = c.SubscriptionLevel,
                ConnectionString = c.ConnectionString

            }).ToList();
            
                        //var categoryDtos = user.Select(c => new GetUserDTO
                        //{
                        //    Id = c.Id,
                        //    Email = c.Email!,
                        //    Username = c.UserName!,
                        //    Tenant = c.TenantId!
                        //}).ToList();

            return tenantObj!;
        }

    }
}
