using Microsoft.AspNetCore.Mvc.Filters;

namespace Spg.SpengerShop.Api.Filters
{
    public class HasRoleFilterAttribute : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
