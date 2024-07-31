using FluentValidation;

namespace Starter.Application.ProductContract.Commands
{
    public record CreateProductCommand(string Name, int Quantity)
          : ICommand<CreateProductResult>;
    public record CreateProductResult(int Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(5).WithMessage("Name is required");

            RuleFor(x => x.Quantity).NotNull().WithMessage("Qty is required");
        }
    }
}
