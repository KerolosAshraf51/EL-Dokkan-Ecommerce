using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;

namespace E_Commerce_API_Angular_Project.Repository
{
    public class TestImg : ITestImg
    {
        EcommContext context;
        public TestImg(EcommContext _context)
        {
            this.context = _context;
        }
        public void Add(ImgTest imgTest)
        {
            context.Add(imgTest);
        }
        public void Remove(ImgTest imgTest)
        {
            context.Remove(imgTest);
        }

        public void save ()
        {
            context.SaveChanges();
        }

        public ImgTest getImg(int userId)

        {
            return context.ImgTest.FirstOrDefault(i => i.UserId == userId);
        }
    }
}
