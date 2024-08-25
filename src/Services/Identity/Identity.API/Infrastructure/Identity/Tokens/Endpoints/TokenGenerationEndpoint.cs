using BuildingBlocksClient.Identity.DTOs;
using BuildingBlocksClient.Identity.Interfaces;
using static BuildingBlocksClient.Identity.DTOs.TokenDTO;

namespace Identity.API.Infrastructure.Identity.Tokens.Endpoints
{
    public static class TokenGenerationEndpoint
    {
        internal static RouteHandlerBuilder MapTokenGenerationEndpoint(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapPost("/", (LoginDTO request,
                IJWTService service,
                HttpContext context,
                CancellationToken cancellationToken) =>
            {
                string ip = context.GetIpAddress();
                return service.LoginUser(request, ip, cancellationToken);
            })
            .WithName(nameof(TokenGenerationEndpoint))
            .WithSummary("Generate JWT.")
            .WithDescription("Login User.")
            .Produces<TokenResponse>(200)
            .AllowAnonymous();
        }
    }
}
