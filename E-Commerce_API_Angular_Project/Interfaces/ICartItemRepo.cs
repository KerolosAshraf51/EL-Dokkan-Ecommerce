using E_Commerce_API_Angular_Project.Models;

namespace E_Commerce_API_Angular_Project.Interfaces
{
    public interface ICartItemRepo
    {
        public void Add(CartItem cartItem);
        public void Update(CartItem cartItem);
        public void Delete(int id);
        public List<CartItem> GetAll();
        public CartItem GetById(int id);

        public void Save();
    }
}
