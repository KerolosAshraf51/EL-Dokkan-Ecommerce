using E_Commerce_API_Angular_Project.Models;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API_Angular_Project.DTO
{
    public class OrderDTO
    {
        // //////////////////////////
        //public int OrderId { get; set; }
        //public int UserId { get; set; }
        ////public DateTime OrderDate { get; set; }
        //public double TotalAmount { get; set; }
        //public OrderStatus Status { get; set; } // Use the enum
        //public PaymentDTO PaymentDto { get; set; }
        ////public bool IsPaid { get; set; }
        ////public List<OrderItemDTO> OrderItems { get; set; } //= new List<OrderItem>();
        ////public List<PaymentDTO> Payments { get; set; } = new List<PaymentDTO>(); // New property
        //////////////////////////////////

        //public Order order {  get; set; }
        //public int userID { get; set; }
      //  public DateTime OrderDate { get; set; }
       
       // public DateTime UpdatedAt { get; set; }

       // [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than zero.")]
        public double TotalAmount { get; set; }
        //public string PaymentMethod { get; set; }
       // public OrderStatus Status { get; set; }
       // public bool IsDeleted { get; set; }
        public List<CartItemsDto> cartItems { get; set; }
    }
}
