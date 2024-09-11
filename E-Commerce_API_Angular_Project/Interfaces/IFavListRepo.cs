using E_Commerce_API_Angular_Project.Models;

namespace E_Commerce_API_Angular_Project.Interfaces
{
    public interface IFavListRepo
    {
        Task<favList> GetFavListByUserID(int userID);
        Task CreateFavList(int userID);
        Task<favList> GetSortedFavList(int userId, string sortBy);
    }
}
