namespace Starter.Domain.Inventory.DTOs
{
    public record INItemCommandDTO
    (
        string ItemCode,
        string ItemDesc,
        decimal Price,
        string Notes,
        bool IsActive
    );

}
