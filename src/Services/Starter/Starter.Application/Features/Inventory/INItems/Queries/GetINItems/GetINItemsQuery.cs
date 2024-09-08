using BuildingBlocksClient.Shared.Abstractions.Pagination;

namespace Starter.Application.Features.Inventory.INItems.Queries.GetINItems
{
    public record GetINItemsQuery(PaginationRequest PaginationRequest)
        : IQuery<GetINItemsResult>;

    public record GetINItemsResult(PaginatedResult<INItem> INItems);
}
