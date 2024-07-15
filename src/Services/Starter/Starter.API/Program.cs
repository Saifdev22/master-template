using Serilog;
using Starter.API;
using Starter.Application;
using Starter.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//Serilog
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Exception Handler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//Libraries
builder.Services
    .AddApiServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration);

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

//Serilog
app.UseSerilogRequestLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();
