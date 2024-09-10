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

        public async Task<favList> GetFavListByUserID(int userID)
        {
            return await _EcommContext.FavLists
                .Include(f=> f.favListItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(f => f.userId == userID);
        }

        public async Task<favList> GetSortedFavList(int userId, string sortBy)
        {
            var favList = await _EcommContext.FavLists
                .Include(f => f.favListItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(f => f.userId == userId);
            if (favList == null)
            {
                return null;
            }
            var SortedItems = favList.favListItems.AsQueryable();
            switch (sortBy.ToLower())
            {
                case "price":
                    SortedItems = SortedItems.OrderBy(i => i.Product.Price);
                    break;
                case "rating":
                    SortedItems = SortedItems.OrderByDescending(i => i.Product.Reviews);
                    break;
                case "name":
                    SortedItems = SortedItems.OrderBy(i => i.Product.Name);
                    break;
            }
            favList.favListItems = SortedItems.ToList(); 
            return favList;

        }
    }
}
