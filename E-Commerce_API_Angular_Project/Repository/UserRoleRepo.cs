using E_Commerce_API_Angular_Project.Interfaces;
using E_Commerce_API_Angular_Project.Models;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce_API_Angular_Project.Repository
{
    public class UserRoleRepo : IUserRoleRepo
    {
        public UserManager<appUser> userManager { get; set; }
        public EcommContext context { get; set; }


        public UserRoleRepo(UserManager<appUser> UserManager,
                            EcommContext ecommContext) 
        {
            userManager = UserManager;
            context = ecommContext;
         
        }


        public async Task<bool> AssignRole(appUser user , string RoleName)
        {
           var res = await userManager.AddToRoleAsync(user, RoleName);

            if (res.Succeeded) 
            {
                return true;
            }
            return false;

        }


        public async Task<bool> RemoveRole(appUser user, string RoleName)
        {
            var res = await userManager.RemoveFromRoleAsync(user, RoleName);

            if (res.Succeeded)
            {
                return true;
            }
            return false;

        }


        public async Task<List<string>> GetUserRoles(appUser user)
        {
            var roles = await userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public string GetRoleNameById(int roleId) 
        {

            var role = context.Roles
                           .FirstOrDefault(r => r.Id == roleId);
                        

            return role?.Name;
        }

        public int getRoleId(string roleName)
        {
            var role = context.Roles
                     .FirstOrDefault(r => r.NormalizedName== roleName.ToUpper());

            
            if (role != null)
            {
                return role.Id;
            }

            return -1;
        }
    }
}
