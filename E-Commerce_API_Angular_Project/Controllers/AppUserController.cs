using E_Commerce_API_Angular_Project.DTO;
using E_Commerce_API_Angular_Project.IRepository;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce_API_Angular_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserRepo appUser;
        private readonly UserManager<appUser> userManager;

        public AppUserController(IAppUserRepo _appuser, UserManager<appUser> userManager)
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
            user.UpdatedAt = DateTime.Now;

            appUser.Update(user);
            appUser.Save();

            return Ok(user);
        }

        //delete account

        [Authorize]
        [HttpPost("DeleteProfile")]//Get api/AppUser/DeleteProfile
        public async Task<ActionResult> DeleteProfile([FromForm] string currentPassword)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier); // Get the current user's ID
            var user = appUser.GetById(int.Parse(userId.Value));
            bool validPass =
                       await userManager.CheckPasswordAsync(user, currentPassword);
            if (validPass == true)
            {
                appUser.Delete(user);
                appUser.Save();
                return Ok();
            }

            return BadRequest("invalid Password");

        }


        [Authorize(Roles = "admin")]
        [HttpPost("BlockUser")]//Get api/AppUser/BlockUser
        public async Task<ActionResult> BlockUser(int userId)
        {

            var user = appUser.GetById(userId);

            appUser.Block(user);
            appUser.Save();
            return Ok();

        }


        [Authorize(Roles = "admin")]
        [HttpGet("getAllUsers")]//Get api/AppUser/getAllUsers
        public ActionResult getAllUsers()
        {
            List<appUser> users = appUser.GetAll();
            List<profileDTO> userData = new List<profileDTO>();
            profileDTO Data = new profileDTO();

            foreach (var user in users)
            {
                Data = new profileDTO();
                Data.UserId = user.Id;
                Data.UserName = user.UserName;
                Data.Email = user.Email;
                Data.Phone = user.PhoneNumber;
                Data.Address = user.Address;
                Data.profileImageURL = user.profileImageURL;
                userData.Add(Data);
            }

            return Ok(userData);
        }
    }
}
