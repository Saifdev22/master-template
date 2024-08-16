using BuildingBlocksClient.Pagination;
using BuildingBlocksClient.Starter.DTOs;
using System.Net.Http.Json;

namespace Clients.BlazorWASM.Services
{
    public class CategoryService(CustomHttpClient _httpClient) : ICategoryService
    {
        public const string BaseUrl = "starter/category";

        public async Task<Guid> AddCategoryAsync(CategoryDto model)
        {
            var httpclient = await _httpClient.GetPrivateHttpClient();
            var response = await httpclient.PostAsJsonAsync($"{BaseUrl}/register", model);
            return await response.Content.ReadFromJsonAsync<Guid>();
        }

        public async Task<PaginatedResult<CategoryGetDto>> GetAllCategoriesAsync(PaginationRequest model)
        {
            var httpclient = await _httpClient.GetPrivateHttpClient();
            var response = await httpclient.GetFromJsonAsync<PaginatedResult<CategoryGetDto>>
            ($"{BaseUrl}" + "?PaginationRequest.PageIndex=" + model.PageIndex + "&PaginationRequest.PageSize=" + model.PageIndex);
            return response!;
        }

        public Task<CategoryDto> DeleteCategoryAsync(Guid categoryId)
        {
            throw new NotImplementedException();
        }


        public Task<CategoryDto> GetCategoryByIdAsync(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDto> UpdateCategoryAsync(CategoryDto model)
        {
            throw new NotImplementedException();
        }
    }
}
