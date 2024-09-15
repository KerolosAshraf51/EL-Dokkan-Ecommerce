using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_Commerce_API_Angular_Project.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Relationships
        [ForeignKey("User")]
        public int UserId { get; set; }

        [JsonIgnore]
        public appUser? User { get; set; }
        public List<CartItem>? CartItems { get; set; }
    }
}
