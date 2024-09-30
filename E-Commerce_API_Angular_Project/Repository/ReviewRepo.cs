using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;

namespace E_Commerce_API_Angular_Project.Repository
{
    public class ReviewRepo : IReviewRepo
    {
        public EcommContext context { get; }
        public ReviewRepo(EcommContext _context)
        {
            context = _context;
        }


        public void Add(Review review)
        {
            context.Add(review);
        }

        public void Delete(int id)
        {
            Review review = GetById(id);
            context.Remove(review);
        }

        public List<Review> GetAll()
        {
            return context.Reviews.ToList();
        }

        public Review GetById(int id)
        {
            return context.Reviews.FirstOrDefault(r => r.Id == id);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public List<Review> GetByProdId(int prodId)
        {
            return context.Reviews
                .Where(r => r.ProductId == prodId)
                .ToList();
        }
    }
}
