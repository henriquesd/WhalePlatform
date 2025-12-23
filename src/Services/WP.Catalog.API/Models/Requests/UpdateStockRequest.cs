using System.ComponentModel.DataAnnotations;

namespace WP.Catalog.API.Models.Requests
{
    public class UpdateStockRequest
    {
        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be 0 or greater")]
        public int Quantity { get; set; }
    }
}
