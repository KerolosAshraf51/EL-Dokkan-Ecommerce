using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace E_Commerce_API_Angular_Project.Repository
{
    public class FavListItemsRepo : IFavListItemsRepo
    {
        private readonly EcommContext _EcommContext;
        

        public FavListItemsRepo(EcommContext ecommContext)
        {
            _EcommContext = ecommContext;
            

        }


        public void AddProductToFavList(favListItems favItem)
        {
            _EcommContext.favListItems.Add(favItem);
            Save();
            
        }

        public void RemoveProductFromFavList(favListItems favItem)
        {
            _EcommContext.favListItems.Remove(favItem);
            Save();
           
        }

        public favListItems GetfavListItem(int userId, int productId)
        {
            var favList = _EcommContext.FavLists
                .Include(f => f.favListItems)
                .FirstOrDefault(f => f.userId == userId);

            return favList.favListItems.FirstOrDefault(i => i.ProductId == productId);
        }

        public favList GetFavListByUserId(int userId)
        {
            return _EcommContext.FavLists
                .Include(f => f.favListItems)
                .FirstOrDefault(f => f.userId == userId);
        }

        public void Save()
        {
            _EcommContext.SaveChanges();
        }

        public void UpdateFavListItems(favListItems favListItems)
        {
            _EcommContext.Update(favListItems);
        }

        public List<favListItems> GetAllItemsInFavList(int userId)
        {
            var favList = _EcommContext.FavLists
                .Include(f => f.favListItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(f => f.userId == userId);

            return favList?.favListItems ?? new List<favListItems>();
        }
    }
}
