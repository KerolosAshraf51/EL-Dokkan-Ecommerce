using E_Commerce_API_Angular_Project.DTO;
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
            product.IsDeleted = false;
            context.Products.Add(product);
        }

        public void Delete(int id)
        {
            Product product = GetById(id);
            context.Products.Remove(product);
        }

        public List<GetProductDTO> GetAll()
        {
            var prods = context.Products
                .Where(p => p.IsDeleted == false)
                .Include("Reviews")
                .ToList();


            List<GetProductDTO> returnDTO = new List<GetProductDTO>();
            foreach (Product product in prods)
            {
                GetProductDTO productDTO = new GetProductDTO();
                productDTO.BrandId = product.BrandId;
                productDTO.IsDeleted = product.IsDeleted;
                productDTO.images = context.imageAsStrings.Where(img => img.productId == product.Id).Select(img => img.Image).ToList();
                productDTO.Name = product.Name;
                productDTO.CategoryId = product.CategoryId;
                productDTO.Id = product.Id;
                productDTO.Description = product.Description;
                productDTO.CreatedAt = product.CreatedAt;
                productDTO.Price = product.Price;
                product.UpdatedAt = product.UpdatedAt;

                returnDTO.Add(productDTO);
            }

            return returnDTO;
        }

        public Product GetById(int id)
        {
            return context.Products
                .Include("Reviews")
                .FirstOrDefault(p => p.Id == id);

        }

        public GetProductDTO GetProductById(int id)
        {

            Product product = context.Products
          .Include("Reviews")
          .FirstOrDefault(p => p.Id == id);

            GetProductDTO returnDTO = new GetProductDTO();
            returnDTO.BrandId = product.BrandId;
            returnDTO.IsDeleted = product.IsDeleted;
            returnDTO.images = context.imageAsStrings.Where(img => img.productId == product.Id).Select(img => img.Image).ToList();
            returnDTO.Name = product.Name;
            returnDTO.CategoryId = product.CategoryId;
            returnDTO.Id = product.Id;
            returnDTO.Description = product.Description;
            returnDTO.CreatedAt = product.CreatedAt;
            returnDTO.Price = product.Price;
            returnDTO.UpdatedAt = product.UpdatedAt;


            return returnDTO;

        }

        public List<GetProductDTO> GetByName(string name)
        {
            List<Product> prods = context.Products
                .Where(p => p.Name.StartsWith(name))
                .Include("Reviews")
                .ToList();

            List<GetProductDTO> returnDTO = new List<GetProductDTO>();
            foreach (Product product in prods)
            {
                GetProductDTO productDTO = new GetProductDTO();
                productDTO.BrandId = product.BrandId;
                productDTO.IsDeleted = product.IsDeleted;
                productDTO.images = context.imageAsStrings.Where(img => img.productId == product.Id).Select(img => img.Image).ToList();
                productDTO.Name = product.Name;
                productDTO.CategoryId = product.CategoryId;
                productDTO.Id = product.Id;
                productDTO.Description = product.Description;
                productDTO.CreatedAt = product.CreatedAt;
                productDTO.Price = product.Price;
                product.UpdatedAt = product.UpdatedAt;

                returnDTO.Add(productDTO);
            }

            return returnDTO;
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
            Product product = context.Products
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
            if (quantity > product.StockQuantity) { return; }

            product.StockQuantity -= quantity;
        }
    }
}
