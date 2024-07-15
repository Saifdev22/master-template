namespace Starter.Application.Endpoints.Categories.Queries
{
    public record GetCategoryByIdQuery(Guid Id)
        : IQuery<GetCategoryByIdResult>;

    public record GetCategoryByIdResult(Category category);
}
