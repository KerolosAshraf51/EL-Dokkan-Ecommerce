
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using E_Commerce_API_Angular_Project.DTO;
using E_Commerce_API_Angular_Project.Models;
using E_Commerce_API_Angular_Project.IRepository;
using E_Commerce_API_Angular_Project.Interfaces;

//////////////////
using System;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using static System.Net.WebRequestMethods;

//////////////////////



namespace E_Commerce_API_Angular_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
     
        private readonly UserManager<appUser> userManager;
        private readonly IConfiguration config;
        public IAppUserRepo AppUserRepo { get; set; }
        public ICartRepo CartRepo { get; set; }
       
        public IMailRepo mailRepo { get; set; }
        public IFavListRepo FavListRepo { get; set; }
        public IUserOtpRepo UserOtpRepo { get; set; }

        public AccountController(UserManager<appUser> UserManager,
                                 IConfiguration config,
                                 IAppUserRepo appUserRepo,
                                 ICartRepo cartRepo,
                                 IMailRepo mailRepo,
                                 IFavListRepo favListRepo,
                                 IUserOtpRepo userOtpRepo)
        {
            userManager = UserManager;
            this.config = config;
            AppUserRepo = appUserRepo;
            CartRepo = cartRepo;
            this.mailRepo = mailRepo;
            FavListRepo = favListRepo;
            UserOtpRepo = userOtpRepo;
        }


        [HttpPost("Register")]//Post api/Account/Register
        public async Task<IActionResult> Register(RegisterDto UserFromRequest)
        {
            if (!AppUserRepo.IsEmailUnique(UserFromRequest.Email))
            {
                ModelState.AddModelError("UserInputErrors", "Email is already Exist");
            }
            else 
            {
                if (ModelState.IsValid)
                {
                    //save DB
                    appUser user = new appUser();
                    user.UserName = UserFromRequest.UserName;
                    user.Email = UserFromRequest.Email;
                    user.Address = UserFromRequest.Address;
                    user.PhoneNumber = UserFromRequest.Phone;
                    user.profileImageURL = UserFromRequest.profileImageURL;

                    IdentityResult result =
                        await userManager.CreateAsync(user, UserFromRequest.Password);




                    if (result.Succeeded)
                    {
                        //create cart and favList for user registered
                         var userId =
                            (await userManager.FindByNameAsync(UserFromRequest.UserName)).Id;

                        // CartRepo.createCart(userId);
                        // FavListRepo.createFavList(userId);

                        return Ok("Created successfully");
                    }
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("UserInputErrors", item.Description);
                    }

                }
           
            }
            return BadRequest(ModelState);
        }



        [HttpPost("RegisterAsAdmin")]//Post api/Account/RegisterAsAdmin
        public async Task<IActionResult> RegisterAsAdmin(RegisterDto UserFromRequest)
        {
            if (ModelState.IsValid)
            {
                //save DB
                appUser user = new appUser();
                user.UserName = UserFromRequest.UserName;
                user.Email = UserFromRequest.Email;
                user.Address = UserFromRequest.Address;
                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = DateTime.Now;

                IdentityResult result =
                    await userManager.CreateAsync(user, UserFromRequest.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    return Ok("Create");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("PAssword", item.Description);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]//Post api/Account/login
        public async Task<IActionResult> Login(LoginDto userFRomRequest)
        {
            if (ModelState.IsValid)
            {
                //check
                appUser userFromDb =
                    await userManager.FindByNameAsync(userFRomRequest.UserName);
                if (userFromDb != null)
                {
                    bool found =
                        await userManager.CheckPasswordAsync(userFromDb, userFRomRequest.Password);
                    if (found == true)
                    {
                        //generate token<==

                        List<Claim> UserClaims = new List<Claim>();

                        //Token Genrated id change (JWT Predefind Claims )
                        UserClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        UserClaims.Add(new Claim(ClaimTypes.NameIdentifier, userFromDb.Id.ToString()));
                        UserClaims.Add(new Claim(ClaimTypes.Name, userFromDb.UserName));

                        var UserRoles = await userManager.GetRolesAsync(userFromDb);

                        foreach (var roleNAme in UserRoles)
                        {
                            UserClaims.Add(new Claim(ClaimTypes.Role, roleNAme));
                        }

                        var SignInKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                                config["JWT:SecritKey"]));

                        SigningCredentials signingCred =
                            new SigningCredentials
                            (SignInKey, SecurityAlgorithms.HmacSha256);

                        //design token
                        JwtSecurityToken mytoken = new JwtSecurityToken(
                            audience: config["JWT:AudienceIP"],
                            issuer: config["JWT:IssuerIP"],
                            expires: DateTime.Now.AddHours(1),
                            claims: UserClaims,
                            signingCredentials: signingCred

                            );
                        //generate token response

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expiration = DateTime.Now.AddHours(1)//mytoken.ValidTo
                            //
                        });
                    }
                }
                ModelState.AddModelError("Username", "Username OR Password  Invalid");
            }
            return BadRequest(ModelState);
        }




        //*******************password problems*********************     



  




        [HttpPost("UpdatePassword")] //Post api/Account/UpdatePassword
           [Authorize]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordDTO UpdatePasswordDTO)
        {

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier); // Get the current user's ID
            var user = AppUserRepo.GetById(int.Parse(userId.Value));

            var passwordValid = await userManager.CheckPasswordAsync(user, UpdatePasswordDTO.CurrentPassword);
            if (!passwordValid)
            {
                return BadRequest("Current password is incorrect.");
            }

            var changePasswordResult = await userManager.ChangePasswordAsync(user, UpdatePasswordDTO.CurrentPassword, UpdatePasswordDTO.NewPassword);

            if (changePasswordResult.Succeeded)
            {
                return Ok("Password updated successfully.");
            }

            foreach (var error in changePasswordResult.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return BadRequest(ModelState);
        }




        [HttpPost("SendVerificationMail")]//Post api/Account/SendVerificationMail
        public async Task<IActionResult> SendVerificationMail(string toAddress)
        {
             int OTP = 0;

        var user =
                     (await userManager.FindByEmailAsync(toAddress));
            if (user == null)
            {
                return BadRequest("mail not found");
            }

            OTP = AppUserRepo.GenerateRandomOtp(user.Id);
            string body = "YOUR OTP IS : " + OTP;

            //SAVE OTP TO DATABASE
            UserOtp userOtp = new UserOtp()
            {
                OTP = OTP,
                userID = user.Id,
                CreatedAt = DateTime.Now
            };

            UserOtpRepo.Add(userOtp);
            UserOtpRepo.save();



            try
            {
               
               // mailRepo.SendEmail(toAddress, "OTP", body);
            }

            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
           
        

            return Ok("OTP sent successfully");
        }


      
        [HttpPost("ResetPassword")] //Post api/Account/ResetPassword
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO ResetPasswordDTO)
        {
           

            var user =
                     (await userManager.FindByEmailAsync(ResetPasswordDTO.Email));

            if (UserOtpRepo.isCorrect(user.Id, ResetPasswordDTO.otp)) 
            {
                if (UserOtpRepo.isValid(user.Id, ResetPasswordDTO.otp)) //expired or still valid
                {
                    var Result = await userManager.RemovePasswordAsync(user);

                    if (Result.Succeeded)
                    {
                        Result = await userManager.AddPasswordAsync(user, ResetPasswordDTO.newPassword);
                    }

                    if (!Result.Succeeded)
                    {
                        foreach (var error in Result.Errors)
                        {
                            ModelState.AddModelError(error.Code, error.Description);
                        }
                        return BadRequest(ModelState);
                    }
                }

                else { return BadRequest("this OTP is expired"); }
            }

            else { return BadRequest("wrong OTP , try again"); }
          


           

            return Ok("password changed successfully");
           
        }








    }



}
