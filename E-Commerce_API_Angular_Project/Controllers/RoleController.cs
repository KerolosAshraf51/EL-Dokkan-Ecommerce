using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API_Angular_Project.Controllers
{

    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole<int>> roleMan;

    

        public RoleController(RoleManager<IdentityRole<int>> _roleMan)
        {
            roleMan = _roleMan;
        }



        [HttpPost("addRole")]//Post api/Account/addRole
        public async Task<IActionResult> addRole(string roleName)
        {
            if (ModelState.IsValid)
            {
                IdentityRole<int> role = new IdentityRole<int>();
                role.Name = roleName;

                IdentityResult res = await roleMan.CreateAsync(role);

                if (res.Succeeded) 
                {
                    return Ok();
                }

                foreach (var e in res.Errors)
                {
                    ModelState.AddModelError("roleErrores", e.Description);
                }

            }

            return BadRequest(ModelState);
        }
    }
}
