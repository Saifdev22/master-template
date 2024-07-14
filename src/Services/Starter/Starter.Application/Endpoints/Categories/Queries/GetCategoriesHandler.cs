using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Starter.Application.Endpoints.Categories.Queries
{
    public class GetCategoriesHandler(IApplicationDbContext dbContext)
        : IQueryHandler<GetCategoriesQuery, GetCategoriesResult>
    {
        public async Task<GetCategoriesResult> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;
            var totalCount = await dbContext.Categories.LongCountAsync(cancellationToken);

            var categories = await dbContext.Categories
                           .AsNoTracking()
                           .OrderBy(o => o.CategoryCode)
                           .Skip(pageSize * pageIndex)
                           .Take(pageSize)
                           .ToListAsync(cancellationToken);

            return new GetCategoriesResult(
                new PaginatedResult<Category>(
                    pageIndex,
                    pageSize,
                    totalCount,
                    categories.ToList()
                ));
        }
    }

}
