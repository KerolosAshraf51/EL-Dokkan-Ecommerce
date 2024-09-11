using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API_Angular_Project.Repository
{
    public class ProductRepo : IProductRepo
    {
        EcommContext context;

        public ProductRepo(EcommContext _context)
        {
            this.context = _context;
        }


        public void Add(Product product)
        {
            context.Products.Add(product);
        }

        public void Delete(int id)
        {
            Product product = GetById(id);
            context.Products.Remove(product);
        }

        public List<Product> GetAll()
        {
            return context.Products
                .Include("Reviews")
                .ToList();
        }

        public Product GetById(int id)
        {
            return context.Products
                .Include("Reviews")
                .FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetByName(string name)
        {
            return context.Products
                .Where(p => p.Name.StartsWith(name))
                .Include("Reviews")
                .ToList();
        }
        public void Update(Product product)
        {
            context.Update(product);
        }

        public void Save()
        {
            context.SaveChanges();
        }

      
    }
}
