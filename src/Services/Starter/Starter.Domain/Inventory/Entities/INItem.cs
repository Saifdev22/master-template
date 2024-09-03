using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Domain.Inventory.Entities
{
    public class INItem
    {
        public Guid Id { get; private set; }
        public string ItemCode { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public bool IsActive { get; private set; }

        public INItem(Guid id, string itemCode, int quantity, decimal price)
        {
            if (string.IsNullOrEmpty(itemCode))
                throw new ValidationException("Name cannot be empty.");
            if (quantity < 0)
                throw new ValidationException("Quantity cannot be negative.");
            if (price < 0)
                throw new ValidationException("Price cannot be negative.");

            Id = id;
            ItemCode = itemCode;
            Quantity = quantity;
            Price = price;
        }

        public void AdjustQuantity(int amount)
        {
            //if (Quantity + amount < 0)
            //    throw new InventoryOperationException("Cannot reduce quantity below zero.");

            Quantity += amount;
        }
    }
}
