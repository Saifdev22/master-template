namespace Starter.Domain.Models
{
    public class Category : Entity<Guid>, IMustHaveTenant
    {
        public string CategoryCode { get; private set; } = default!;
        public string CategoryDesc { get; private set; } = default!;
        public bool IsActive { get; private set; } = default!;
        public string TenantId { get; set; } = default!;

        public static Category Create(Guid id, string categoryCode, string categoryDesc, bool isActive)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(categoryCode);
            ArgumentException.ThrowIfNullOrWhiteSpace(categoryDesc);

            var category = new Category
            {
                Id = id,
                CategoryCode = categoryCode,
                CategoryDesc = categoryDesc,
                IsActive = isActive
            };

            return category;
        }


    }
}
