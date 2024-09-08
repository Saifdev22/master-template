using FluentValidation;

namespace Starter.Application.Features.Inventory.INItems.Commands.CreateINItem
{
    public class CreateINItemCommandValidator : AbstractValidator<CreateINItemCommand>
    {
        public CreateINItemCommandValidator()
        {
            RuleFor(p => p.INItemCommandDTO.ItemCode)
                .NotEmpty()
                .MaximumLength(20)
                .WithMessage("ItemCode validation occured."); 

            RuleFor(p => p.INItemCommandDTO.ItemDesc)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("ItemDesc validation occured."); 

            RuleFor(p => p.INItemCommandDTO.Price)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Price validation occured.");
        }
    }

}
