using E_Commerce_API_Angular_Project.Models;

namespace E_Commerce_API_Angular_Project.Interfaces
{
    public interface ICartRepo
    {
        public void Add(Cart cart);
        public void Update(Cart cart);
        public void Delete(int id);
        public List<Cart> GetAll();
        public Cart GetById(int id);
        public Product GetProductByCartItem(CartItem cartItem);
        public void ClearCart(int cartId);
        public Cart GetCartByUserId(int userId);
        public void CreateCart(int userId);

        public void Save();
    }
}
