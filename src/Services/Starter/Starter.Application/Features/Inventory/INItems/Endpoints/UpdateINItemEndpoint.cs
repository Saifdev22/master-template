using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Starter.Application.Features.Inventory.INItems.Commands.UpdateINItem;

namespace Starter.Application.Features.Inventory.INItems.Endpoints
{
    public static class UpdateINItemEndpoint
    {
        internal static RouteHandlerBuilder MapUpdateINItemEndpoint(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapPut("/", async ([FromBody] UpdateINItemCommand request, ISender mediator) =>
            {
                var response = await mediator.Send(request);
                return Results.Ok(response);
            })
            .WithName(nameof(UpdateINItemEndpoint))
            .WithSummary("Update INItem.")
            .WithDescription("Update Description.")
            .Produces<UpdateINItemResult>();
        }
    }
}
