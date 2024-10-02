using E_Commerce_API_Angular_Project.Models;

namespace E_Commerce_API_Angular_Project.Interfaces
{
    public interface ITestImg
    {
        public void Add(ImgTest imgTest);
        public void Remove(ImgTest imgTest);

        public ImgTest getImg(int userId);
        public void save(); 

    }
}
