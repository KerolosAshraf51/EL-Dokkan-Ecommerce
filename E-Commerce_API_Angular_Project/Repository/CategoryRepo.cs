using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API_Angular_Project.Repository
{
    public class CategoryRepo : ICategoryRepo
    {
        EcommContext context;

        public CategoryRepo(EcommContext _context)
        {
            this.context = _context;
        }

        public void Add(Category category)
        {
            context.Add(category);

        }

        public void Delete(int id)
        {
            Category category = GetById(id);
            context.Categories.Remove(category);
        }

        public List<Category> GetAll()
        {
            return context.Categories
                .Where(c => c.IsDeleted == false)
               .Include("Products")
               .Include("Brands")
               .ToList();
        }

        public Category GetById(int id)
        {
            return context.Categories
                .Include("Products")
                .Include("Brands")
                .FirstOrDefault(c => c.Id == id);
        }

        public Category GetByName(string name)
        {
            return context.Categories
               .Include("Products")
                .Include("Brands")
                .FirstOrDefault(c => c.Name == name);
        }
        public void Update(Category category)
        {
            context.Update(category);

        }

        public void Save()
        {
            context.SaveChanges();
        }

    }
}
