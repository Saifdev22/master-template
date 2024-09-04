using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Domain.Inventory.Entities
{
    public sealed class INItem 
    {
        private INItem
        (
            int itemId,
            int categoryId,
            string itemCode,
            string itemDesc,
            decimal price,
            string notes,
            bool isActive
        )
            : base()
        {
           ItemId = itemId;
           CategoryId = categoryId;
           ItemCode = itemCode;
           ItemDesc = itemDesc;
           Price = price;
           Notes = notes;
           IsActive = isActive;
        }

        public int ItemId { get; private set; }
        public int CategoryId { get; private set; }
        public string ItemCode { get; private set; }
        public string ItemDesc { get; private set; }
        public decimal Price { get; private set; }
        public string Notes { get; private set; }
        public bool IsActive { get; private set; }

        //Static Factory Method
        public static INItem Create
        (
            int itemId,
            int categoryId,
            string itemCode,
            string itemDesc,
            decimal price,
            string notes,
            bool isActive
        )
        {
            var initem = new INItem
            (
                itemId,
                categoryId,
                itemCode,
                itemDesc,
                price,
                notes,
                isActive
            );

            return initem;
        }
    }
}
