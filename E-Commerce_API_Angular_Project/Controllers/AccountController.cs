
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
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using static System.Net.WebRequestMethods;
using E_Commerce_API_Angular_Project.Repository;

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
        public IUserRoleRepo IUserRoleRepo { get; set; }
        public AccountController(UserManager<appUser> UserManager,
                                 IConfiguration config,
                                 IAppUserRepo appUserRepo,
                                 ICartRepo cartRepo,
                                 IMailRepo mailRepo,
                                 IFavListRepo favListRepo,
                                 IUserOtpRepo userOtpRepo,
                                 IUserRoleRepo IuserRoleRepo)
        {
            userManager = UserManager;
            this.config = config;
            AppUserRepo = appUserRepo;
            CartRepo = cartRepo;
            this.mailRepo = mailRepo;
            FavListRepo = favListRepo;
            UserOtpRepo = userOtpRepo;
            IUserRoleRepo = IuserRoleRepo;
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
                        //isdeleted ? 
                        user.IsDeleted = false;

                        //create cart and favList for user registered
                        var userId =
                           (await userManager.FindByNameAsync(UserFromRequest.UserName)).Id;

                        // create Cart for new user

                        Cart cart = new Cart();
                        cart.CreatedAt = DateTime.Now;
                        cart.UpdatedAt = DateTime.Now;
                        cart.UserId = userId;


                        CartRepo.Add(cart);
                        CartRepo.Save();
                        //creat FavList for new user

                        favList FavList = new favList();
                        FavList.userId = userId;
                        FavListRepo.CreateFavList(FavList);
                        FavListRepo.Save();


                        //=====================================

                        return Ok();
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
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RegisterAsAdmin(RegisterDto UserFromRequest)
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
                    user.CreatedAt = DateTime.Now;
                    user.UpdatedAt = DateTime.Now;

                    IdentityResult result =
                        await userManager.CreateAsync(user, UserFromRequest.Password);
                    if (result.Succeeded)
                    { 
                        await IUserRoleRepo.AssignRole(user, "admin");
                        user.IsDeleted = false;
                        return Ok();
                    }
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("UserInputErrors", item.Description);
                    }
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
                if (userFromDb != null && userFromDb.IsDeleted == false)
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
                            expires: DateTime.Now.AddYears(1),
                            claims: UserClaims,
                            signingCredentials: signingCred

                            );
                        //generate token response

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(mytoken),

                            expiration = DateTime.Now.AddYears(1)//mytoken.ValidTo

                            ,roles = UserRoles

                        });
                    }
                }
                ModelState.AddModelError("Username", "Username OR Password  Invalid");
            }
            return BadRequest(ModelState);
        }


        [HttpGet("getCurrentUserID")]//Post api/Account/getCurrentUserID
        [Authorize]
        public async Task<IActionResult> getCurrentUserID()
        {
            int userID = int.Parse((User.Claims
                                       .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier))
                                       .Value);

            return Ok(userID);
        }



        [HttpGet("GetUserById")]//Post api/Account/GetUserById
        [Authorize]
        public async Task<IActionResult> GetUserById(int userId)
        {
            appUser user = AppUserRepo.GetById(userId);
            if (user != null)
            {
                profileDTO profileDTO = new profileDTO();
                profileDTO.UserName = user.UserName;
                profileDTO.profileImageURL = user.profileImageURL;
                profileDTO.Phone = user.PhoneNumber;
                profileDTO.Email = user.Email;
                profileDTO.Address = user.Address;
                return Ok(profileDTO);
            }
            return BadRequest("USER NOT FOUND , ENTER VALID ID");
        }


        [HttpPost("UpdateUserInfo")]//Post api/Account/UpdateUserInfo
        [Authorize]
        public async Task<IActionResult> UpdateUserInfo(int userId, profileDTO profileDTO)
        {
            appUser user = AppUserRepo.GetById(userId);
            if (user != null)
            {
                if (!AppUserRepo.IsEmailUnique(profileDTO.Email))
                {
                    ModelState.AddModelError("UserInputErrors", "Email is already Exist");
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        user.UserName = profileDTO.UserName; // not allowed
                        user.NormalizedUserName = profileDTO.UserName.ToUpper();
                        user.profileImageURL = profileDTO.profileImageURL;
                        user.PhoneNumber = profileDTO.Phone;
                        user.Email = profileDTO.Email;
                        user.NormalizedEmail = profileDTO.Email.ToUpper();
                        user.Address = profileDTO.Address;
                        AppUserRepo.Update(user);
                        AppUserRepo.Save();
                        return Ok(profileDTO);
                    }
                }

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
                return Ok();
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

            var res = await mailRepo.SendEmail(toAddress, "OTP", body);

            if (res == "1")
            {
                return Ok();
            }

            return BadRequest(res);
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





            return Ok();

        }



        //*****************************ROLES MANAGMENT*****************************

        [HttpPost("asssinRole")]//Post api/Account/asssinRole
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> asssinRole(UserRoleDTO userRoleDTO)
        {
            var user = AppUserRepo.GetById(userRoleDTO.userId);
            var RoleName = IUserRoleRepo.GetRoleNameById(userRoleDTO.RoleId);

            var res = await IUserRoleRepo.AssignRole(user, RoleName);

            if (res)
            {
                return Ok();
            }

            return BadRequest();
        }


        [HttpPost("RemoveRole")]//Post api/Account/RemoveRole
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> RemoveRole(UserRoleDTO userRoleDTO)
        {
            var user = AppUserRepo.GetById(userRoleDTO.userId);
            if (user == null)
            {
                return BadRequest("userNotFound");
            }
            var RoleName = IUserRoleRepo.GetRoleNameById(userRoleDTO.RoleId);
            if (RoleName == null)
            {
                return BadRequest("RoleNotFound");
            }
            var res = await IUserRoleRepo.RemoveRole(user, RoleName);

            if (res)
            {
                return Ok();
            }

            return BadRequest("user not assigned to this role");
        }



        [HttpPost("GetUserRoles")]//Post api/Account/GetUserRoles
        [Authorize]
        public async Task<IActionResult> GetUserRoles(int userId)
        {
            var user = AppUserRepo.GetById(userId);
            if (user != null)
            {
                List<string> roles = await IUserRoleRepo.GetUserRoles(user);
                if (roles.Count > 0)
                {
                    return Ok(roles);
                }
                return BadRequest("no roles assigned to this user");
            }

            return BadRequest("userNotFound");


        }


        [HttpPost("GetRoleId")]//Post api/Account/GetRoleId
        [Authorize]
        public async Task<IActionResult> GetRoleId(string roleName)
        {
            int RoleId = IUserRoleRepo.getRoleId(roleName);
            if (RoleId >= 0)
            {
                return Ok(RoleId);
            }
            return BadRequest("role not found");
        }

    }



}
