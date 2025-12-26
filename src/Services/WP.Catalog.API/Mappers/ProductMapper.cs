using WP.Catalog.API.Models;
using WP.Catalog.API.Models.Dtos;

namespace WP.Catalog.API.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToDto(Product product)
        {
            if (product == null) return null;

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Active = product.Active,
                Value = product.Value,
                RegisterDate = product.RegisterDate,
                Image = product.Image,
                StockQuantity = product.StockQuantity
            };
        }

        public static Product ToEntity(CreateProductDto dto)
        {
            if (dto == null) return null;

            return new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                Active = dto.Active,
                Value = dto.Value,
                Image = dto.Image,
                StockQuantity = dto.StockQuantity,
                RegisterDate = DateTime.UtcNow
            };
        }

        public static void UpdateEntity(Product product, UpdateProductDto dto)
        {
            if (product == null || dto == null) return;

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Active = dto.Active;
            product.Value = dto.Value;
            product.Image = dto.Image;
            product.StockQuantity = dto.StockQuantity;
        }

        public static List<ProductDto> ToDtoList(IEnumerable<Product> products)
        {
            if (products == null) return new List<ProductDto>();

            return products.Select(ToDto).ToList();
        }

        public static PagedResult<ProductDto> ToPagedDto(PagedResult<Product> pagedResult)
        {
            if (pagedResult == null) return null;

            return new PagedResult<ProductDto>
            {
                List = ToDtoList(pagedResult.List),
                TotalResults = pagedResult.TotalResults,
                PageIndex = pagedResult.PageIndex,
                PageSize = pagedResult.PageSize,
                Query = pagedResult.Query
            };
        }
    }
}
