using Microsoft.AspNetCore.Mvc;
using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.MvcFrontEnd.Filters;
using Spg.SpengerShop.MvcFrontEnd.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Spg.SpengerShop.MvcFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AuthorisationFilter(AllowedRole = "admin")]
        public IActionResult Privacy()
        {
            // Check Role, Permission from Databse NOT necessary
            string? json = HttpContext.Request.Cookies["4bhif_login"];
            return View("Privacy", json);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Unauthorized()
        {
            return View();
        }
    }
}