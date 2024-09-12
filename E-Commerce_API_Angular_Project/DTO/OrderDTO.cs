using E_Commerce_API_Angular_Project.Models;

namespace E_Commerce_API_Angular_Project.DTO
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        //public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }
        public OrderStatus Status { get; set; } // Use the enum
        public PaymentDTO PaymentDto { get; set; }
        //public bool IsPaid { get; set; }
        //public List<OrderItemDTO> OrderItems { get; set; } //= new List<OrderItem>();
        //public List<PaymentDTO> Payments { get; set; } = new List<PaymentDTO>(); // New property
    }
}
