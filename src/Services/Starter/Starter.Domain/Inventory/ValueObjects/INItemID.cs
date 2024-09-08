using Starter.Domain.Common.Exceptions;

namespace Starter.Domain.Inventory.ValueObjects
{
    public record INItemID
    {
        public Guid Value { get; }
        private INItemID(Guid value) => Value = value;
        public static INItemID Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("Item Id cannot be empty.");
            }

            return new INItemID(value);
        }
    }
}
