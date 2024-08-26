using Identity.API.Infrastructure.Identity.Roles;
using Identity.API.Infrastructure.Identity.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Infrastructure.Identity.Persistence.Configurations
{
    public static class ModelBuilderExtension
    {
        private const string CustomSchema = IdentityAppConstants.SchemaName;

        public static void ApplyCustomTableAndSchemaNames(this ModelBuilder builder)
        {
            builder.Entity<IdentityAppUser>(entity =>
            {
                entity.ToTable("Users", CustomSchema);
            });

            builder.Entity<IdentityAppRole>(entity =>
            {
                entity.ToTable("Roles", CustomSchema);
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims", CustomSchema);
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins", CustomSchema);
            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles", CustomSchema);
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims", CustomSchema);
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens", CustomSchema);
            });

            //builder.Entity<IdentityUser>().Ignore(c => c.AccessFailedCount)
            //                       .Ignore(c => c.LockoutEnabled)
            //                       .Ignore(c => c.NormalizedEmail)
            //                       .Ignore(c => c.Roles)
            //                       .Ignore(c => c.TwoFactorEnabled);

        }
    }
}
