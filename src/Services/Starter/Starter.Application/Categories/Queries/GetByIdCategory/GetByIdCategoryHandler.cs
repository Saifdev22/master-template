using BuildingBlocks.Application.CQRS;

namespace Starter.Application.Categories.Queries.GetCategoryById
{
    public class GetByIdCategoryHandler(IApplicationDbContext dbContext)
        : IQueryHandler<GetCategoryByIdQuery, GetCategoryByIdResult>
    {
        public async Task<GetCategoryByIdResult> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var category = await dbContext.Categories
                .FindAsync(query.Id, cancellationToken);

            return new GetCategoryByIdResult(category!);
        }
    }
}