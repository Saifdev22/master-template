using Starter.Application;
using Starter.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.AddInfrastructureServices();
builder.AddApplicationServices();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseInfrastructureServices();
app.UseApplicationServices();

app.MapControllers();

app.Run();
