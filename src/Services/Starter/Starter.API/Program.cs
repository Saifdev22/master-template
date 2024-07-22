using BuildingBlocks.Gateway;
using Serilog;
using Starter.API;
using Starter.Application;
using Starter.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//Serilog
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

//For Minimal APIs
builder.Services.AddEndpointsApiExplorer();

//Swagger
builder.Services.AddSwaggerGen();

//Exception Handler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//Libraries
builder.Services
    .AddApiServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration);

// Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
        //.WithOrigins("http://client.saifkhan.co.za", "https://localhost:7100")
        //.AllowCredentials()
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
       
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

//Exception Handler
app.UseExceptionHandler(_ => { });

//Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

//Serilog
app.UseSerilogRequestLogging();

app.UseAuthorization();

//Only allow requests from the gateway.
app.UseMiddleware<RestrictAccessMiddleware>();

app.MapControllers();

app.Run();
