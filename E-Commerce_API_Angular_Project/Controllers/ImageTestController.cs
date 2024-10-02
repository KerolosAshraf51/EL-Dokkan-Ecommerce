using Application.Helpers;
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
using imageAsString = E_Commerce_API_Angular_Project.Models.imageAsString;

namespace E_Commerce_API_Angular_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageTestController : ControllerBase
    {
        private readonly ITestImg imgRepo;
        private readonly IimageAsStringRepo ImgStringRepo ;
  

        public ImageTestController(ITestImg imgTest, IimageAsStringRepo imgStringRepo)
        {
            imgRepo = imgTest;
            ImgStringRepo = imgStringRepo;
        }


        [HttpPost("addPic")]//Post api/ImageTest/addPic
        public async Task<IActionResult> addPic( picDTO pic)
        {
           
                ImgTest img = new ImgTest();
                img.ImageData = Convert.FromBase64String(pic.ImageData);
                img.UserId = pic.UserId;

                imgRepo.Add(img);
                 imgRepo.save();
                return Ok(img);

        }

        [HttpPost("addImageList")]//Post api/ImageTest/addImageList
        public async Task<IActionResult> addImageList([FromForm] List<IFormFile> files, [FromForm] string folderName, [FromBody] int productId)
        {
            List<string> imageNames;
            imageAsString tempImg;
            IimageAsStringRepo imgRepo;
            if (string.IsNullOrEmpty(folderName))
            {
                return BadRequest("Folder name cannot be empty.");
            }

            try
            {
                imageNames = await ImageSavingHelper.SaveImagesAsync(files, folderName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            foreach(var img in imageNames)
            {
                tempImg = new imageAsString();
                tempImg.productId = productId;
                tempImg.Image = img;
                ImgStringRepo.Add(tempImg);
                ImgStringRepo.save();

            }
            return Ok(imageNames);
        }



        [HttpGet("getImg")]//Post api/ImageTest/getImg
        public ActionResult getImg(int userId)
        {
         ImgTest img = imgRepo.getImg(userId);
           var image = Convert.ToBase64String(img.ImageData);
            return Ok(new { image = $"data:image/jpeg;base64,{image}" });

        }
    }
}
