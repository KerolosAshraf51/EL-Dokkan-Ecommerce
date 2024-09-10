using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API_Angular_Project.Models
{
    public class Category
    {
         public int Id { get; set; }
         public string Name { get; set; }
         
         // Relationships
         public List<Product> Products { get; set; }
         public List<Brand> Brands { get; set; }
    }
}
