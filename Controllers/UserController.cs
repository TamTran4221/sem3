using Microsoft.AspNetCore.Mvc;

namespace sem3.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
       public IActionResult Register()
        {
            return View();
        }

    }
}
