using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_API_Angular_Project.Models
{
    public class favListItems
    {
        public int Id { get; set; }



        [ForeignKey("favList")]
        public int favlistId { get; set; }
        public favList favList {  get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
