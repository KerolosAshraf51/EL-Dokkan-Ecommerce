
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API_Angular_Project.Models
{
    /*
     * Summary of Relationships
       --> User has many Orders, Reviews, and a Cart.
       --> Product has many OrderItems and Reviews.
       --> Category has many Products.
       --> Order has many OrderItems and belongs to a User.
       --> OrderItem belongs to an Order and a Product.
       --> Review belongs to a Product and a User.
       --> Cart belongs to a User and has many CartItems.
       --> CartItem belongs to a Cart and a Product.
     */
    public class EcommContext: IdentityDbContext<appUser, IdentityRole<int>, int>
    {
        public DbSet<appUser> AppUsers { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<favList> FavLists { get; set; }
        public DbSet<favListItems> favListItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<productImage> productImages {  get; set; }
        public DbSet<productImageList> productImageLists { get; set; }
        public DbSet<Review> Reviews { get; set; }





        public EcommContext(DbContextOptions<EcommContext> options):base(options)
        {
            
        }

    }
}
