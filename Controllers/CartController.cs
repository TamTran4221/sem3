using Microsoft.AspNetCore.Mvc;

namespace sem3.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
