namespace E_Commerce_API_Angular_Project.DTO
{
    //public enum Shipping
    //{
    //    Flat_rate,
    //    Local_pickup
    //}
    public enum PaymentMethod
    {
        Direct_bank_transfer,
        Check_payments,
        Cash_on_delivery
    }
    public class PlaceOrderDTO
    {
        //public int OrderId { get; set; }
        public OrderDTO orderDTO {  get; set; }
        public int userID { get; set; }
        public BillingDetailsDTO BillingDetails { get; set; }
        //public Shipping Shipping { get; set; }
        // public double SubTotal { get; set; }
       // public double TotalAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
