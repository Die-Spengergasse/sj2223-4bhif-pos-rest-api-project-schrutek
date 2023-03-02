using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.Filter
{
    public class HasRoleAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string role = context.HttpContext.Request.Headers["role"].ToString() ?? string.Empty;
            if (role != "admin")
            {
                context.Result = new UnauthorizedObjectResult("Zutritt verboten!");
            }
        }

        public virtual void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
