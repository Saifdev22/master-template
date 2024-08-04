namespace BuildingBlocksClient.DTOs

{
    public record CategoryDto(string CategoryCode, string CategoryDesc, bool IsActive);

    public class CategoryGetDto
    {
        public Guid Id { get; set; }
        public string CategoryCode { get; set; } = default!;
        public string CategoryDesc { get; set; } = default!;
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}

