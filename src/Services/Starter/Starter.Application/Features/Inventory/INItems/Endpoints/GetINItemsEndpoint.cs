using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Starter.Application.Features.Inventory.INItems.Queries.GetINItems;

namespace Starter.Application.Features.Inventory.INItems.Endpoints
{
    public static class GetINItemsEndpoint
    {
        internal static RouteHandlerBuilder MapGetINItemsEndpoint(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapGet("/getinitems", async ([FromBody] GetINItemsQuery request, ISender mediator) =>
            {
                var response = await mediator.Send(request);
                return Results.Ok(response);
            })
            .WithName(nameof(GetINItemsEndpoint))
            .WithSummary("creates a product")
            .WithDescription("creates a product")
            .Produces<GetINItemsResult>();
        }
    }
}
