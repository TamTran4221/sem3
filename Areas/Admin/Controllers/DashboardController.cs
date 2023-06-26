using Microsoft.AspNetCore.Mvc;

namespace sem3.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
