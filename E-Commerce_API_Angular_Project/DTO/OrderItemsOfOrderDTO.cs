namespace E_Commerce_API_Angular_Project.DTO
{
    public class OrderItemsOfOrderDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; } // Price at the time of purchase
    }
}
