
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using E_Commerce_API_Angular_Project.DTO;
using E_Commerce_API_Angular_Project.Models;

namespace E_Commerce_API_Angular_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<appUser> userManager;
        private readonly IConfiguration config;

        public AccountController(UserManager<appUser> UserManager,IConfiguration config)
        {
            userManager = UserManager;
            this.config = config;
        }


        [HttpPost("Register")]//Post api/Account/Register
        public async Task<IActionResult> Register(RegisterDto UserFromRequest )
        {
            if(ModelState.IsValid)
            {
                //save DB
                appUser user = new appUser();
                user.UserName=UserFromRequest.UserName;
                user.Email = UserFromRequest.Email;
                user.Address = UserFromRequest.Address;

                IdentityResult result=
                    await userManager.CreateAsync(user, UserFromRequest.Password);       
                if (result.Succeeded)
                {
                    return Ok("Created successfully");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("PAssword", item.Description);
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
                appUser userFromDb=
                    await userManager.FindByNameAsync(userFRomRequest.UserName);
                if (userFromDb != null) {
                    bool found =
                        await userManager.CheckPasswordAsync(userFromDb, userFRomRequest.Password); ;
                    if (found == true)
                    {
                        //generate token<==

                        List<Claim> UserClaims = new List<Claim>();

                        //Token Genrated id change (JWT Predefind Claims )
                        UserClaims.Add(new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()));
                        UserClaims.Add(new Claim(ClaimTypes.NameIdentifier, userFromDb.Id.ToString()));
                        UserClaims.Add(new Claim(ClaimTypes.Name, userFromDb.UserName));

                        var UserRoles =await userManager.GetRolesAsync(userFromDb);
                        
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
                            expires:DateTime.Now.AddHours(1),
                            claims: UserClaims,
                            signingCredentials: signingCred

                            );
                        //generate token response

                        return Ok(new
                        {
                            token=new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expiration=DateTime.Now.AddHours(1)//mytoken.ValidTo
                            //
                        });
                    }
                }
                ModelState.AddModelError("Username", "Username OR Password  Invalid");
            }
            return BadRequest(ModelState);
        }
    }
}
