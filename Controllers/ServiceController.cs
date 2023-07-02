using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sem3.Data;
using X.PagedList;

namespace sem3.Controllers
{
	public class ServiceController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ServiceController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Admin/Service
		public async Task<IActionResult> Index(int page = 1)
		{
			int limit = 8;
            IPagedList<Service> ListServices = await _context.Services.OrderBy(x => x.Id).ToPagedListAsync(page, limit);
            return View(ListServices);
		}

		// GET: SeviecController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: SeviecController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: SeviecController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: SeviecController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: SeviecController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: SeviecController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: SeviecController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
