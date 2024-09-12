using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API_Angular_Project.Repository
{
    public class FavListRepo : IFavListRepo
    {
        private readonly EcommContext _EcommContext;
        public string Id { get; set; }


        public FavListRepo(EcommContext ecommContext)
        {
            _EcommContext = ecommContext;
            Id = Guid.NewGuid().ToString();
        }

        public void CreateFavList(favList favList)
        {

            _EcommContext.FavLists.Add(favList);
            Save();
        }
        public favList GetFavListByUserID(int userID)
        {
            return _EcommContext.FavLists
                .Include(f => f.favListItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(f => f.userId == userID);

        }
        
        public favList GetfavListById(int id)
        {
            return _EcommContext.FavLists
                .Include(f => f.favListItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(f => f.Id == id);
        }
        public void DeleteFavList(int id)
        {
           var favList = GetfavListById(id);
            if (favList != null)
            {
                _EcommContext.FavLists.Remove(favList);
                Save();
            }
        }

        public void Save()
        {
            _EcommContext.SaveChanges();
        }

        public void UpdateFavList(favList favList)
        {
            _EcommContext.Update(favList);
            Save();
        }

        public List<favListItems> GetSortedFavList(int userId, string sortBy)
        {
            var favList = _EcommContext.FavLists
                .Include(f => f.favListItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(f => f.userId == userId);

            if (favList == null)
            {
                return new List<favListItems>(); // عشان فاضيه فاهترجع ليست فاضيه (كانها صفحه فاضيه مش هيتعمل عليها اي سورت 
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
                    //default:
                    //    throw new ArgumentException("Invalid sorting parameter.");
            }
            return SortedItems.ToList(); //


        }

      
    }
}
