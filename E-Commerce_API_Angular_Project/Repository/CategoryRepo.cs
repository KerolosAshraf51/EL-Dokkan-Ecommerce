using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;

namespace E_Commerce_API_Angular_Project.Repository
{
    public class CategoryRepo : ICategoryRepo
    {
        EcommContext context;

        public CategoryRepo(EcommContext _context)
        {
            this.context = _context;
        }

        public void Add(Category obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAll()
        {
            throw new NotImplementedException();
        }

        public Category GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Category GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Category obj)
        {
            throw new NotImplementedException();
        }
    }
}
