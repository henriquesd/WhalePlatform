using WP.Core.DomainObjects;

namespace WP.Catalog.API.Models
{
   public class Product : Entity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public decimal Value { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Image{ get; set; }
        public int StockQuantity { get; set; }

        public void RemoveStock(int quantity)
        {
            if (StockQuantity >= quantity)
                StockQuantity -= quantity;
        }

        public bool IsAvailable(int quantity)
        {
            return Active && StockQuantity >= quantity;
        }
    }
}