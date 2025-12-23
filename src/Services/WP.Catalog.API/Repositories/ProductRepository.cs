using Dapper;
using Microsoft.EntityFrameworkCore;
using WP.Catalog.API.Data;
using WP.Catalog.API.Models;
using WP.Core.Data;

namespace WP.Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _context;

        public ProductRepository(CatalogContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<PagedResult<Product>> GetAll(int pageSize, int pageIndex, string? query = null)
        {
            var sql = @$"SELECT * FROM Products 
                      WHERE (@Name IS NULL OR Name LIKE '%' + @Name + '%') 
                      ORDER BY [Name] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS 
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(Id) FROM Products 
                      WHERE (@Name IS NULL OR Name LIKE '%' + @Name + '%')";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Name = query });

            var products = multi.Read<Product>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedResult<Product>()
            {
                List = products,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query
            };
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetProductsById(string ids)
        {
            var idsGuid = ids.Split(',')
                .Select(id => (Ok: Guid.TryParse(id, out var x), Value: x));

            if (!idsGuid.All(nid => nid.Ok)) return new List<Product>();

            var idsValue = idsGuid.Select(id => id.Value);

            return await _context.Products.AsNoTracking()
                .Where(p => idsValue.Contains(p.Id) && p.Active).ToListAsync();
        }

        public async Task<PagedResult<Product>> GetActiveProducts(int pageSize, int pageIndex)
        {
            var totalItems = await _context.Products
                .Where(p => p.Active)
                .CountAsync();

            var items = await _context.Products
                .Where(p => p.Active)
                .OrderBy(p => p.Name)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Product>()
            {
                List = items,
                TotalResults = totalItems,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }

        public async Task<IEnumerable<Product>> GetProductsWithLowStock(int threshold)
        {
            return await _context.Products
                .Where(p => p.Active && p.StockQuantity <= threshold)
                .OrderBy(p => p.StockQuantity)
                .ToListAsync();
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}