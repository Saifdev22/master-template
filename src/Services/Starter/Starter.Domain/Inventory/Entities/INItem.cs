﻿using Starter.Domain.Inventory.Events;
using Starter.Domain.Inventory.ValueObjects;

namespace Starter.Domain.Inventory.Entities
{
    public class INItem : Aggregate<INItemID>
    {
        public string ItemCode { get; private set; } = default!;
        public string ItemDesc { get; private set; } = default!;
        public decimal Price { get; private set; }
        public string Notes { get; private set; } = default!;
        public bool IsActive { get; private set; }

        //Static Factory Method
        public static INItem Create
        (
            INItemID id,
            string itemCode,
            string itemDesc,
            decimal price,
            string notes,
            bool isActive
        )
        {
            var initem = new INItem
            {
                Id = id,
                ItemCode = itemCode,
                ItemDesc = itemDesc,
                Price = price,
                Notes = notes,
                IsActive = isActive
            };

            initem.AddDomainEvent(new ItemCreatedEvent(initem));

            return initem;
        }
    }
}
