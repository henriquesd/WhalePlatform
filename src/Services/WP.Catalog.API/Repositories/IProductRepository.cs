using WP.Catalog.API.Models;
using WP.Core.Data;

namespace WP.Catalog.API.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<PagedResult<Product>> GetAll(int pageSize, int pageIndex, string? query = null);
        Task<Product> GetById(Guid id);
        Task<List<Product>> GetProductsById(string ids);
        Task<PagedResult<Product>> GetActiveProducts(int pageSize, int pageIndex);
        Task<IEnumerable<Product>> GetProductsWithLowStock(int threshold);

        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
    }
}