using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
            product.IsDeleted = false;
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
                .Where(p => p.IsDeleted == false)
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

        public List<Product> OrderBy(string str)
        {

            switch (str)
            {
                case "Name": return context.Products.OrderBy(p => p.Name).ToList(); 
                case "Price": return context.Products.OrderBy(p => p.Price).ToList();
                case "Quntatity": return context.Products.OrderBy(p => p.StockQuantity).ToList();

                default: return context.Products.ToList();
            }
        }

        public List<Product> GetByCategoryId(int categoryId)
        {
            return context.Products
                .Where(p => p.CategoryId == categoryId)
                .ToList();
        }

        public List<Product> GetByBrandId(int brandId)
        {
            return context.Products
                .Where(p => p.BrandId == brandId)
                .ToList();
        }

        public void IncreaseQty(int prodId, int quantity)
        {
            Product product =  context.Products
                .FirstOrDefault(p => p.Id == prodId);

            if (product == null) { return; }
            if (quantity == 0) { return; }
            
            product.StockQuantity += quantity;
        }

        public void DecreaseQty(int prodId, int quantity)
        {
            Product product = context.Products
                .FirstOrDefault(p => p.Id == prodId);

            if (product == null) { return; }
            if(quantity > product.StockQuantity) { return; }

            product.StockQuantity -= quantity;
        }
    }
}
