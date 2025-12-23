using System.ComponentModel.DataAnnotations;

namespace WP.Catalog.API.Models.Requests
{
    public class DeductStockRequest
    {
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }
    }
}
