using BuildingBlocksClient.Identity.Interfaces;
using static BuildingBlocksClient.Identity.DTOs.TokenDTO;

namespace Identity.API.Infrastructure.Identity.Tokens.Endpoints
{
    public static class RefreshTokenEndpoint
    {
        internal static RouteHandlerBuilder MapRefreshTokenEndpoint(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapPost("/refresh", (TokenRequest request,
                IJWTService service,
                HttpContext context,
                CancellationToken cancellationToken) =>
            {
                string ip = context.GetIpAddress();
                return service.GetTokenWithRefreshToken(request, ip!, cancellationToken);
            })
            .WithName(nameof(RefreshTokenEndpoint))
            .WithSummary("refresh JWTs")
            .WithDescription("refresh JWTs")
            .AllowAnonymous();
        }
    }
}
