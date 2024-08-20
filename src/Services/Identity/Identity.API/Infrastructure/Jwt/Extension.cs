using BuildingBlocksClient.Application.Identity.Interfaces;
using Identity.API.Infrastructure.Jwt.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Identity.API.Infrastructure.Jwt
{
    public static class Extension
    {
        public static IServiceCollection ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();

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

            return services;
        }

        public static string GetIpAddress(this HttpContext context)
        {
            string ip = "N/A";
            if (context.Request.Headers.TryGetValue("X-Forwarded-For", out var ipList))
            {
                ip = ipList.FirstOrDefault() ?? "N/A";
            }
            else if (context.Connection.RemoteIpAddress != null)
            {
                ip = context.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }

            return ip;

        }

    }
}
