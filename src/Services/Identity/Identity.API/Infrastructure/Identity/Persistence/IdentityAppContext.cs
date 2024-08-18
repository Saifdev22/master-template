using Identity.API.Infrastructure.Identity.Persistence.Configurations;
using Identity.API.Infrastructure.Identity.Persistence.SeedData;
using Identity.API.Infrastructure.Identity.Roles;
using Identity.API.Infrastructure.Identity.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Infrastructure.Identity.Persistence
{
    public class IdentityAppContext(DbContextOptions options) : IdentityDbContext<IdentityAppUser, IdentityAppRole, string>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyCustomTableAndSchemaNames();
            IdentitySeedData.SeedData(builder);
        }
    }
}
