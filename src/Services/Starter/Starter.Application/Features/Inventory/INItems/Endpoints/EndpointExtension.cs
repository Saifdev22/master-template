using Microsoft.AspNetCore.Routing;

namespace Starter.Application.Features.Inventory.INItems.Endpoints
{
    internal static class EndpointExtension
    {
        public static IEndpointRouteBuilder MapINItemEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapCreateINItemEndpoint();

            return app;
        }
    }
}
