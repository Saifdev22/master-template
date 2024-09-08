using BuildingBlocks.Domain.Exceptions.Handler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Starter.Infrastructure.Identity;
using Starter.Infrastructure.Identity.Users;
using Starter.Infrastructure.Multitenancy.Extensions;
using Starter.Infrastructure.Persistence;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using static Starter.Infrastructure.Identity.IdentityConstants;

namespace Starter.Infrastructure
{
    public static class DependencyInjection
    {
        public static WebApplicationBuilder AddInfrastructureServices(this WebApplicationBuilder builder)
        {
            //Minimal APIs
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = JwtConstants.Audience,
                    ValidIssuer = JwtConstants.Issuer,
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConstants.Key)),
                    ValidateIssuerSigningKey = true,
                };
            });

            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));

            builder.Services.AddExceptionHandler<CustomExceptionHandler>();

            builder.Services.ConfigureIdentity(builder.Configuration);
            builder.Services.ConfigureDatabase(builder.Configuration);

            // Cors
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

            });

            return builder;
        }

        public static WebApplication UseInfrastructureServices(this WebApplication app)
        {
            //Only allow requests from the gateway.
            //app.UseMiddleware<RestrictAccessMiddleware>();

            //Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandler(_ => { });
            app.UseSerilogRequestLogging();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<UserMiddleware>();
            app.UseMiddleware<TenantMiddleware>();

            app.UseCors("CorsPolicy");

            return app;
        }
    }
}