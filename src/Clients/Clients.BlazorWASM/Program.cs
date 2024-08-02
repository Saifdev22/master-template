using Blazored.LocalStorage;
using BuildingBlocksClient.Interfaces;
using Clients.BlazorWASM;
using Clients.BlazorWASM.Helpers;
using Clients.BlazorWASM.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<CustomHttpDelegate>();
builder.Services.AddHttpClient("SystemApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7000/");
}).AddHttpMessageHandler<CustomHttpDelegate>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<CustomHttpClient>();
builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IUserService, UserServiceClient>();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
