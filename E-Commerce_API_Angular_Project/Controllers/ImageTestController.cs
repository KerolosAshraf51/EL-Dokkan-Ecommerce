using E_Commerce_API_Angular_Project.DTO;
using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.IRepository;
using E_Commerce_API_Angular_Project.Migrations;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce_API_Angular_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageTestController : ControllerBase
    {
        private readonly ITestImg imgRepo;
  

        public ImageTestController(ITestImg imgTest)
        {
            imgRepo = imgTest;
         
        }


        [HttpPost("addPic")]//Post api/AppUser/addPic
        public ActionResult addPic(picDTO picDTO)
        {
            ImgTest img = new ImgTest();
            img.ImageData = picDTO.img;
            img.UserId = picDTO.UserId;

            imgRepo.Add(img);
            imgRepo.save();
            return Ok(img);

        }

        [HttpGet("getImg")]//Post api/AppUser/addPic
        public ActionResult getImg(int userId)
        {
         ImgTest img = imgRepo.getImg(userId);

            return Ok(img);

        }
    }
}
