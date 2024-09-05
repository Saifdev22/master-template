using Starter.Domain.Inventory.Entities;

namespace Starter.Domain.Inventory.Events
{
    public record OrderUpdatedEvent(INItem item) : IDomainEvent;
}
