using Starter.Domain.Common.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Domain.Common.Contracts
{
    public interface IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
        IDomainEvent[] ClearDomainEvents();
    }

    public interface IEntity<out TId> : IEntity
    {
        TId Id { get; }
    }
}
