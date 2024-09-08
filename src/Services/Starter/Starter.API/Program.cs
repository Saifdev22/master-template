using Serilog;
using Starter.Application;
using Starter.Infrastructure;

Log.Information("Starter.API is booting up..");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//C# Libraries
builder.AddInfrastructureServices();
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseInfrastructureServices();
app.UseApplicationServices();

app.MapControllers();

app.Run();
