namespace E_Commerce_API_Angular_Project.DTO
{
    public class CartItemsDto
    {
        #region shaimaa
       public int Price { get; set; }
        #endregion
        public int CartItemId { get; set; }
        public int CartId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
