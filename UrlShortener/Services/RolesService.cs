using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using UrlShortener.Context;
using UrlShortener.Models;

namespace UrlShortener.Services
{
    public class RolesService : IRolesService
    {
        private readonly UrlShortenerDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesService(UrlShortenerDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }
        public async Task<List<RoleVM>> GetRoles()
        {
            return await _context.Queryable<AppRole>().Select(c => (RoleVM)c).ToListAsync();
        }
        public async Task<RoleVM> GetRoleByID(string Id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(c => c.Id == Id);
            return (RoleVM)role;
        }

        public async Task UpdateRole(RoleVM role)
        {
            var rolBD = await _context.Queryable<AppRole>().FirstOrDefaultAsync(c => c.Id == role.Id);
            if (rolBD == null)
            {
                throw new Exception();
            }

            rolBD.Name = role.Name;
            rolBD.NormalizedName = role.Name.ToUpper();
            rolBD.NumOfUrls = role.NumOfUrls;
            await _roleManager.UpdateAsync(rolBD);
        }

        public async Task DeleteRole(string id)
        {
            var rolBD = _context.Roles.FirstOrDefault(r => r.Id == id);
            if (rolBD == null)
            {
                throw new Exception("This role didnt exist");
            }

            var usuariosParaEsteRol = _context.UserRoles.Where(u => u.RoleId == id).Count();
            if (usuariosParaEsteRol > 0)
            {
                throw new Exception("There are users with this role");
            }

            await _roleManager.DeleteAsync(rolBD);
        }
    }
}
