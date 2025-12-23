using Microsoft.AspNetCore.Mvc;
using WP.Catalog.API.Models;
using WP.Catalog.API.Repositories;
using WP.WebAPI.Core.Controllers;

namespace WP.Catalog.API.Controllers
{
    [Route("api")]
    public class CatalogController : MainController
    {
        private readonly IProductRepository _productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("catalog/products")]
        public async Task<PagedResult<Product>> Index(
            [FromQuery] int pageSize = 10, [FromQuery] int page = 1, [FromQuery] string? query = null)
        {
            var products = await _productRepository.GetAll(pageSize, page, query);

            return products;
        }

        [HttpGet("catalog/products/{id}")]
        public async Task<Product> ProductDetails(Guid id)
        {
            var product = await _productRepository.GetById(id);

            return product;
        }

        [HttpGet("catalog/products/list/{ids}")]
        public async Task<IEnumerable<Product>> GetProductById(string ids)
        {
            var products = await _productRepository.GetProductsById(ids);

            return products;
        }
    }
}