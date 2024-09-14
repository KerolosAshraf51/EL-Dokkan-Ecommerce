using E_Commerce_API_Angular_Project.Models;

namespace E_Commerce_API_Angular_Project.Interfaces
{
    public interface IReviewRepo
    {
        public void Add(Review review);
        public void Delete(int id);
        public List<Review> GetAll();
        public Review GetById(int id);
        public void Save();
    }
}
