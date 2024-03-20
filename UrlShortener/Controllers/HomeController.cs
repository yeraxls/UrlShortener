using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UrlShortener.Models;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUrlService _urlService;

        public HomeController(ILogger<HomeController> logger, IUrlService urlService)
        {
            _logger = logger;
            _urlService = urlService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/{userName}/{path}")]
        public async Task<IActionResult> RedirectUrl(string userName, string path)
        {
            var urlVM = await _urlService.GetUrlByName($"{userName}/{path}");
            if (urlVM == null)
                return View("Error");
            Response.Redirect(urlVM.Url);
            return View("Index");
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
