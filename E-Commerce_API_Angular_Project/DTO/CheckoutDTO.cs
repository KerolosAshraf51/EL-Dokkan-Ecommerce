using E_Commerce_API_Angular_Project.Models;

namespace E_Commerce_API_Angular_Project.DTO
{
    public class CheckoutDTO
    {
        public int userID { get; set; }
        public List<CartItemsDto> cartItems { get; set; }
        //public int orderID { get; set; }
        //public double TotalAmount { get; set; }
        //public OrderStatus Status { get; set; } // Use the enum
       // public List<CartItem> cartItems { get; set; }
      
        //public string PaymentMethod { get; set; }
    }
}
