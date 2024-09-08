using FluentValidation;

namespace Starter.Application.Features.Inventory.INItems.Commands.CreateINItem
{
    public class CreateINItemValidator : AbstractValidator<CreateINItemCommand>
    {
        public CreateINItemValidator()
        {
            RuleFor(p => p.createINItemDTO.ItemCode)
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("ItemCode validation occured.");

            RuleFor(p => p.createINItemDTO.ItemDesc)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("ItemDesc validation occured.");

            RuleFor(p => p.createINItemDTO.Price)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Price validation occured.");
        }
    }

}
