using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sem3.Data;
using sem3.Models;

namespace sem3.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Service
        public async Task<IActionResult> Index()
        {
              return View(await _context.Services.ToListAsync());
        }

        // GET: Admin/Service/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Admin/Service/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Service/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Image,Price")] Service service, IFormFile imageFile)
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
						service.Image = fileName;
					}
				}

				_context.Add(service);
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}

			return RedirectToAction(nameof(Index));
		}

        // GET: Admin/Service/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        // POST: Admin/Service/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Image,Price")] Service service, IFormFile imageFile)
        {
            if (id != service.Id)
            {
                return NotFound();
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
						service.Image = fileName;
					}
				}
				_context.Update(service);
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}
			return View(service);
		}

        // GET: Admin/Service/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Admin/Service/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Services == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Services'  is null.");
            }
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
          return _context.Services.Any(e => e.Id == id);
        }
    }
}
