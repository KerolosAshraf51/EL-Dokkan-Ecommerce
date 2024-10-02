using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;

namespace E_Commerce_API_Angular_Project.Repository
{
    public class ImageAsStringRepo : IimageAsStringRepo
    {

        EcommContext context;
        public ImageAsStringRepo(EcommContext _context)
        {
            this.context = _context;
        }
        public void Add(imageAsString imgTest) 
        {
            context.Add(imgTest);
        }
        public void Remove(imageAsString imgTest)
        {
            context.Remove(imgTest);
        }

        public List<imageAsString> getAllImages(int productId)
        {
            return context.imageAsStrings.Where(i => i.productId == productId).ToList();
        }

  
        public void save()
        {
            context.SaveChanges();
        }
    }
}
