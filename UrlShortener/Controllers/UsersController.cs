using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Context;
using UrlShortener.Models;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserRepositoryService _repository;

        public UsersController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, UrlShortenerDbContext context, IUserRepositoryService repository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
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
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction(nameof(Index), "Home");
        }

        [AllowAnonymous]
        //Manejador de errores
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
        [AllowAnonymous]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _repository.GetUsers();
            return View(result);
        }
    }
}
