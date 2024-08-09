using Serilog;
using Starter.Application;
using Starter.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//Serilog
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

//Minimal APIs
builder.Services.AddEndpointsApiExplorer();

//Exception Handler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddApplicationServices(builder.Configuration);
builder.AddInfrastructureServices();
builder.Services.AddApplicationServices(builder.Configuration);
//C# Libraries
//builder.
//    //.AddApiServices(builder.Configuration)
//    .AddInfrastructureServices(builder.Configuration)
//    .AddApplicationServices(builder.Configuration);
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
