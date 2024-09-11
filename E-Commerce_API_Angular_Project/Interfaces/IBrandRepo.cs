using E_Commerce_API_Angular_Project.Models;

namespace E_Commerce_API_Angular_Project.Interfaces
{
    public interface IBrandRepo
    {
        public void Add(Brand obj);
        public void Update(Brand obj);
        public void Delete(int id);
        public List<Brand> GetAll();
        public Brand GetById(int id);
        public Brand GetByName(string name);
        public void Save();

    }
}
