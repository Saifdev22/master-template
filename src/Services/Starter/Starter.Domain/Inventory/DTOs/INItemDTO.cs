namespace Starter.Domain.Inventory.DTOs
{
    public record CreateINItemDTO
    (
        string ItemCode,
        string ItemDesc,
        decimal Price,
        string Notes,
        bool IsActive
    );

    public record UpdateINItemDTO
    (
        Guid ItemID,
        string ItemCode,
        string ItemDesc,
        decimal Price,
        string Notes,
        bool IsActive
    );
}
