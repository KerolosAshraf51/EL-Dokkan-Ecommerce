using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace E_Commerce_API_Angular_Project.Repository
{
    public class FavListItemsRepo : IFavListItemsRepo
    {
        private readonly EcommContext _EcommContext;
        public string Id { get; set; }

        public FavListItemsRepo(EcommContext ecommContext)
        {
            _EcommContext = ecommContext;
            Id = Guid.NewGuid().ToString();

        }


        public void AddProductToFavList(favListItems favItem)
        {
            _EcommContext.favListItems.Add(favItem);
            Save();
        }

        public void RemoveProductFromFavList(favListItems favItem)
        {
            _EcommContext.favListItems.Remove(favItem);
            Save();
        }


        //public void AddProductToFavList(int userID, int productId)
        //{
        //    var favList = _EcommContext.FavLists
        //        .Include(f => f.favListItems)
        //        .FirstOrDefault(f => f.userId == userID);

        //    if (favList != null)
        //    {
        //        var product = _EcommContext.Products.Find(productId);

        //        if (product != null)
        //        {
        //            if (product.StockQuantity <= 0)
        //            {
        //                throw new InvalidOperationException("Product is out of stock.");
        //            }

        //            var favItem = new favListItems
        //            {
        //                favlistId = favList.Id,
        //                ProductId = productId
        //            };

        //            _EcommContext.favListItems.Add(favItem);
        //            _EcommContext.SaveChanges();
        //        }

        //    }
        //}

        

        //public void RemoveProductFromFavList(int userID, int productId)
        //{
        //    var favList = _EcommContext.FavLists
        //       .Include(f => f.favListItems)
        //       .FirstOrDefault(f => f.userId == userID);

        //    if (favList != null)
        //    {
        //        var favItem = favList.favListItems.FirstOrDefault(i => i.ProductId == productId);
        //        if (favItem != null)
        //        {
        //            _EcommContext.favListItems.Remove(favItem);
        //            _EcommContext.SaveChanges();
        //        }
        //    }

        //}

        public favListItems GetfavListItem(int userId, int productId)
        {
            var favList = _EcommContext.FavLists
                .Include(f => f.favListItems)
                .FirstOrDefault(f => f.userId == userId);

            return favList.favListItems.FirstOrDefault(i => i.ProductId == productId);
        }

        public favList GetFavListByUserId(int userId)
        {
            return _EcommContext.FavLists
                .Include(f => f.favListItems)
                .FirstOrDefault(f => f.userId == userId);
        }

        public Product GetProductById(int productId)
        {
            return _EcommContext.Products.Find(productId);
        }

        public void Save()
        {
            _EcommContext.SaveChanges();
        }

        public void UpdateFavListItems(favListItems favListItems)
        {
            _EcommContext.Update(favListItems);
        }

       
    }
}
