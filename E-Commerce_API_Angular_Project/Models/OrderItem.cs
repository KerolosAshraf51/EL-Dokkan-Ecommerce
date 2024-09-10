using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_API_Angular_Project.Models
{
    public class OrderItem
    {
        public int Id { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least one.")]
        public int Quantity { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price at purchase must be greater than zero.")]
        public double PriceAtPurchase { get; set; }

        // Relationships
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
