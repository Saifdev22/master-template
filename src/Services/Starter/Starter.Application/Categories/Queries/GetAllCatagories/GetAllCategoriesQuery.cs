using BuildingBlocksClient.Shared.Abstractions.Pagination;
using BuildingBlocksClient.Starter.DTOs;

namespace Starter.Application.Categories.Queries.GetAllCatagories
{
    public record GetAllCategoriesQuery(PaginationRequest PaginationRequest)
        : IQuery<GetCategoriesResult>;

    public record GetCategoriesResult(PaginatedResult<CategoryGetDto> Categories);
}
