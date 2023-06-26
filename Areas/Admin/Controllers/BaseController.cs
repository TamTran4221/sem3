using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace sem3.Areas.Admin.Controllers
{
	public class BaseController : Controller, IActionFilter
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			string loginString = HttpContext.Session.GetString("AdminLogin");
			if (loginString == null)
			{
				context.Result = new RedirectToRouteResult(
					new RouteValueDictionary(new { controller = "Account", action = "Login" })
				);
			}

			base.OnActionExecuting(context);
		}
	}
}
