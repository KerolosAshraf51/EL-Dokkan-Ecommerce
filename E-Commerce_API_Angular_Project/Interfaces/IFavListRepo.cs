using E_Commerce_API_Angular_Project.Models;

namespace E_Commerce_API_Angular_Project.Interfaces
{
    public interface IFavListRepo
    {
        favList GetFavListByUserID(int userID);
        void CreateFavList(favList favList);
        List<favListItems> GetSortedFavList(int userId, string sortBy);

    }
}
