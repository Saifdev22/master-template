using BuildingBlocksClient.Pagination;
using BuildingBlocksClient.Starter.DTOs;

namespace BuildingBlocksClient.Starter.Interfaces
{
    public interface ICategoryService
    {
        Task<Guid> AddCategoryAsync(CategoryDto model);
        Task<CategoryDto> UpdateCategoryAsync(CategoryDto model);
        Task<CategoryDto> DeleteCategoryAsync(Guid categoryId);
        Task<PaginatedResult<CategoryGetDto>> GetAllCategoriesAsync(PaginationRequest model);
        Task<CategoryDto> GetCategoryByIdAsync(Guid categoryId);
    }
}
