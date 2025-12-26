using WP.Catalog.API.Models;
using WP.Catalog.API.Models.Responses;

namespace WP.Catalog.API.Mappers
{
    public static class ProductResponseMapper
    {
        public static ProductResponse ToResponse(Product product)
        {
            if (product == null) return null;

            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Active = product.Active,
                Value = product.Value,
                RegisterDate = product.RegisterDate,
                Image = product.Image,
                StockQuantity = product.StockQuantity,
                IsAvailable = product.IsAvailable(1)
            };
        }

        public static List<ProductResponse> ToResponseList(IEnumerable<Product> products)
        {
            if (products == null) return new List<ProductResponse>();

            return products.Select(ToResponse).ToList();
        }

        public static PagedResult<ProductResponse> ToPagedResponse(PagedResult<Product> pagedResult)
        {
            if (pagedResult == null) return null;

            return new PagedResult<ProductResponse>
            {
                List = ToResponseList(pagedResult.List),
                TotalResults = pagedResult.TotalResults,
                PageIndex = pagedResult.PageIndex,
                PageSize = pagedResult.PageSize,
                Query = pagedResult.Query
            };
        }
    }
}
