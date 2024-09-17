namespace E_Commerce_API_Angular_Project.DTO
{
    public class ResetPasswordDTO
    {
        public int otp {  get; set; }
        public string Email { get; set; }
        public string newPassword { get; set; }
    }
}
