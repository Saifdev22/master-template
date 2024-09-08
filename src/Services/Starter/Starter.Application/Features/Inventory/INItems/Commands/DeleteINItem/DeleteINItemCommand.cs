namespace Starter.Application.Features.Inventory.INItems.Commands.DeleteINItem
{
    public record DeleteINItemCommand(Guid INItemId)
        : ICommand<DeleteINItemResult>;

    public record DeleteINItemResult(bool IsSuccess);
}
