using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UrlShortener.Models;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    public class RolController : Controller
    {
        private readonly ILogger<RolController> _logger;
        private readonly IRolesService _rolesService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolController(ILogger<RolController> logger, IRolesService rolesService, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _rolesService = rolesService;
            _roleManager = roleManager;
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
            var roles = await _rolesService.GetRoles();
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRole(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return View();
            }
            else
            {
                var role = await _rolesService.GetRoleByID(id);
                return View(role);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> UpdateRole(RoleVM rol)
        {
            var roleDb = await _rolesService.GetRoleByID(rol.Id);
            if (roleDb.Id == rol.Id && roleDb.NumOfUrls == rol.NumOfUrls)
            {
                TempData["Error"] = "There are not any change";
                return RedirectToAction(nameof(Index));
            }
            if (await _roleManager.RoleExistsAsync(rol.Name) && rol.Name != roleDb.Name)
            {
                TempData["Error"] = "El rol ya existe";
                return RedirectToAction(nameof(Index));
            }
            try
            {
                await _rolesService.UpdateRole(rol);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }

            TempData["Correcto"] = "Rol editado correctamente";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            try
            {
                await _rolesService.DeleteRole(id);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["Correcto"] = "This Rol was deleted";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public IActionResult CreateRol()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> CreateRol(RoleVM role)
        {
            if (await _roleManager.RoleExistsAsync(role.Name))
            {
                TempData["Error"] = "This name´s rol exits";
                return RedirectToAction(nameof(Index));
            }

            await _roleManager.CreateAsync(new IdentityRole() { Name = role.Name });
            TempData["Correcto"] = "Rol creado correctamente";
            return RedirectToAction(nameof(Index));
        }


    }
}
