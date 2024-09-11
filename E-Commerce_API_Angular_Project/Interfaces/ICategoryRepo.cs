using E_Commerce_API_Angular_Project.Models;

namespace E_Commerce_API_Angular_Project.Interfaces
{
    public interface ICategoryRepo
    {
        public void Add(Category obj);
        public void Update(Category obj);
        public void Delete(int id);
        public List<Category> GetAll();
        public Category GetById(int id);
        public Category GetByName(string name);
        public void Save();
    }
}
