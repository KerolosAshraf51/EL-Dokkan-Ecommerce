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

       
        [HttpPost("CreateFavList")]
        public IActionResult CreateFavList(int userId)
        {
            var existingFavList = _favListRepo.GetFavListByUserID(userId);
            if (existingFavList != null)
            {
                return BadRequest("A favorite list already exists for this user.");
            }

            var favList = new favList
            {
                userId = userId,
                //favListItems = new List<favListItems>() //created from item controller not here
            };

            _favListRepo.CreateFavList(favList);

            

        }


        [HttpGet("GetFavListByUserId")]
        public IActionResult GetFavListByUserId(int userID)
        {
            var favList = _favListRepo.GetFavListByUserID(userID);
            if (favList == null)
            {
                return NotFound("Favorites list not found.");
            }
            return Ok(favList);
        }



        

    }
}