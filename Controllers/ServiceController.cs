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
			var projectContext = _context.Services.Skip((page - 1) * 9).Take(9);
			double totalPage = _context.Services.Count() / 9;
			if (_context.Services.Count() - totalPage * 9 > 0)
			{
				totalPage += 1;
			}
			ViewBag.TotalPage = totalPage;
			ViewBag.CurrentPage = page;
			return View(await projectContext.ToListAsync());
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
