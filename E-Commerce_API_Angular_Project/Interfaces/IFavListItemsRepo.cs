using E_Commerce_API_Angular_Project.Models;
using System.Diagnostics;

namespace E_Commerce_API_Angular_Project.Interfaces
{
    public interface IFavListItemsRepo
    {
       public void AddProductToFavList(favListItems favItem);
        public void RemoveProductFromFavList(favListItems favItem);
        public favListItems GetfavListItem(int userId, int productId);
        public favList GetFavListByUserId(int userId);


        List<favListItems> GetAllItemsInFavList(int userId);
        //Product GetProductById(int productId);

        public void UpdateFavListItems(favListItems favListItem);
        //public List<favListItems> GetSortedFavList(int userId, string sortBy);

        public void Save();
    }
}
