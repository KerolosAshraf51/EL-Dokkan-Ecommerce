using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_API_Angular_Project.Models
{
    public class CartItem
    {
        public int Id { get; set; }
       

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least one.")]
        public int Quantity { get; set; }

        // Relationships

        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public Cart Cart { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
