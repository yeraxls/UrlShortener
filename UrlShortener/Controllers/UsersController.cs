using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Context;
using UrlShortener.Models;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserRepositoryService _repository;

        public UsersController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, UrlShortenerDbContext context, IUserRepositoryService repository, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _repository = repository;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            //Para la creación de los roles
            if (!await _roleManager.RoleExistsAsync("Administrador"))
            {
                //Creación de rol usuario Administrador
                await _roleManager.CreateAsync(new IdentityRole("Administrador"));
            }

            //Para la creación de los roles
            if (!await _roleManager.RoleExistsAsync("Registrado"))
            {
                //Creación de rol usuario Registrado
                await _roleManager.CreateAsync(new IdentityRole("Registrado"));
            }

            RegisterVM registerVM = new RegisterVM();
            return View(registerVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
                return View(registerVM);
            var user = new AppUser
            {
                UserName = registerVM.Username,
                Email = registerVM.Email,
                Name = registerVM.Name,
                Lastname = registerVM.Lastname,
                CountryCode = registerVM.CountryCode,
                Phone = registerVM.Phone,
                Country = registerVM.Country,
                City = registerVM.City,
                DateOfBirth = registerVM.DateOfBirth
            };
            var result = await _userManager.CreateAsync(user, registerVM.Password);
            if (!result.Succeeded)
            {
                ValidateErrors(result);
                return View(registerVM);
            }
            await _userManager.AddToRoleAsync(user, "Registrado");
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction(nameof(Index), "Home");
        }

        [AllowAnonymous]
        private void ValidateErrors(IdentityResult resultado)
        {
            foreach (var error in resultado.Errors)
            {
                ModelState.AddModelError(String.Empty, error.Description);
            }
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string? returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM login, string? returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            returnurl = returnurl ?? Url.Content("~/");
            if (!ModelState.IsValid)
                return View(login);
            var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, login.RememberMe, lockoutOnFailure: true);

            if (result.IsLockedOut)
                return View("Bloqueado");

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Acceso inválido");
                return View(login);
            }



            return LocalRedirect(returnurl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _repository.GetUsers();
            return View(result);
        }

        [HttpGet]
        public IActionResult Denied()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateProfile(string id)
        {
            var user = await _repository.GetUserById(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(UserForTableVM user)
        {
            if (!ModelState.IsValid)
                return View(user);
            await _repository.UpdateUser(user);
            return RedirectToAction(nameof(Index), "Home");

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM changePassword, string id)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                    return RedirectToAction("Error");

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, token, changePassword.Password);
                if (result.Succeeded)
                    return RedirectToAction("ConfirmChangePassword");
                else
                    return View(changePassword);
            }
            return View(changePassword);
        }

        [HttpGet]
        public IActionResult ConfirmChangePassword()
        {
            return View();
        }
    }
}
