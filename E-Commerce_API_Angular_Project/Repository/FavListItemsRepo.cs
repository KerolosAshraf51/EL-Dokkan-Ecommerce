using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;

namespace E_Commerce_API_Angular_Project.Repository
{
    public class FavListItemsRepo : IFavListItemsRepo
    {
        private readonly EcommContext _EcommContext;

        public FavListItemsRepo(EcommContext ecommContext)
        {
            _EcommContext = ecommContext;
        }

        public Task AddProductToFavList(int userId, int productId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveProductFromFavList(int userId, int productId)
        {
            throw new NotImplementedException();
        }
    }
}
