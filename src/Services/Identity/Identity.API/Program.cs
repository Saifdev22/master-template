using Identity.API.Data;
using Identity.API.Extensions;
using Identity.API.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

//Controller Support.
builder.Services.AddControllers();

//Database Connection
builder.Services.AddDbContext<IdentityAppContext>(options =>
         options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ??
         throw new InvalidOperationException("Issue With Connection String!")));

//Identity Manager
builder.Services.AddIdentity<IdentityAppUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityAppContext>()
    .AddSignInManager()
    .AddRoles<IdentityRole>();

// JWT
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddScoped<ITokenService, TokenService>();

//Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

//Configures all requests globally
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = long.MaxValue;
});

//File Upload
builder.Services.Configure<FormOptions>(x =>
{
    x.ValueLengthLimit = int.MaxValue;
    x.MultipartBodyLengthLimit = int.MaxValue;
    x.MultipartBoundaryLengthLimit = int.MaxValue;
    x.MultipartHeadersCountLimit = int.MaxValue;
    x.MultipartHeadersLengthLimit = int.MaxValue;
});

//Swagger
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

//Middleware & Services
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddTransient<CustomAuthorizationMiddleware>();

var app = builder.Build();

//Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<RestrictAccessMiddleware>();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

//app.UseMiddleware<CustomAuthenticationMiddleware>();
//app.UseMiddleware<CustomAuthorizationMiddleware>();
app.Use(async (context, next) =>
{
    Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
    await next();

});

app.MapControllers();

app.Run();
