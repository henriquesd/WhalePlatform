using System.ComponentModel.DataAnnotations;

namespace WP.Web.Client.Models
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(250, ErrorMessage = "Name must be at most 250 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description must be at most 500 characters")]
        public string Description { get; set; } = string.Empty;

        public bool Active { get; set; } = true;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Value { get; set; }

        public DateTime RegisterDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Image URL is required")]
        [StringLength(250, ErrorMessage = "Image URL must be at most 250 characters")]
        public string Image { get; set; } = string.Empty;

        [Required(ErrorMessage = "Stock quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be 0 or greater")]
        public int StockQuantity { get; set; }
    }
}
