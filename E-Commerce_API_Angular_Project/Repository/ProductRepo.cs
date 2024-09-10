using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;

namespace E_Commerce_API_Angular_Project.Repository
{
    public class ProductRepo : IProductRepo
    {
        EcommContext context;

        public ProductRepo(EcommContext _context)
        {
            this.context = _context;
        }


        public void Add(Product obj)
        {
            context.Products.Add(obj);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Product obj)
        {
            throw new NotImplementedException();
        }
    }
}
