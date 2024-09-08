using BuildingBlocksClient.Shared.Abstractions.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Application.Features.Inventory.INItems.Queries.GetINItems
{
    public record GetINItemsQuery(PaginationRequest PaginationRequest)
        : IQuery<GetINItemsResult>;

    public record GetINItemsResult(PaginatedResult<INItem> INItems);
}
