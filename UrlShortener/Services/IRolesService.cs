using UrlShortener.Models;

namespace UrlShortener.Services
{
    public interface IRolesService
    {
        Task<List<RoleVM>> GetRoles();
        Task<RoleVM> GetRoleByID(string Id);
        Task UpdateRole(RoleVM role);
        Task DeleteRole(string id);
    }
}