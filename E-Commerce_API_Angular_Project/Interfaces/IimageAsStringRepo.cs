using E_Commerce_API_Angular_Project.Models;
using System.Collections.Generic;

namespace E_Commerce_API_Angular_Project.Interfaces
{
    public interface IimageAsStringRepo
    {
        public void Add(imageAsString imgTest);
        public void Remove(imageAsString imgTest);

        public List<imageAsString> getAllImages(int productId);

   
        public void save(); 

    }
}
