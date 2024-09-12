using E_Commerce_API_Angular_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API_Angular_Project.Interfaces
{
    public interface IFavListRepo
    {
       public favList GetFavListByUserID(int userID);
        public void CreateFavList(favList favList);
        public List<favListItems> GetSortedFavList(int userId, string sortBy);
        public favList GetfavListById(int id);
        
        public void UpdateFavList(favList favList);
        public void DeleteFavList(int id);

        public void Save();
        


    }
}
