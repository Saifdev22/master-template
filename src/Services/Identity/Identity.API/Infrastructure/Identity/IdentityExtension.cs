using Identity.API.Infrastructure.Identity.Persistence;
using Identity.API.Infrastructure.Identity.Roles;
using Identity.API.Infrastructure.Identity.Roles.Endpoints;
using Identity.API.Infrastructure.Identity.Roles.Services;
using Identity.API.Infrastructure.Identity.Users;
using Identity.API.Infrastructure.Identity.Users.Endpoints;
using Identity.API.Infrastructure.Identity.Users.Services;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Infrastructure.Identity
{
    internal static class IdentityExtension
    {
        internal static IServiceCollection ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            //Database Connection
            services.AddDbContext<IdentityAppContext>(options =>
                     options.UseSqlServer(configuration.GetConnectionString("DefaultConnection") ??
                     throw new InvalidOperationException("Issue With Connection String!")));

            services.AddIdentity<IdentityAppUser, IdentityAppRole>(options =>
            {
                options.Password.RequiredLength = IdentityAppConstants.PasswordLength;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<IdentityAppContext>()
                //.AddSignInManager()                     
                .AddRoles<IdentityAppRole>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IAccountService, AccountService>();

            return services;
        }

        public static IEndpointRouteBuilder MapIdentityEndpoints(this IEndpointRouteBuilder app)
        {
            var roles = app.MapGroup("identity/roles").WithTags("roles");
            roles.MapRoleEndpoints();

            var users = app.MapGroup("identity/users").WithTags("users");
            users.MapUserEndpoints();

            return app;
        }
    }
}
