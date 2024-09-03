using Microsoft.EntityFrameworkCore;

using System.Reflection;

namespace Starter.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public string? CurrentTenantId { get; set; }
        public string? TenantConnectionString { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            ICurrentTenantService _tenantRepository) : base(options)
        {
            CurrentTenantId = _tenantRepository.TenantId;
            TenantConnectionString = _tenantRepository.ConnectionString;
        }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<ProductDM> ProductDMs => Set<ProductDM>();

        //Fires once on app startup.
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //Select all records with matching tenant Id. 
            builder.Entity<Category>().HasQueryFilter(a => a.TenantId == CurrentTenantId);
            base.OnModelCreating(builder);
        }

        // Dynamic connection string, fires on every request.
        // Use tenant database connection string whenever there’s one specified by the current tenant.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string tenantConnectionString = TenantConnectionString!;
            if (!string.IsNullOrEmpty(tenantConnectionString))
            {
                _ = optionsBuilder.UseSqlServer(tenantConnectionString);
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.Entity.TenantId = CurrentTenantId!;
                        break;
                }
            }
            var result = await base.SaveChangesAsync(cancellationToken);
            return result;
        }

        //Save tenant Id to any entity that implemented the IMustHaveTenant interface.
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.Entity.TenantId = CurrentTenantId!;
                        break;
                }
            }
            var result = base.SaveChanges();
            return result;
        }

    }
}
