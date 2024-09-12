using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API_Angular_Project.Repository
{
    public class FavListItemsRepo : IFavListItemsRepo
    {
        private readonly EcommContext _EcommContext;

        public FavListItemsRepo(EcommContext ecommContext)
        {
            _EcommContext = ecommContext;
        }

        public void AddProductToFavList(int userID, int productId)
        {
            var favList = _EcommContext.FavLists
                .Include(f => f.favListItems)
                .FirstOrDefault(f => f.userId == userID);

            if (favList != null)
            {
                var product = _EcommContext.Products.Find(productId);
               
                if (product != null)
                {
                    if(product.StockQuantity <= 0)
                    {
                        throw new InvalidOperationException("Product is out of stock.");
                    }

                    var favItem = new favListItems
                    {
                        favlistId = favList.Id,
                        ProductId = productId
                    };

                    _EcommContext.favListItems.Add(favItem);
                    _EcommContext.SaveChanges();
                }

            }
        }

        public void RemoveProductFromFavList(int userID, int productId)
        {
            var favList =  _EcommContext.FavLists
               .Include(f => f.favListItems)
               .FirstOrDefault(f => f.userId == userID);

            if (favList != null)
            {
                var favItem = favList.favListItems.FirstOrDefault(i => i.ProductId == productId);
                if (favItem != null)
                {
                    _EcommContext.favListItems.Remove(favItem);
                    _EcommContext.SaveChanges();
                }
            }

        }
    }
}
