using FluentValidation;

namespace Starter.Application.Features.Inventory.INItems.Commands.UpdateINItem
{
    public class UpdateINItemValidator : AbstractValidator<UpdateINItemCommand>
    {
        public UpdateINItemValidator()
        {
            RuleFor(p => p.ItemID)
                .NotEmpty()
                .WithMessage("ItemId validation occured.");

            RuleFor(p => p.ItemCode)
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("ItemCode validation occured.");

            RuleFor(p => p.ItemDesc)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("ItemDesc validation occured.");

            RuleFor(p => p.Price)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Price validation occured.");
        }
    }
}
