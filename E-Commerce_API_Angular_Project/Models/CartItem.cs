using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_Commerce_API_Angular_Project.Models
{
    public class CartItem
    {
        public int Id { get; set; }
       

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least one.")]
        public int Quantity { get; set; }
        //asdasd
        // Relationships

        [ForeignKey("Cart")]
        public int CartId { get; set; }
        [JsonIgnore]
        public Cart Cart { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
    }
}
