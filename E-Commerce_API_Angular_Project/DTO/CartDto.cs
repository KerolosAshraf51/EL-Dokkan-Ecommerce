using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_API_Angular_Project.DTO
{
    public class CartDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UserId { get; set; }
    }
}
