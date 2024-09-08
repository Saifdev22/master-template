namespace Starter.Application.Features.Inventory.INItems.Commands.CreateINItem
{
    public class CreateINItemHandler(IApplicationDbContext _dbContext)
        : ICommandHandler<CreateINItemCommand, CreateINItemResult>
    {
        public async Task<CreateINItemResult> Handle(CreateINItemCommand command, CancellationToken cancellationToken)
        {
            var newINItem = CreateNewINItem(command.INItemCommandDTO);

            _dbContext.INItems.Add(newINItem);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateINItemResult(newINItem.Id.Value);
        }

        private INItem CreateNewINItem(INItemCommandDTO orderDto)
        {
            var newINItem = INItem.Create
            (
                id: INItemID.Of(Guid.NewGuid()),
                itemCode: orderDto.ItemCode,
                itemDesc: orderDto.ItemDesc,
                price: orderDto.Price,
                notes: orderDto.Notes,
                isActive: orderDto.IsActive
            );

            return newINItem;
        }
    }
}
