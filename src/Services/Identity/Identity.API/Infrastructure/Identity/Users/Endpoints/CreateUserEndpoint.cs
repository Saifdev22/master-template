using Azure.Core;
using Identity.API.Infrastructure.Identity.Roles.Endpoints;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Infrastructure.Identity.Users.Endpoints
{
    public static class CreateUserEndpoint
    {
        public static RouteHandlerBuilder MapCreateUserEndpoint(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapPost("/", async ([FromForm] CreateUserDTO userDTO, IFormFileCollection file, HttpRequest request, IUserService _userService) =>
            {     
                return await _userService.CreateUser(userDTO, file);
            })
            .DisableAntiforgery()
            .WithName(nameof(CreateUserEndpoint))
            .WithSummary("Creates a user.")
            .WithDescription("Create a new user in the system.");
        }
    }
}
