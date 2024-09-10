using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_API_Angular_Project.Models
{
    public class appUser : IdentityUser<int>
    {  
        public string Address { get; set; }
        public string? profileImageURL { get; set; } //optional
    
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Relationships
        public List<Order> Orders { get; set; }
        public List<Review> Reviews { get; set;}

        [ForeignKey("Cart")]
        public int? CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
