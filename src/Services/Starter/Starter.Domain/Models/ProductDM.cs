namespace Starter.Domain.Models
{
    public class ProductDM : Entity<int>
    {
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
