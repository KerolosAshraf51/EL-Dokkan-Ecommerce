namespace E_Commerce_API_Angular_Project.DTO
{
    public class PaymentDTO
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } // e.g., "Credit Card", "PayPal"
        public DateTime PaymentDate { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
