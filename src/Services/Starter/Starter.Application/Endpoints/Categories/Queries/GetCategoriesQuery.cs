using BuildingBlocks.Pagination;

namespace Starter.Application.Endpoints.Categories.Queries
{
    public record GetCategoriesQuery(PaginationRequest PaginationRequest)
        : IQuery<GetCategoriesResult>;

    public record GetCategoriesResult(PaginatedResult<Category> Categories);
}
