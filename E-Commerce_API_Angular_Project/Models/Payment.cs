using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_API_Angular_Project.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string PaymentMethod { get; set; } // e.g., "Credit Card", "PayPal"
        public DateTime PaymentDate { get; set; }
        public bool IsSuccessful { get; set; }

        // Navigation property
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
