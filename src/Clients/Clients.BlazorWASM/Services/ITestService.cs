using Clients.BlazorWASM.Models;

namespace Clients.BlazorWASM.Services
{
    public interface ITestService
    {
        Task<Product> AddProductAsync(Product model);
        Task<Product> UpdateProductAsync(Product model);
        Task<Product> DeleteProductAsync(int productId);
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int productId);
    }
}
