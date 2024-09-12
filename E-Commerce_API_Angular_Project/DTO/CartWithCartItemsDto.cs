namespace E_Commerce_API_Angular_Project.DTO
{
    public class CartWithCartItemsDto
    {

        public int CartId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<CartItemsOfCartDto> Items { get; set; }
    }
}
