using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_API_Angular_Project.Models
{
    public class productImage
    {
        public int Id { get; set; }

        //Relations
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public List<productImageList> Items { get; set; }
    }
}
