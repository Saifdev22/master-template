using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Starter.Application.Features.Inventory.INItems.Commands.CreateINItem;

namespace Starter.Application.Features.Inventory.INItems.Endpoints
{
    public static class CreateINItemEndpoint
    {
        internal static RouteHandlerBuilder MapCreateINItemEndpoint(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapPost("/", async (CreateINItemCommand request, ISender mediator) =>
            {
                var response = await mediator.Send(request);
                return Results.Ok(response);
            })
            .WithName(nameof(CreateINItemEndpoint))
            .WithSummary("creates a product")
            .WithDescription("creates a product")
            .Produces<CreateINItemResult>();
        }
    }
}
