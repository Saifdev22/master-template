using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Starter.Application.Features.Inventory.INItems.Queries.GetINItemByID;

namespace Starter.Application.Features.Inventory.INItems.Endpoints
{
    public static class GetINItemByIDEndpoint
    {
        internal static RouteHandlerBuilder MapGetINItemByIDEndpoint(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapGet("/getinitembyid", async ([FromBody] GetINItemByIDQuery request, ISender mediator) =>
            {
                var response = await mediator.Send(request);
                return Results.Ok(response);
            })
            .WithName(nameof(GetINItemByIDEndpoint))
            .WithSummary("creates a product")
            .WithDescription("creates a product")
            .Produces<GetINItemByIDResult>();
        }
    }
}
