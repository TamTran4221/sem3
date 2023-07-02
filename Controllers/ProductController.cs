using Microsoft.AspNetCore.Mvc;
using sem3.Data;
using sem3.Models;
using X.PagedList;

namespace sem3.Controllers;

public class ProductController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
         
        int limit = 8;
        IPagedList<Product> products = await _context.Products.OrderBy(x => x.Id).ToPagedListAsync(page, limit);
        return View(products);
    }
    
    public async Task<IActionResult> Detail(int id)
    {
        if (id == null || _context.Products == null)
        {
            return NotFound();
        }

        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }
}