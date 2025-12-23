using System.Net.Http.Json;
using WP.Web.Client.Models;

namespace WP.Web.Client.Services
{
    public interface ICatalogService
    {
        Task<PagedResult<ProductDto>?> GetProductsAsync(int pageSize = 10, int page = 1, string? query = null);
        Task<ProductDto?> GetProductByIdAsync(Guid id);
        Task<(bool Success, string Message)> CreateProductAsync(ProductDto product);
        Task<(bool Success, string Message)> UpdateProductAsync(ProductDto product);
        Task<(bool Success, string Message)> DeleteProductAsync(Guid id);
    }

    public class CatalogService : BaseHttpService, ICatalogService
    {
        public CatalogService(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<PagedResult<ProductDto>?> GetProductsAsync(int pageSize = 10, int page = 1, string? query = null)
        {
            try
            {
                var endpoint = $"api/catalog/products?pageSize={pageSize}&page={page}";
                if (!string.IsNullOrWhiteSpace(query))
                {
                    endpoint += $"&query={Uri.EscapeDataString(query)}";
                }

                return await GetAsync<PagedResult<ProductDto>>(endpoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting products: {ex.Message}");
                return null;
            }
        }

        public async Task<ProductDto?> GetProductByIdAsync(Guid id)
        {
            try
            {
                return await GetAsync<ProductDto>($"api/catalog/products/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting product: {ex.Message}");
                return null;
            }
        }

        public async Task<(bool Success, string Message)> CreateProductAsync(ProductDto product)
        {
            try
            {
                var response = await PostAsync("api/catalog/products", product);

                if (response.IsSuccessStatusCode)
                {
                    return (true, "Product created successfully!");
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                return (false, $"Failed to create product: {errorMessage}");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> UpdateProductAsync(ProductDto product)
        {
            try
            {
                var response = await PutAsync($"api/catalog/products/{product.Id}", product);

                if (response.IsSuccessStatusCode)
                {
                    return (true, "Product updated successfully!");
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                return (false, $"Failed to update product: {errorMessage}");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> DeleteProductAsync(Guid id)
        {
            try
            {
                var response = await DeleteAsync($"api/catalog/products/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return (true, "Product deleted successfully!");
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                return (false, $"Failed to delete product: {errorMessage}");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }
    }
}
