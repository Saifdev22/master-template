using Identity.API.Infrastructure.Identity.Roles;
using Identity.API.Infrastructure.Identity.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Infrastructure.Identity.Persistence.SeedData
{
    public static class IdentitySeedData
    {
        public static void SeedData(ModelBuilder builder)
        {
            //Seed Roles
            var masterRoleId = Guid.NewGuid().ToString();
            var adminRoleId = Guid.NewGuid().ToString();
            var userRoleId = Guid.NewGuid().ToString();


            var roles = new IdentityAppRole[]
            {
                new IdentityAppRole("Master", "Master role with complete software control.")
                {
                    Id = masterRoleId
                },

                new IdentityAppRole("Admin", "Administrator role with limited permissions.")
                {
                    Id = adminRoleId
                },

                new IdentityAppRole("User", "User role with limited permissions.")
                {
                    Id = userRoleId
                }
            };

            builder.Entity<IdentityAppRole>().HasData(roles);

            //Seed Users
            var hasher = new PasswordHasher<IdentityAppUser>();

            var users = new IdentityAppUser[]
            {
                new IdentityAppUser("Master", "master@gmail.com")
                {
                    Id = Guid.NewGuid().ToString(),
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null!, "Master@2024"),

                    TenantId = "tenant_1",
                    Gender = "Male",
                    Notes = "System Master",
                    ImageUrl = "Image1",
                    ProfileImage = BitConverter.GetBytes(100),
                    IsActive = true
                },
                new IdentityAppUser("Admin", "admin@gmail.com")
                {
                    Id = Guid.NewGuid().ToString(),
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null!, "Admin@2024"),

                    TenantId = "tenant_1",
                    Gender = "Male",
                    Notes = "System Administrator",
                    ImageUrl = "Image2",
                    ProfileImage = BitConverter.GetBytes(200),
                    IsActive = true
                },
                new IdentityAppUser("User", "user@example.com")
                {
                    Id = Guid.NewGuid().ToString(),
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null!, "User@2024"),

                    TenantId = "tenant_1",
                    Gender = "Female",
                    Notes = "Regular User",
                    ImageUrl = "Image3",
                    ProfileImage = BitConverter.GetBytes(300),
                    IsActive = true
                }
            };

            builder.Entity<IdentityAppUser>().HasData(users);

            // Seed UserRoles
            var userRoles = new IdentityUserRole<string>[]
            {
                new IdentityUserRole<string>
                {
                    UserId = users[0].Id, // Master
                    RoleId = masterRoleId
                },
                new IdentityUserRole<string>
                {
                    UserId = users[1].Id, // Admin
                    RoleId = adminRoleId
                },
                new IdentityUserRole<string>
                {
                    UserId = users[2].Id, // User
                    RoleId = userRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(userRoles);

        }
    }
}
