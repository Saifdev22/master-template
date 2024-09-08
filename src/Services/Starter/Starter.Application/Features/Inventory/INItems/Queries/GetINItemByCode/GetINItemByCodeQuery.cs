namespace Starter.Application.Features.Inventory.INItems.Queries.GetINItemByCode
{
    public record GetINItemByCodeQuery(string INItemCode)
        : IQuery<GetINItemByCodeResult>;

    public record GetINItemByCodeResult(INItem INItem);
}
