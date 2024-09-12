using E_Commerce_API_Angular_Project.Models;

namespace E_Commerce_API_Angular_Project.DTO
{
    public class OrderWithOrderItemsDTO
    {
        //public int OrderItemId { get; set; }
        //public int OrderId { get; set; }
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime UpdatedAt { get; set; }

        public double TotalAmount { get; set; }
        public OrderStatus Status { get; set; }

        public string ClientName { get; set; }
        public List<OrderItemsOfOrderDTO> items { get; set; }

    }
}
