using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_API_Angular_Project.Models
{
    public class productImageList
    {
        public int Id { get; set; }
        public string imageURL { get; set; }


        [ForeignKey("productImage")]
        public int productImageId  { get; set; }
        public productImage productImage { get; set; }
    }
}
