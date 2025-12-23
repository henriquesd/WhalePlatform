using System.Net.Http.Json;
using WP.Web.Client.Models;

namespace WP.Web.Client.Services
{
    public interface ICatalogService
    {
        Task<PagedResult<ProductDto>?> GetProductsAsync(int pageSize = 10, int page = 1, string? query = null);
        Task<ProductDto?> GetProductByIdAsync(Guid id);
        Task<List<ProductDto>?> GetProductsByIdsAsync(string ids);
        Task<PagedResult<ProductDto>?> GetProductsByNameAsync(string name, int pageSize = 10, int page = 1);
        Task<PagedResult<ProductDto>?> GetActiveProductsAsync(int pageSize = 10, int page = 1);
        Task<List<ProductDto>?> GetLowStockProductsAsync(int threshold = 10);
        Task<(bool Success, string Message)> CreateProductAsync(ProductDto product);
        Task<(bool Success, string Message)> UpdateProductAsync(ProductDto product);
        Task<(bool Success, string Message)> DeleteProductAsync(Guid id);
        Task<(bool Success, string Message)> UpdateStockAsync(Guid id, int quantity);
        Task<(bool Success, string Message)> DeductStockAsync(Guid id, int quantity);
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

        public async Task<List<ProductDto>?> GetProductsByIdsAsync(string ids)
        {
            try
            {
                return await GetAsync<List<ProductDto>>($"api/catalog/products/list/{ids}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting products by ids: {ex.Message}");
                return null;
            }
        }

        public async Task<PagedResult<ProductDto>?> GetProductsByNameAsync(string name, int pageSize = 10, int page = 1)
        {
            try
            {
                var endpoint = $"api/catalog/products/by-name/{Uri.EscapeDataString(name)}?pageSize={pageSize}&page={page}";
                return await GetAsync<PagedResult<ProductDto>>(endpoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting products by name: {ex.Message}");
                return null;
            }
        }

        public async Task<PagedResult<ProductDto>?> GetActiveProductsAsync(int pageSize = 10, int page = 1)
        {
            try
            {
                var endpoint = $"api/catalog/products/active?pageSize={pageSize}&page={page}";
                return await GetAsync<PagedResult<ProductDto>>(endpoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting active products: {ex.Message}");
                return null;
            }
        }

        public async Task<List<ProductDto>?> GetLowStockProductsAsync(int threshold = 10)
        {
            try
            {
                return await GetAsync<List<ProductDto>>($"api/catalog/products/low-stock?threshold={threshold}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting low stock products: {ex.Message}");
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

        public async Task<(bool Success, string Message)> UpdateStockAsync(Guid id, int quantity)
        {
            try
            {
                var request = new { Quantity = quantity };
                var response = await _httpClient.PatchAsJsonAsync($"api/catalog/products/{id}/stock", request);

                if (response.IsSuccessStatusCode)
                {
                    return (true, "Stock updated successfully!");
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                return (false, $"Failed to update stock: {errorMessage}");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> DeductStockAsync(Guid id, int quantity)
        {
            try
            {
                var request = new { Quantity = quantity };
                var response = await PostAsync($"api/catalog/products/{id}/deduct-stock", request);

                if (response.IsSuccessStatusCode)
                {
                    return (true, "Stock deducted successfully!");
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                return (false, $"Failed to deduct stock: {errorMessage}");
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}");
            }
        }
    }
}

