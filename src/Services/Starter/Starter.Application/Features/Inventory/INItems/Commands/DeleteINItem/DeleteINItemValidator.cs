using FluentValidation;

namespace Starter.Application.Features.Inventory.INItems.Commands.DeleteINItem
{
    public class DeleteINItemCommandValidator : AbstractValidator<DeleteINItemCommand>
    {
        public DeleteINItemCommandValidator()
        {
            RuleFor(x => x.INItemId)
                .NotEmpty()
                .WithMessage("INItemId validation occured.");
        }
    }
}
