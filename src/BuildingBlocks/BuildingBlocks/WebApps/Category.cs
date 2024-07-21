namespace BuildingBlocks.WebApps;

public class Category 
{
    public string CategoryCode { get; private set; } = default!;
    public string CategoryDesc { get; private set; } = default!;

    public static Category Create(Guid id, string categoryCode, string categoryDesc)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(categoryCode);
        ArgumentException.ThrowIfNullOrWhiteSpace(categoryDesc);

    var category = new Category
    {
        CategoryCode = categoryCode,
        CategoryDesc = categoryDesc
    };

    return category;
    }
}

