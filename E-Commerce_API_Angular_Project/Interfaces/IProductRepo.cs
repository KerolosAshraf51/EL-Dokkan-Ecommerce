using E_Commerce_API_Angular_Project.Models;

namespace E_Commerce_API_Angular_Project.Interfaces
{
    public interface IProductRepo
    {
        public void Add(Product obj);
        public void Update(Product obj);
        public void Delete(int id);
        public List<Product> GetAll();
        public Product GetById(int id);
        public List<Product> GetByName(string name);
        public List<Product> OrderBy(string str);
        public List<Product> GetByCategoryId(int categoryId);
        public List<Product> GetByBrandId(int brandId);
        public void Save();
    }
}
