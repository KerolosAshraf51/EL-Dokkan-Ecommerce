using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API_Angular_Project.Repository
{
    public class FavListRepo : IFavListRepo
    {
        private readonly EcommContext _EcommContext;

        public FavListRepo(EcommContext ecommContext)
        {
            _EcommContext = ecommContext;
        }
        public async Task CreateFavList(int userID)
        {
            var favList = new favList
            {
                userId = userID,
                favListItems = new List<favListItems>()

            };
            _EcommContext.FavLists.Add(favList);
            await _EcommContext.SaveChangesAsync();
        }
8
        public Task<favList> GetFavListByUserID(int userID)
        {
            throw new NotImplementedException();
        }

        public Task<favList> GetSortedFavList(int userId, string sortBy)
        {
            throw new NotImplementedException();
        }
    }
}
