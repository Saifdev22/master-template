namespace Starter.Application.Features.Inventory.INItems.Queries.GetINItemByID
{
    public class GetINItemByIDHandler(IApplicationDbContext dbContext)
        : IQueryHandler<GetINItemByIDQuery, GetINItemByIDResult>
    {
        public async Task<GetINItemByIDResult> Handle(GetINItemByIDQuery query, CancellationToken cancellationToken)
        {
            var initems = await dbContext.INItems.FindAsync(query.INItemID, cancellationToken);

            return new GetINItemByIDResult(initems!);

        }
    }
}
