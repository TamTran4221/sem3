﻿using Microsoft.AspNetCore.Mvc;
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

		// GET: Admin/Blog/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Blogs == null)
			{
				return NotFound();
			}

			var Blog = await _context.Blogs
				.FirstOrDefaultAsync(m => m.Id == id);
			if (Blog == null)
			{
				return NotFound();
			} 

			return View(Blog);
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
		public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,SalePrice,Status,Image")] Blog Blog, IFormFile imageFile)
		{
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

				_context.Add(Blog);
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
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,SalePrice,Status,Image")] Blog Blog, IFormFile imageFile)
		{
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
				_context.Add(Blog);
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}
			return View(Blog);
		}

		// GET: Admin/Blog/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Blogs == null)
			{
				return NotFound();
			}

			var Blog = await _context.Blogs
				.FirstOrDefaultAsync(m => m.Id == id);
			if (Blog == null)
			{
				return NotFound();
			}

			return View(Blog);
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
