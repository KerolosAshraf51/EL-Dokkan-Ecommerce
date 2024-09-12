namespace E_Commerce_API_Angular_Project.DTO
{
    public class OrderItemDTO
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }

        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; } // Price at the time of purchase
       // public List<OrderWithOrderItemsDTO> Items { get; set; }
    }
}
