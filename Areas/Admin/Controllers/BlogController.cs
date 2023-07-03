using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sem3.Data;
using sem3.Models;
using X.PagedList;

namespace sem3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public BlogController(ApplicationDbContext context)
        {
            _context = context;
        }

		// GET: Admin/Blog
		public async Task<IActionResult> Index(int page = 1)
		{
			int limit = 10;

			var Blogs = await _context.Blogs.OrderBy(c => c.Id).ToPagedListAsync(page, limit);

			return View(Blogs);
		}
		
		// GET: Admin/Blog/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Admin/Blog/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Title,Description,SubDescription,Image")] Blog Blog, IFormFile imageFile)
		{
			if (Blog.Description == null)
			{
				ModelState.AddModelError("Description", "Description not null");
			}
			if (Blog.Title == null)
			{
				ModelState.AddModelError("Description", "Title not null");
			}
			if (ModelState.IsValid)
			{
				if (imageFile != null && imageFile.Length > 0)
				{
					// Lưu ảnh vào thư mục "wwwroot/uploads"
					var fileName = Path.GetFileName(imageFile.FileName);
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await imageFile.CopyToAsync(stream);
						Blog.Image = fileName;
					}
				}

				await _context.AddAsync(Blog);
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}

			return View(Blog);
		}


		// GET: Admin/Blog/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Blogs == null)
			{
				return NotFound();
			}

			var Blog = await _context.Blogs.FindAsync(id);
			if (Blog == null)
			{
				return NotFound();
			}
			return View(Blog);
		}

		// POST: Admin/Blog/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Image,Description,SubDescription,Image")] Blog Blog)
		{
			if (id != Blog.Id)
			{
				return NotFound();
			}
			var files = HttpContext.Request.Form.Files;
			if (ModelState.IsValid)
			{
				try
				{
					if (files.Any() && files[0].Length > 0)
					{
						var file = files[0];
						var fileName = file.FileName;
						var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads", fileName);

						using (var stream = new FileStream(path, FileMode.Create))
						{
							file.CopyTo(stream);
							Blog.Image = fileName;
						}
					} 
					_context.Update(Blog);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!BlogExists(Blog.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(Blog);
		}

		// GET: Admin/Blog/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (_context.Blogs == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Blogs'  is null.");
			}
			var Blog = await _context.Blogs.FindAsync(id);
			if (Blog != null)
			{
				_context.Blogs.Remove(Blog);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		// POST: Admin/Blog/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Blogs == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Blogs'  is null.");
			}
			var Blog = await _context.Blogs.FindAsync(id);
			if (Blog != null)
			{
				_context.Blogs.Remove(Blog);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool BlogExists(int id)
		{
			return _context.Blogs.Any(e => e.Id == id);
		}
	}
}
