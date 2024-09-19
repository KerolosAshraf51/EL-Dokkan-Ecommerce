using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API_Angular_Project.Repository
{
    public class CartRepo:ICartRepo
    {
        EcommContext context;

        public CartRepo(EcommContext _context)
        {
            this.context = _context;
        }

        public void Add(Cart cart)
        {
            context.Add(cart);
        }
        public void Update(Cart cart)
        {
            context.Update(cart);
        }
        public void Delete(int id)
        {
           Cart deletedCart = GetById(id);
            if (deletedCart != null)
            {
                context.Carts.Remove(deletedCart);

            }
        }
        public List<Cart> GetAll()
        {

            List<Cart> Cartlist =
               context.Carts
               .Include(c=>c.CartItems)
               .ToList();
            return Cartlist;
        }
        public Cart GetById(int id)
        {
            Cart cart =
              context.Carts.Include(c=>c.CartItems)
              .Include(c=>c.User)
              .FirstOrDefault(c => c.Id == id);

            if(cart== null)
            {
                return null;
            }
            return cart;
        }

        public Product GetProductByCartItem(CartItem cartItem)
        {
            return context.Products.FirstOrDefault(p => p.Id == cartItem.ProductId);
        }

        public void ClearCart(int cartId)
        {
           
            var cart = context.Carts.Include(c => c.CartItems)
                                     .FirstOrDefault(c => c.Id == cartId);
            if (cart == null)
                throw new ArgumentException("Cart not found.");

            context.CartItems.RemoveRange(cart.CartItems);

           
            context.SaveChanges();
        }

        public Cart GetCartByUserId(int userId)
        {
          
            return context.Carts.Include(c => c.CartItems)
                               //  .ThenInclude(ci => ci.Product)
                                 .FirstOrDefault(c => c.UserId == userId);
        }
        public void Save()
        {
            context.SaveChanges();
        }

    }
}
