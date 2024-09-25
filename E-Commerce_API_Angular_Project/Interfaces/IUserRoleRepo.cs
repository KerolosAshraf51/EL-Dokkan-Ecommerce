using E_Commerce_API_Angular_Project.Models;

namespace E_Commerce_API_Angular_Project.Interfaces
{

    public interface IUserRoleRepo
    {
        public  Task<bool> AssignRole(appUser user, string RoleName);
        public  Task<bool> RemoveRole(appUser user, string RoleName);
        public Task<List<string>> GetUserRoles(appUser user);
        public string GetRoleNameById(int roleId);
        public int getRoleId(string roleName);
    }
}
