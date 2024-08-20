using System.ComponentModel.DataAnnotations;

namespace BuildingBlocksClient.Application.Starter.DTOs
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Quantity { get; set; }
    }
}
