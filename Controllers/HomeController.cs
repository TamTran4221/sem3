using Microsoft.AspNetCore.Mvc;
using sem3.Data;
using sem3.Models;
using System.Diagnostics;
using X.PagedList;

namespace sem3.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<LoginController> _logger;
		private readonly ApplicationDbContext _context;

		public HomeController(ILogger<LoginController> logger, ApplicationDbContext context)
		{
			_logger = logger;
            _context = context;
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
		// public IActionResult Contact()
		// {
		// 	return View();
		// }
		public async Task<IActionResult> Blog(int page = 1)
		{
			int limit = 6;
            var blogs = await _context.Blogs.OrderBy(x => x.Id).ToPagedListAsync(page,limit);

            return View(blogs);
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