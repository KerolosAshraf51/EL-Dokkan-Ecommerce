using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API_Angular_Project.Repository
{
    public class BrandRepo : IBrandRepo
    {
        EcommContext context;

        public BrandRepo(EcommContext _context)
        {
            this.context = _context;
        }

        public void Add(Brand brand)
        {
            context.Brands.Add(brand);

        }

        public void Delete(int id)
        {
            Brand brand = GetById(id);
            context.Brands.Remove(brand);
        }

        public List<Brand> GetAll()
        {
            return context.Brands
                .Where(b => b.IsDeleted == false)
                .Include("categories")
                .Include("products")
                .ToList();
        }

        public Brand GetById(int id)
        {
            return context.Brands
                .Include("categories")
                .Include("products")
                .FirstOrDefault(b => b.Id == id);
        }

        public Brand GetByName(string name)
        {
            return context.Brands
               .Include("categories")
               .Include("products")
               .FirstOrDefault(b => b.Name == name);
        }

        public void Update(Brand brand)
        {
            context.Update(brand);
        }
        public void Save()
        {
            context.SaveChanges();
        }

    }
}
