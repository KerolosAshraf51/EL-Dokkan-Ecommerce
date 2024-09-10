using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_API_Angular_Project.Models
{
    public class Order
    {
        public int Id { get; set; }
   

        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; } 


        public string Status { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than zero.")]
        public double TotalAmount { get; set; }

        // Relationships
        
        [ForeignKey("User")]
        public int UserId { get; set; }
        public appUser User { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
