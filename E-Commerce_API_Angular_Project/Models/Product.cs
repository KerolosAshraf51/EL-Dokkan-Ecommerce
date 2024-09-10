using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_API_Angular_Project.Models
{
    public class Product//device mob lab lcd 
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public double Price { get; set; }



        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be zero or greater.")]
        public int StockQuantity { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; } 

        // Relationships

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public List<Review> Reviews { get; set; }
    }
}
