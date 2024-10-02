using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce_API_Angular_Project.Models
{
    public class ImgTest
    {
       
            public int Id { get; set; }
            public byte[] ImageData { get; set; }

            [ForeignKey("User")]
            public int UserId { get; set; }
            public appUser? User { get; set; }
        
    }
}
