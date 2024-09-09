using Starter.Domain.Models;

namespace Starter.Domain.Inventory.Entities
{
    public class INCategory
    {
        public int CategoryId { get; set; }
        public string CategoryCode { get; private set; } = default!;
        public string CategoryDesc { get; private set; } = default!;
        public string Notes { get; private set; } = default!;
        public bool IsActive { get; private set; }
    }
}
