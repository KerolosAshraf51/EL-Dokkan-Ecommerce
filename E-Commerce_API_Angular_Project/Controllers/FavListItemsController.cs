using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;
using E_Commerce_API_Angular_Project.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API_Angular_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavListItemsController : ControllerBase
    {
        private readonly IFavListItemsRepo _favListItemsRepo;
        IProductRepo _productRepo;
        IFavListRepo _favListItems;

        public FavListItemsController(IFavListItemsRepo favListItemsRepo, IProductRepo productRepo,IFavListRepo favList)
        {
            _favListItemsRepo = favListItemsRepo;
            _productRepo = productRepo; 
            _favListItems = favList;
        }

        [HttpPost("AddProductToFavList")]
        public IActionResult AddProductToFavList(int userId, int productId )
        {
            try
            {
                var favList = _favListItemsRepo.GetFavListByUserId(userId);
                if (favList == null)
                    return NotFound("Favorite list not found.");

                var product = _productRepo.GetById(productId);
                if (product == null)
                    return NotFound("Product not found.");

                
                var existingFavItem = favList.favListItems.FirstOrDefault(i => i.ProductId == productId);
                if (existingFavItem != null)
                {
                    return BadRequest("Product is already in the favorite list.");
                }

                var favItem = new favListItems
                {
                    favlistId = favList.Id,
                    ProductId = productId
                };

                _favListItemsRepo.AddProductToFavList(favItem);
                return Ok("Product added to favorites.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }





        [HttpDelete("RemoveProductFromFavList")]

        public IActionResult RemoveProductFromFavList(int userId, int productId)
        {
            try
            {
                var favItem = _favListItemsRepo.GetfavListItem(userId, productId);
                if (favItem == null)
                    return NotFound("Favorite item not found.");

                _favListItemsRepo.RemoveProductFromFavList(favItem);
                return Ok("Product removed from favorites.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


       




        //[HttpGet("GetSortedFavList")]
        //public IActionResult GetSortedFavList(int userId, string sortBy)
        //{
        //    try
        //    {
        //        var favList = _favListItemsRepo.GetFavListByUserId(userId);

        //        if (favList == null)
        //        {
        //            return NotFound("Favorite list not found.");
        //        }

        //        var sortedItems = favList.favListItems.AsQueryable();

        //        switch (sortBy.ToLower())
        //        {
        //            case "price":
        //                sortedItems = sortedItems.OrderBy(i => i.Product.Price);
        //                break;
        //            case "rating":
        //                sortedItems = sortedItems.OrderByDescending(i => i.Product.Reviews);
        //                break;
        //            case "name":
        //                sortedItems = sortedItems.OrderBy(i => i.Product.Name);
        //                break;
        //            default:
        //                return BadRequest("Invalid sorting parameter.");
        //        }

        //        return Ok(sortedItems.ToList());
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}



        [HttpGet("GetAllItemsInFavList")]
        public IActionResult GetAllItemsInFavList(int userId)
        {
            var items = _favListItemsRepo.GetAllItemsInFavList(userId);

            if (items == null || items.Count == 0)
            {
                return NotFound("No items found in the favorite list.");
            }


            if (items.Count == 1)
            {
                return Ok(new { message = "There is only one item in your favorite list.", items });
            }

            return Ok(items);
        }

    }
}
