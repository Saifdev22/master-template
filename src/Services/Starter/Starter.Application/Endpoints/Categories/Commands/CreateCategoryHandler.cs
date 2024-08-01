namespace Starter.Application.Endpoints.Categories.Commands
{
    public class CreateCategoryHandler(IApplicationDbContext dbContext)
        : ICommandHandler<CreateCategoryCommand, CreateCategoryResult>
    {
        public async Task<CreateCategoryResult> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = Category.Create
            (
                Guid.NewGuid(),
                command.category.CategoryCode,
                command.category.CategoryDesc,
                command.category.IsActive
            );

            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreateCategoryResult(category.Id);
        }


    }
}
