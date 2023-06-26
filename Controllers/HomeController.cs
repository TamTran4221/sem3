using Microsoft.AspNetCore.Mvc;
using sem3.Models;
using System.Diagnostics;

namespace sem3.Controllers
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

		public IActionResult Privacy()
		{
			return View();
		}
		public IActionResult About()
		{
			return View();
		}
		public IActionResult Contact()
		{
			return View();
		}
		public IActionResult Blog()
		{
			return View();
		}
		public IActionResult Features()
		{
			return View();
		}
		public IActionResult Services()
		{
			return View();
		}
		public IActionResult FeaturesDetails()
		{
			return View();
		}
		public IActionResult Feedback()
		{
			return View();
		}
		public IActionResult Project()
		{
			return View();
		}
		public IActionResult Product()
		{
			return View();

		}
		public IActionResult ProductDetail()
		{
			return View();
		}
        public IActionResult Team()
        {
            return View();
        }
        public IActionResult TermsConditions()
        {
            return View();
        }
        public IActionResult FAQ()
        {
            return View();
        }
        public IActionResult Pricing()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}