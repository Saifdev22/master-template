namespace Starter.Application.Features.Inventory.INItems.Commands.UpdateINItem
{
    public record UpdateINItemCommand
    (
        Guid ItemID,
        string ItemCode,
        string ItemDesc,
        decimal Price,
        string Notes,
        bool IsActive
    ) : ICommand<UpdateINItemResult>;

    public record UpdateINItemResult(bool IsSuccess);
}
