using Microsoft.OpenApi.Models;
using Serilog;
using Starter.API;
using Starter.Application;
using Starter.Infrastructure;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//Serilog
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

//Minimal APIs
builder.Services.AddEndpointsApiExplorer();

//Exception Handler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//C# Libraries
builder.Services.AddApiServices(builder.Configuration);
builder.AddInfrastructureServices();
builder.Services.AddApplicationServices(builder.Configuration);

// Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

});

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

//Http Client
builder.Services.AddHttpClient("testapi", (serviceProvider, client) =>
{
    //var settings = serviceProvider
    //    .GetRequiredService<IOptions<GitHubSettings>>().Value;

    //client.DefaultRequestHeaders.Add("Authorization", "ss");
    //client.DefaultRequestHeaders.Add("User-Agent", "");

    client.BaseAddress = new Uri("https://fakestoreapi.com");
});

var app = builder.Build();

//Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Exception Handler
app.UseExceptionHandler(_ => { });

app.UseHttpsRedirection();

//Cors
app.UseCors("CorsPolicy");

//Serilog
app.UseSerilogRequestLogging();

app.UseAuthentication();
app.UseAuthorization();

app.UseInfrastructureServices();

//Only allow requests from the gateway.
//app.UseMiddleware<RestrictAccessMiddleware>();

app.MapControllers();

app.Run();
