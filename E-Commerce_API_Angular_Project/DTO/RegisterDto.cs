namespace E_Commerce_API_Angular_Project.DTO
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? profileImageURL { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
