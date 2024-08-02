using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Starter.Application.Categories.Queries.GetAllCatagories
{
    public class GetAllCategoriesHandler(IApplicationDbContext dbContext)
        : IQueryHandler<GetAllCategoriesQuery, GetCategoriesResult>
    {
        public async Task<GetCategoriesResult> Handle(GetAllCategoriesQuery query, CancellationToken cancellationToken)
        {
            // var client = factory.CreateClient("wordpress");

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
