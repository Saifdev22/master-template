namespace Starter.Application.Features.Inventory.INItems.Commands.CreateINItem
{
    public record CreateINItemCommand(INItemCommandDTO INItemCommandDTO) : ICommand<CreateINItemResult>;
    public record CreateINItemResult(Guid Id);
}
