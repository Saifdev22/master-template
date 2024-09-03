using Starter.Domain.Common.Abstractions;

namespace Starter.Domain.Models
{
    public class ProductDM : Entity<int>
    {
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double CostPrice { get; set; }
        public double SellingPrice { get; set; }
        public bool IsActive { get; set; }
    }
}
