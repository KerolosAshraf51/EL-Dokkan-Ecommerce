using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API_Angular_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavListItemsController : ControllerBase
    {
        private readonly IFavListItemsRepo _favListItemsRepo;

        public FavListItemsController(IFavListItemsRepo favListItemsRepo)
        {
            _favListItemsRepo = favListItemsRepo;
        }

        [HttpPost("AddProductToFavList")]
        public IActionResult AddProductToFavList(int userId, int productId)
        {
            try
            {
                var favList = _favListItemsRepo.GetFavListByUserId(userId);
                if (favList == null)
                    return NotFound("Favorite list not found.");

                var product = _favListItemsRepo.GetProductById(productId);
                if (product == null)
                    return NotFound("Product not found.");

                

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
    }
}
