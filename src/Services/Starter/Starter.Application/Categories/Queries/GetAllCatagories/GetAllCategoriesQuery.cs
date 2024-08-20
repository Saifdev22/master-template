using BuildingBlocks.Application.CQRS;
using BuildingBlocksClient.Application.Starter.DTOs;
using BuildingBlocksClient.Domain.Pagination;

namespace Starter.Application.Categories.Queries.GetAllCatagories
{
    public record GetAllCategoriesQuery(PaginationRequest PaginationRequest)
        : IQuery<GetCategoriesResult>;

    public record GetCategoriesResult(PaginatedResult<CategoryGetDto> Categories);
}
