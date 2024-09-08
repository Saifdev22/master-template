using BuildingBlocksClient.Shared.Abstractions.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Starter.Application.Features.Inventory.INItems.Queries.GetINItems
{
    public class GetINItemsHandler(IApplicationDbContext dbContext)
        : IQueryHandler<GetINItemsQuery, GetINItemsResult>
    {
        public async Task<GetINItemsResult> Handle(GetINItemsQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;

            var totalCount = await dbContext.INItems.LongCountAsync(cancellationToken);

            var orders = await dbContext.INItems
                           .Include(o => o.Id)
                           .OrderBy(o => o.ItemCode)
                           .Skip(pageSize * pageIndex)
                           .Take(pageSize)
                           .AsNoTracking()
                           .ToListAsync(cancellationToken);

            return new GetINItemsResult(
                new PaginatedResult<INItem>(
                    pageIndex,
                    pageSize,
                    totalCount,
                    orders.ToList()));
        }
    }
}
