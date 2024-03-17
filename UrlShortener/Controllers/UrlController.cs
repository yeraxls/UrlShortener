using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using UrlShortener.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using UrlShortener.Services;
using Microsoft.AspNetCore.Authorization;

namespace UrlShortener.Controllers
{
    public class UrlController : Controller
    {
        private readonly IUrlService _urlService;

        public UrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateUrl()
        {
            var urlShort = new UrlVM();
            return View(urlShort);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> CreateUrl(UrlVM urlShort)
        {
            var result = await _urlService.AddUrl(urlShort);
            if (!result)
            {
                ModelState.AddModelError(String.Empty, "This name its alrready exist");
                return View(urlShort);
            }
            return RedirectToAction("GetAllUrls", "Url", new { id = urlShort.UserId });
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUrls(string id)
        {
            var result = await _urlService.GetAllUrls(id);
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUrl(int id, string idUsuario)
        {
            var urlDB = await _urlService.GetUrl(id);
            if (urlDB == null)
            {
                ModelState.AddModelError(String.Empty, "Url didnt found");
                return RedirectToAction("GetAllUrls", "Url", new { id = idUsuario });
            }
            await _urlService.Delete(urlDB);
            return RedirectToAction("GetAllUrls", "Url", new { id = idUsuario });
        }
        [HttpPost]
        public async Task<IActionResult> ChangeEnableUrl(int id, string idUsuario)
        {
            var urlDB = await _urlService.GetUrl(id);
            if (urlDB == null)
            {
                ModelState.AddModelError(String.Empty, "Url didnt found");
                return RedirectToAction("GetAllUrls", "Url", new { id = idUsuario });
            }

            await _urlService.UpdateEnabled(urlDB);
            return RedirectToAction("GetAllUrls", "Url", new { id = idUsuario });
        }
    }
}
