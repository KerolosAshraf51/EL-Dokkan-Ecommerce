using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace E_Commerce_API_Angular_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavListController : ControllerBase
    {
        private readonly IFavListRepo _favListRepo;

        public FavListController(IFavListRepo favListRepo)
        {
            _favListRepo = favListRepo;
        }

        [HttpGet("{userId}")]
        public IActionResult GetFavListByUserId(int userId )
        {
            var favList = _favListRepo.GetFavListByUserID( userId );
            if (favList == null)
            {
                return NotFound("Favorites list not found.");
            }
            return Ok(favList);
        }
        [HttpPost("{userId}")]
        public IActionResult CreateFavList(int userId)
        {
            var favList = new favList
            {
                userId = userId,
                favListItems = new List<favListItems>()
            };
            _favListRepo.CreateFavList(favList);
            return Ok(favList);
        }

        [HttpGet("{userId}/sorted")]
        public IActionResult GetSortedFavList(int userId, string sortBy)
        {
            try
            {
                var sortedFavList = _favListRepo.GetSortedFavList(userId, sortBy);
                return Ok(sortedFavList);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
