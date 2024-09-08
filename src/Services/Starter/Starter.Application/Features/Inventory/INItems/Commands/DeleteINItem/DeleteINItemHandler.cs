namespace Starter.Application.Features.Inventory.INItems.Commands.DeleteINItem
{
    public class DeleteINItemHandler(IApplicationDbContext dbContext)
        : ICommandHandler<DeleteINItemCommand, DeleteINItemResult>
    {
        public async Task<DeleteINItemResult> Handle(DeleteINItemCommand command, CancellationToken cancellationToken)
        {
            var itemId = INItemID.Of(command.INItemId);

            var iNItem = await dbContext.INItems.FindAsync([itemId], cancellationToken: cancellationToken);

            if (iNItem is null)
            {
                throw new NotFoundException(command.INItemId.ToString());
            }

            dbContext.INItems.Remove(iNItem);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteINItemResult(true);
        }
    }
}
