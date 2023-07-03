using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using sem3.Data;
using sem3.Models;

namespace sem3.Controllers;

public class CheckoutController : Controller
{
    private readonly ApplicationDbContext _context;
    
    public CheckoutController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        User acc = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("AppLogin") ?? string.Empty);
        List<Cart> carts = _context.Carts.Where(a => a.UserId == acc.Id).Include(c => c.Product).ToList();
        ViewBag.acc = acc;
        ViewBag.carts = carts;
        Order order = new Order();
        order.Name = acc.Name;
        order.Email = acc.Email;
        return View(order);
    }
    
    [HttpPost]
    public IActionResult CheckOut(Order model, int[] productIds)
    {
        User acc = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("AppLogin") ?? string.Empty);

        model.UserId = acc.Id;
        _context.Orders.Add(model);
        _context.SaveChanges();
        foreach (int productId in productIds)
        {
            Cart cart = _context.Carts.FirstOrDefault(c => c.UserId == acc.Id && c.ProductId == productId);

            OrderDetail detail = new OrderDetail();
            detail.ProductId = productId;
            detail.OrderId = model.Id;
            if (cart != null)
            {
                detail.Quantity = cart.Quantity;
                detail.Price = cart.Price;
            }

            _context.OrderDetails.Add(detail);
            _context.SaveChanges();
        }
        TempData["yes"] = "Tạo đơn hàng thành công";
        return RedirectToAction("Index", "Cart");
    }
}