using Microsoft.AspNetCore.Mvc;
using WP.Catalog.API.Models;
using WP.Catalog.API.Models.Requests;
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
        [ProducesResponseType(typeof(PagedResult<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagedResult<Product>>> GetAllProducts(
            [FromQuery] int pageSize = 10, 
            [FromQuery] int page = 1, 
            [FromQuery] string? query = null)
        {
            if (pageSize <= 0 || page <= 0)
            {
                AddProcessingError("PageSize and Page must be greater than 0");
                return CustomResponse();
            }

            var products = await _productRepository.GetAll(pageSize, page, query);
            return CustomResponse(products);
        }

        [HttpGet("catalog/products/{id:guid}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> GetProductById(Guid id)
        {
            if (id == Guid.Empty)
            {
                AddProcessingError("Id is not valid");
                return CustomResponse();
            }

            var product = await _productRepository.GetById(id);

            if (product == null)
            {
                AddProcessingError("Product not found");
                return CustomResponse();
            }

            return CustomResponse(product);
        }

        [HttpGet("catalog/products/list/{ids}")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByIds(string ids)
        {
            if (string.IsNullOrWhiteSpace(ids))
            {
                AddProcessingError("Ids parameter is required");
                return CustomResponse();
            }

            var products = await _productRepository.GetProductsById(ids);
            return CustomResponse(products);
        }

        [HttpPost("catalog/products")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            product.Id = Guid.NewGuid();
            product.RegisterDate = DateTime.UtcNow;

            _productRepository.Add(product);

            if (await _productRepository.UnitOfWork.Commit())
            {
                return CreatedAtAction(
                    nameof(GetProductById),
                    new { id = product.Id },
                    product);
            }

            AddProcessingError("Failed to create product");
            return CustomResponse();
        }

        [HttpPut("catalog/products/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateProduct(Guid id, [FromBody] Product product)
        {
            if (id == Guid.Empty)
            {
                AddProcessingError("Id is not valid");
                return CustomResponse();
            }

            if (!ModelState.IsValid)
            {
                return CustomResponse(ModelState);
            }

            var existingProduct = await _productRepository.GetById(id);

            if (existingProduct == null)
            {
                AddProcessingError($"Product with id {id} not found");
                return CustomResponse();
            }

            product.Id = id;
            product.RegisterDate = existingProduct.RegisterDate;

            _productRepository.Update(product);

            if (await _productRepository.UnitOfWork.Commit())
            {
                return NoContent();
            }

            AddProcessingError("Failed to update product");
            return CustomResponse();
        }

        [HttpDelete("catalog/products/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteProduct(Guid id)
        {
            if (id == Guid.Empty)
            {
                AddProcessingError("Id is not valid");
                return CustomResponse();
            }

            var product = await _productRepository.GetById(id);

            if (product == null)
            {
                AddProcessingError($"Product with id {id} not found");
                return CustomResponse();
            }

            _productRepository.Delete(product);

            if (await _productRepository.UnitOfWork.Commit())
            {
                return NoContent();
            }

            AddProcessingError("Failed to delete product");
            return CustomResponse();
        }

        [HttpGet("catalog/products/by-name/{name}")]
        [ProducesResponseType(typeof(PagedResult<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagedResult<Product>>> GetProductsByName(
            string name,
            [FromQuery] int pageSize = 10,
            [FromQuery] int page = 1)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                AddProcessingError("Name parameter is required");
                return CustomResponse();
            }

            var products = await _productRepository.GetAll(pageSize, page, name);
            return CustomResponse(products);
        }

        [HttpGet("catalog/products/active")]
        [ProducesResponseType(typeof(PagedResult<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagedResult<Product>>> GetActiveProducts(
            [FromQuery] int pageSize = 10,
            [FromQuery] int page = 1)
        {
            var products = await _productRepository.GetActiveProducts(pageSize, page);
            return CustomResponse(products);
        }

        [HttpGet("catalog/products/low-stock")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsWithLowStock(
            [FromQuery] int threshold = 10)
        {
            var products = await _productRepository.GetProductsWithLowStock(threshold);
            return CustomResponse(products);
        }

        [HttpPatch("catalog/products/{id:guid}/stock")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateProductStock(Guid id, [FromBody] UpdateStockRequest request)
        {
            if (id == Guid.Empty)
            {
                AddProcessingError("Id is not valid");
                return CustomResponse();
            }

            var product = await _productRepository.GetById(id);

            if (product == null)
            {
                AddProcessingError($"Product with id {id} not found");
                return CustomResponse();
            }

            if (request.Quantity < 0)
            {
                AddProcessingError("Stock quantity cannot be negative");
                return CustomResponse();
            }

            product.StockQuantity = request.Quantity;
            _productRepository.Update(product);

            if (await _productRepository.UnitOfWork.Commit())
            {
                return NoContent();
            }

            AddProcessingError("Failed to update product stock");
            return CustomResponse();
        }

        [HttpPost("catalog/products/{id:guid}/deduct-stock")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeductStock(Guid id, [FromBody] DeductStockRequest request)
        {
            if (id == Guid.Empty)
            {
                AddProcessingError("Id is not valid");
                return CustomResponse();
            }

            var product = await _productRepository.GetById(id);

            if (product == null)
            {
                AddProcessingError($"Product with id {id} not found");
                return CustomResponse();
            }

            if (!product.IsAvailable(request.Quantity))
            {
                AddProcessingError("Insufficient stock or product is not active");
                return CustomResponse();
            }

            product.RemoveStock(request.Quantity);
            _productRepository.Update(product);

            if (await _productRepository.UnitOfWork.Commit())
            {
                return NoContent();
            }

            AddProcessingError("Failed to deduct stock");
            return CustomResponse();
        }
    }
}