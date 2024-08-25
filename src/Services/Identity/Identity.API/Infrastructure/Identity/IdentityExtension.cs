using BuildingBlocksClient.Identity.Interfaces;
using Identity.API.Infrastructure.Identity.Persistence;
using Identity.API.Infrastructure.Identity.Roles;
using Identity.API.Infrastructure.Identity.Roles.Endpoints;
using Identity.API.Infrastructure.Identity.Roles.Services;
using Identity.API.Infrastructure.Identity.Tokens.Endpoints;
using Identity.API.Infrastructure.Identity.Tokens.Services;
using Identity.API.Infrastructure.Identity.Users;
using Identity.API.Infrastructure.Identity.Users.Endpoints;
using Identity.API.Infrastructure.Identity.Users.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Identity.API.Infrastructure.Identity
{
    internal static class IdentityExtension
    {
        internal static IServiceCollection ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddScoped<IJWTService, JWTService>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();

            //Tokens
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                }).AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!))
                    };
                });

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

            return services;
        }

        public static IEndpointRouteBuilder MapIdentityEndpoints(this IEndpointRouteBuilder app)
        {
            var tokens = app.MapGroup("identity/tokens").WithTags("tokens");
            tokens.MapTokenEndpoints();

            var roles = app.MapGroup("identity/roles").WithTags("roles");
            roles.MapRoleEndpoints();

            var users = app.MapGroup("identity/users").WithTags("users");
            users.MapUserEndpoints();

            return app;
        }
    }
}
