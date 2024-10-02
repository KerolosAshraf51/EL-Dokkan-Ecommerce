using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_API_Angular_Project.Models
{
    public class imageAsString
    {

        public int Id { get; set; }
        public string Image { get; set; }

        [ForeignKey("product")]
        public int productId { get; set; }
        public Product? product { get; set; }
    }
}
