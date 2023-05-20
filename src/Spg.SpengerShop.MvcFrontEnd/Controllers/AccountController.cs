using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Helpers;
using Spg.SpengerShop.Domain.Interfaces;
using System.Text.Json;

namespace Spg.SpengerShop.MvcFrontEnd.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet()]
        public IActionResult Login()
        {
            string userName = HttpContext.Request.Cookies["4bhif_login"] ?? "nicht angemeldet";

            return View("Login", new LoginDto() { UserName = "" });
        }

        [HttpPost()]
        public IActionResult Login(LoginDto dto)
        {
            (UserDto? userInformation, bool isAuthenticated) = _authService.CheckCredentials(dto.UserName, "geheim");
            if (isAuthenticated 
                && userInformation is not null)
            {
                userInformation.Signature = HashHelpers.
                    CalculateHash($"{userInformation.FullName}{userInformation.Role}{userInformation.EMail}", "gI976UUn3/m59A==");
                string token = JsonSerializer.Serialize(userInformation);

                HttpContext.Response.Cookies.Append("4bhif_login", token, new CookieOptions() 
                {
                    Expires = DateTime.Now.AddMinutes(3) 
                });

                return RedirectToAction("Index", "Home");
            }
            return View(dto);
        }

        [HttpPost()]
        public IActionResult Logout()
        {
            return View();
        }
    }
}
