using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Starter.Application.Features.Inventory.INItems.Commands.DeleteINItem;

namespace Starter.Application.Features.Inventory.INItems.Endpoints
{
    public static class DeleteINItemEndpoint
    {
        internal static RouteHandlerBuilder MapDeleteINItemEndpoint(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapDelete("/{id:guid}", async (Guid id, ISender mediator) =>
            {
                var response = await mediator.Send(new DeleteINItemCommand(id));
                return Results.Ok(response);
            })
            .WithName(nameof(DeleteINItemEndpoint))
            .WithSummary("creates a product")
            .WithDescription("creates a product")
            .Produces<DeleteINItemResult>();
        }
    }
}
