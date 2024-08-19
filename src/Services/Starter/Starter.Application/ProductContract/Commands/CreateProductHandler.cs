using BuildingBlocks.Application.CQRS;

namespace Starter.Application.ProductContract.Commands
{
    public class CreateProductHandler(IApplicationDbContext dbContext)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new ProductDM
            {
                Id = 0,
                Name = command.Name,
                Quantity = command.Quantity,
            };

            dbContext.ProductDMs.Add(product);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }


    }
}
