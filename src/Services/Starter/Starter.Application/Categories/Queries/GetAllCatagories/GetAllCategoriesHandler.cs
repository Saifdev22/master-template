using BuildingBlocksClient.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Starter.Application.Categories.Queries.GetAllCatagories
{
    public class GetAllCategoriesHandler(IApplicationDbContext dbContext)
        : IQueryHandler<GetAllCategoriesQuery, GetCategoriesResult>
    {
        public async Task<GetCategoriesResult> Handle(GetAllCategoriesQuery query, CancellationToken cancellationToken)
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


            var categoryDtos = categories.Select(c => new CategoryGetDto
            {
                Id = c.Id,
                CategoryCode = c.CategoryCode,
                CategoryDesc = c.CategoryDesc,
                IsActive = c.IsActive,
                CreatedAt = c.CreatedAt,
                CreatedBy = c.CreatedBy,
                LastModified = c.LastModified,
                LastModifiedBy = c.LastModifiedBy

            }).ToList();


            return new GetCategoriesResult(
                new PaginatedResult<CategoryGetDto>(
                    pageIndex,
                    pageSize,
                    totalCount,
                    categoryDtos.ToList()
                ));
        }
    }

}
