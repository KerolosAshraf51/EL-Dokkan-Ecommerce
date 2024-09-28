using Azure.Core;
using E_Commerce_API_Angular_Project.DTO;
using E_Commerce_API_Angular_Project.IRepository;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using System.Security.Claims;

namespace E_Commerce_API_Angular_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserRepo appUser;
        private readonly UserManager<appUser> userManager;

        public AppUserController(IAppUserRepo _appuser , UserManager<appUser> userManager)
        {
            appUser = _appuser;
            this.userManager = userManager;
        }

        //****************Actions for any AppUser****************

        // view profile 

        [Authorize]
        [HttpGet("ViewProfile")]//Get api/AppUser/ViewProfile
        public ActionResult ViewProfile()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier); // Get the current user's ID
            var user = appUser.GetById(int.Parse(userId.Value));

            profileDTO userData = new profileDTO();

            userData.UserName = user.UserName;
            userData.Email = user.Email;
            userData.Phone = user.PhoneNumber;
            userData.Address = user.Address;
            userData.profileImageURL = user.profileImageURL;

            return Ok(userData);

        }

        //Edit profile

        [Authorize]
        [HttpPost("EditProfile")]//Post api/AppUser/EditProfile
        public ActionResult EditProfile(profileDTO data)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier); // Get the current user's ID
            var user = appUser.GetById(int.Parse(userId.Value));

            user.UserName = data.UserName;
            user.NormalizedUserName = data.UserName.ToUpper();
            user.Email = data.Email;
            user.NormalizedEmail = data.Email.ToUpper();
            user.Address = data.Address;
            user.profileImageURL = data.profileImageURL;
            user.UpdatedAt= DateTime.Now;
            
            appUser.Update(user);
            appUser.Save();

            return Ok(user);
        }

        //delete account

        [Authorize]
        [HttpDelete("DeleteProfile")]//Get api/AppUser/DeleteProfile
        public async Task<ActionResult> DeleteProfile(CurrentPasswordDTO currentPassword)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier); // Get the current user's ID
            var user = appUser.GetById(int.Parse(userId.Value));
            bool validPass =
                       await userManager.CheckPasswordAsync(user, currentPassword.Password);
            if (validPass == true)
            {
                appUser.Delete(user);
                appUser.Save();
                return Ok();
            }

            return BadRequest("invalid Password");
         
        }


       

        //****************Actions for client AppUser****************

        //add product to cart

        //add product to favList

        //add review to product

        // checkout product and confirm order

        //cancel order


        //****************Actions for Admin AppUser****************

        //add new category

        //add new product

        //edit product details

        //

    }
}
