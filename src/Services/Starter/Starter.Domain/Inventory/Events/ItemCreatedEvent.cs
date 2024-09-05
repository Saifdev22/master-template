using Starter.Domain.Inventory.Entities;

namespace Starter.Domain.Inventory.Events
{
    public record ItemCreatedEvent(INItem item) : IDomainEvent;
}
