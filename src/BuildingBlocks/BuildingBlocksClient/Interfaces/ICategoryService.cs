using BuildingBlocksClient.DTOs;
using BuildingBlocksClient.Pagination;

namespace BuildingBlocksClient.Interfaces
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
