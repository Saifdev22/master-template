using BuildingBlocks.Application.CQRS;

namespace Starter.Application.Categories.Queries.GetCategoryById
{
    public record GetCategoryByIdQuery(Guid Id)
        : IQuery<GetCategoryByIdResult>;

    public record GetCategoryByIdResult(Category category);
}
