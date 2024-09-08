using Microsoft.EntityFrameworkCore;

namespace Starter.Application.Features.Inventory.INItems.Queries.GetINItemByCode
{
    public class GetINItemByCodeHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetINItemByCodeQuery, GetINItemByCodeResult>
    {
        public async Task<GetINItemByCodeResult> Handle(GetINItemByCodeQuery query, CancellationToken cancellationToken)
        {
            var initems = await dbContext.INItems.FirstOrDefaultAsync(c => c.ItemCode == query.INItemCode);

            return new GetINItemByCodeResult(initems!);

        }
    }
}
