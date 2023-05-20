using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Helpers;

namespace Spg.SpengerShop.MvcFrontEnd.Filters
{
    public class AuthorisationFilterAttribute : ActionFilterAttribute, IActionFilter
    {
        public string AllowedRole { get; set; }

        public void OnActionExecuted(ActionExecutedContext context)
        { }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string? token = context.HttpContext.Request.Cookies["4bhif_login"];
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new RedirectToActionResult("Unauthorized", "Home", null);
                return;
            }

            UserDto? userInformation = JsonSerializer.Deserialize<UserDto>(token);
            if (userInformation is null)
            {
                context.Result = new RedirectToActionResult("Unauthorized", "Home", null);
                return;
            }
            string hash = HashHelpers.CalculateHash($"{userInformation.FullName}{userInformation.Role}{userInformation.EMail}", "gI976UUn3/m59A==");
            if (hash != userInformation.Signature)
            {
                context.Result = new RedirectToActionResult("Unauthorized", "Home", null);
                return;
            }
            if (userInformation.Role.ToLower() != AllowedRole.ToLower()) // guest, admin
            {
                context.Result = new RedirectToActionResult("Unauthorized", "Home", null);
                return;
            }
        }
    }
}