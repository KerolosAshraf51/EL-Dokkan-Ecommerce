using E_Commerce_API_Angular_Project.DTO;
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
        private readonly EcommContext _EcommContext;


        public FavListController(IFavListRepo favListRepo, EcommContext ecommContext)
        {
            _favListRepo = favListRepo;
            _EcommContext = ecommContext;
        }


        [HttpPost("CreateFavList")]
        public IActionResult CreateFavList(FavListDto favListDto)
        {
            //var existingFavList = _favListRepo.GetFavListByUserID(userId);
            //if (existingFavList != null)
            //{
            //    return BadRequest("A favorite list already exists for this user.");
            //}

            //var favList = new favList
            //{
            //    userId = userId,
            //    favListItems = new List<favListItems>()
            //};

            //_favListRepo.CreateFavList(favList);


            //return Ok(favList);

            favList FavList = new favList();
            FavList.userId = favListDto.UserId;
            _favListRepo.CreateFavList(FavList);

            _favListRepo.Save();
            return Ok();
        }


        //[HttpPost("CreateFavList")]
        //public IActionResult CreateFavList(FavListDto favListDto)
        //{
        //    var user = _EcommContext.Users.FirstOrDefault(u => u.Id == favListDto.UserId);
        //    if (user == null)
        //    {
        //        return BadRequest("User not found.");
        //    }

        //    var favList = new favList
        //    {
        //        userId = favListDto.UserId,
        //        favListItems = new List<favListItems>()
        //    };

        //    _favListRepo.CreateFavList(favList);
        //    _favListRepo.Save();

        //    return Ok(favList);
        //}


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