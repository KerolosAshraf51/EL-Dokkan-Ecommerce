using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce_API_Angular_Project.DTO
{
    public class productDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
 
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
       public List<IFormFile> files { get; set; }
    }
}
