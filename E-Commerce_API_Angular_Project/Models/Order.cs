using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_Commerce_API_Angular_Project.Models
{
    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Canceled,
        Returned,
        Refunded
    }
    public class Order
    {
        public int Id { get; set; }
   

        //[DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        //[DataType(DataType.Date)]
        public DateTime UpdatedAt { get; set; } 

        [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than zero.")]
        public double TotalAmount { get; set; }

        public OrderStatus Status { get; set; }

        //public bool IsPaid { get; set; }
        //public bool IsCanceled { get; set; }
       // public bool IsDeleted { get; set; } // For soft delete

        // Relationships

        [ForeignKey("User")]
        public int UserId { get; set; }
        public appUser User { get; set; }
        [ForeignKey("Payment")]
        public int? paymentId { get; set; } //temporary null 
        public Payment Payment { get; set; }

        
        public List<OrderItem> OrderItems { get; set; }
      
        
    }
}
