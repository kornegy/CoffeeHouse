using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoffeeHouse.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdminAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasCookie = context.HttpContext.Request.Cookies.ContainsKey("AdminAuth");
            if (!hasCookie)
            {
                // redirect to login
                context.Result = new RedirectToActionResult("Login", "Auth", null);
            }
        }
    }
}
