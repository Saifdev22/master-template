using BuildingBlocksClient.Starter.DTOs;
using FluentValidation;

namespace Starter.Application.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand(CategoryDto category)
        : ICommand<CreateCategoryResult>;
    public record CreateCategoryResult(Guid Id);

    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.category.CategoryCode)
                .NotEmpty()
                .MaximumLength(5)
                .WithMessage("Code is required");

            RuleFor(x => x.category.CategoryDesc).NotNull().WithMessage("Description is required");
        }
    }
}
