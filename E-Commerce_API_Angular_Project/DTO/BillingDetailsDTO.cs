using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API_Angular_Project.DTO
{
    public class BillingDetailsDTO
    {
        [Required(ErrorMessage = "First name is a required field.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is a required field.")]
        public string LastName { get; set; }

        public string? CompanyName { get; set; }

        [Required(ErrorMessage = "Country/Region is a required field.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Street address is a required field.")]
        public string StreetAddress { get; set; }

        [Required(ErrorMessage = "Town/City is a required field.")]
        public string TownCity { get; set; }

        [Required(ErrorMessage = "Phone is a required field.")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [Required(ErrorMessage = "Email is a required field.")]
        public string Email { get; set; }
        public string? Postcode { get; set; }
        public string? OrderNotes { get; set; }
    }
}
