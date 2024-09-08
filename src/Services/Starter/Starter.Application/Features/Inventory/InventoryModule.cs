using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Starter.Application.Features.Inventory.INItems.Endpoints;

namespace Starter.Application.Features.Inventory
{
    internal static class InventoryModule
    {
        public static IEndpointRouteBuilder MapInventoryEndpoints(this IEndpointRouteBuilder app)
        {
            var inItems = app.MapGroup("inventory/initem").WithTags("initems");
            inItems.MapINItemEndpoints();

            return app;
        }
    }
}
