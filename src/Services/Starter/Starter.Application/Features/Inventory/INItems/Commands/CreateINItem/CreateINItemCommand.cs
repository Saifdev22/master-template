namespace Starter.Application.Features.Inventory.INItems.Commands.CreateINItem
{
    public record CreateINItemCommand(CreateINItemDTO createINItemDTO) : ICommand<CreateINItemResult>;
    public record CreateINItemResult(Guid ID);
}
