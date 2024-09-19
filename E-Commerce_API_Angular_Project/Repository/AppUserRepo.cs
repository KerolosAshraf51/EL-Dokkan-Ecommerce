using E_Commerce_API_Angular_Project.IRepository;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.EntityFrameworkCore;

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




        public bool IsEmailUnique(string email)
        {
            return !context.Users.Any(u => u.Email == email);
        }


        public int GenerateRandomOtp(int userId)
        {
            int length = 5;

            Random random = new Random();
            int minValue = (int)Math.Pow(10, length - 1);
            int maxValue = (int)Math.Pow(10, length) - 1;

            return random.Next(minValue, maxValue + 1); ;
        }






    }
}
