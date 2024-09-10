using E_Commerce_API_Angular_Project.Models;

namespace E_Commerce_API_Angular_Project.IRepository
{
    public interface IAppUserRepo
    {



        public void Add(appUser user);
        public void Update(appUser user);
        public void Delete(int id);
        public List<appUser> GetAll();
        public appUser GetById(int id);
        public void Save();


    }
}
