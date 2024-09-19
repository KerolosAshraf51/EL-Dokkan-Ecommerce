using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_API_Angular_Project.Models
{
    public class UserOtp
    {
        public int Id { get; set; }
        public int OTP { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey("user")]
        public int userID { get; set; }
        public appUser user { get; set; }
    }
}
