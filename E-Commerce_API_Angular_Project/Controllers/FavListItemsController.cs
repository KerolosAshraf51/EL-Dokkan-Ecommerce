using E_Commerce_API_Angular_Project.Interfaces;
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

        [HttpPost("{userId}/add/{productId}")]
        public IActionResult AddProductToFavList(int userId, int productId)
        {
            try
            {
                _favListItemsRepo.AddProductToFavList(userId, productId);
                return Ok("Product added");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{userId}/remove/{productId}")]

        public IActionResult RemoveProductFromFavList(int userId, int productId)
        {
            try
            {
                _favListItemsRepo.RemoveProductFromFavList(userId, productId);
                return Ok("Product Removed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
