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
            var INItem = app.MapGroup("inventory/initem").WithTags("INItem");
            INItem.MapINItemEndpoints();

            return app;
        }
    }
}
