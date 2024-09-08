namespace Starter.Application.Features.Inventory.INItems.Commands.UpdateINItem
{
    public class UpdateINItemHandler(IApplicationDbContext _dbContext)
    : ICommandHandler<UpdateINItemCommand, UpdateINItemResult>
    {
        public async Task<UpdateINItemResult> Handle(UpdateINItemCommand command, CancellationToken cancellationToken)
        {
            var iNItem = await _dbContext.INItems.FindAsync([command.ItemID], cancellationToken: cancellationToken);

            if (iNItem == null)
            {
                // Handle the case where the item is not found (could throw an exception or return an error result)
                throw new KeyNotFoundException($"INItem with ID {command.ItemID} not found.");
            }

            var abc = UpdateINItem(command);

            _dbContext.INItems.Update(abc);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateINItemResult(true);
        }

        private INItem UpdateINItem(UpdateINItemCommand createINItemDTO)
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
