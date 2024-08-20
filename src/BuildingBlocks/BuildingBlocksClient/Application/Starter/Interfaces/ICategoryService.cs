using BuildingBlocksClient.Application.Starter.DTOs;
using BuildingBlocksClient.Domain.Pagination;

namespace BuildingBlocksClient.Application.Starter.Interfaces
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
