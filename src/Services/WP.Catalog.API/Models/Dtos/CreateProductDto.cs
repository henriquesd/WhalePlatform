using System.ComponentModel.DataAnnotations;

namespace WP.Catalog.API.Models.Dtos
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 250 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 500 characters")]
        public string Description { get; set; }

        public bool Active { get; set; } = true;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        [StringLength(250, ErrorMessage = "Image URL must not exceed 250 characters")]
        [Url(ErrorMessage = "Image must be a valid URL")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Stock quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be 0 or greater")]
        public int StockQuantity { get; set; }
    }
}
