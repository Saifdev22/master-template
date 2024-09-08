namespace Starter.Application.Features.Inventory.INItems.Commands.CreateINItem
{
    public class CreateINItemHandler(IApplicationDbContext _dbContext)
        : ICommandHandler<CreateINItemCommand, CreateINItemResult>
    {
        public async Task<CreateINItemResult> Handle(CreateINItemCommand command, CancellationToken cancellationToken)
        {
            var newINItem = CreateNewINItem(command.createINItemDTO);

            _dbContext.INItems.Add(newINItem);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateINItemResult(newINItem.Id.Value);
        }

        private INItem CreateNewINItem(CreateINItemDTO createINItemDTO)
        {
            var newINItem = INItem.Create
            (
                id: INItemID.Of(Guid.NewGuid()),
                itemCode: createINItemDTO.ItemCode,
                itemDesc: createINItemDTO.ItemDesc,
                price: createINItemDTO.Price,
                notes: createINItemDTO.Notes,
                isActive: createINItemDTO.IsActive
            );

            return newINItem;
        }
    }
}
