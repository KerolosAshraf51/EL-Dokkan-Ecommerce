using E_Commerce_API_Angular_Project.IRepository;
using E_Commerce_API_Angular_Project.Models;

namespace E_Commerce_API_Angular_Project.Repository
{
    public class AppUserRepo : IAppUserRepo
    {
        EcommContext context;
        public AppUserRepo (EcommContext _context)
        {
            this.context = _context;
        }
        public void Add(appUser user) 
        {
            context.Add(user);
        }
        public void Update(appUser user)
        {
            context.Update(user);
        }
        public void Delete(int id)
        {
            appUser user = GetById(id);
            context.Remove(user);
        }
        public List<appUser> GetAll()
        {
            return context.AppUsers.ToList();
        }
        public appUser GetById(int id)
        {
            return context.AppUsers.FirstOrDefault(u=>u.Id==id);
        }
        public void Save()
        {
            context.SaveChanges();
        }

    }
}
