using Identity.API.Infrastructure;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

//Controller Support.
builder.Services.AddControllers();

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

builder.AddInfrastructure();

var app = builder.Build();

//Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseInfrastructure();

//app.UseMiddleware<RestrictAccessMiddleware>();

app.UseHttpsRedirection();

app.UseCors();

app.MapControllers();

app.Run();
