namespace Starter.Application.Features.Inventory.INItems.Queries.GetINItemByID
{
    public record GetINItemByIDQuery(Guid INItemID)
        : IQuery<GetINItemByIDResult>;

    public record GetINItemByIDResult(INItem INItem);
}
