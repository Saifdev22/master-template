using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Starter.Application.Features.Inventory.INItems.Queries.GetINItemByCode;

namespace Starter.Application.Features.Inventory.INItems.Endpoints
{
    public static class GetINItemByCodeEndpoint
    {
        internal static RouteHandlerBuilder MapGetINItemByCodeEndpoint(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapGet("/getinitembycode", async ([FromBody] GetINItemByCodeQuery request, ISender mediator) =>
            {
                var response = await mediator.Send(request);
                return Results.Ok(response);
            })
            .WithName(nameof(GetINItemByCodeEndpoint))
            .WithSummary("creates a product")
            .WithDescription("creates a product")
            .Produces<GetINItemByCodeResult>();
        }
    }
}
