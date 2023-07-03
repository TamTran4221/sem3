using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using sem3.Data;
using sem3.Models;
using sem3.Response;

namespace sem3.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            User acc = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("AppLogin") ?? string.Empty);
            List<Cart> carts = _context.Carts.Where(a => a.UserId == acc.Id).Include(c => c.Product).ToList();
            return View(carts);
        }

        public IActionResult Add(int id)
        {
            User acc = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("AppLogin") ?? string.Empty);
            Cart _cart = _context.Carts.Where(x => x.UserId == acc.Id && x.ProductId == id).FirstOrDefault();

            if (_cart != null)
            {
                _cart.Quantity += 1;
                _context.Carts.Update(_cart);
                _context.SaveChanges();
            }
            else
            {
                Product product = _context.Products.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    Cart cart = new Cart() { UserId = acc.Id, Price = product.Price, ProductId = product.Id, Quantity = 1 , TotalPrice = product.Price};
                    _context.Carts.Add(cart);
                }

                _context.SaveChanges();
            }
            TempData["yes"] = "Thêm sản phẩm vào giỏ hàng thành công";
            return RedirectToAction("Index", "Cart");
        }
        
        [HttpPost]
        public IActionResult AddMutiple(CartRequest request)
        {
            User acc = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("AppLogin") ?? string.Empty);
            Cart _cart = _context.Carts.Where(x => x.UserId == acc.Id && x.ProductId == request.ProductId).FirstOrDefault();

            if (_cart != null)
            {
                _cart.Quantity += request.quantity;
                _cart.TotalPrice = _cart.Quantity * _cart.Price;
                _context.Carts.Update(_cart);
                _context.SaveChanges();
            }
            else
            {
                Product product = _context.Products.FirstOrDefault(p => p.Id == request.ProductId);
                if (product != null)
                {
                    Cart cart = new Cart() { UserId = acc.Id, Price = product.Price, ProductId = product.Id, Quantity = request.quantity , TotalPrice = product.Price};
                    _context.Carts.Add(cart);
                }

                _context.SaveChanges();
            }
            TempData["yes"] = "Thêm sản phẩm vào giỏ hàng thành công";
            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Delete(int id)
        {
            User acc = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("AppLogin") ?? string.Empty);
            Cart _cart = _context.Carts.Where(c => c.UserId == acc.Id && c.ProductId == id).FirstOrDefault();

            if (_cart != null)
            {
                _context.Carts.Remove(_cart);
                _context.SaveChanges();
            }
            TempData["yes"] = "Xóa giỏ hàng thành công";
            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Update(int proid, int quantity)
        {
            User acc = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("AppLogin") ?? string.Empty);
            Cart _cart = _context.Carts.FirstOrDefault(c => c.UserId == acc.Id && c.ProductId == proid);

            if (_cart != null)
            {
                quantity = quantity > 0 ? quantity : 1;
                _cart.Quantity = quantity;
                _context.Carts.Update(_cart);
                _context.SaveChanges();
            }
            TempData["yes"] = "Cập nhật giỏ hàng thành công";
            return RedirectToAction("Index", "Cart");
        }
        
        public IActionResult Order()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Order(int[] id)
        {
            User acc = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("AppLogin") ?? string.Empty);
            List<Cart> carts = _context.Carts.Where(c => id.Contains(c.ProductId) && c.UserId == acc.Id).Include(c => c.Product).ToList();
            ViewBag.acc = acc;
            ViewBag.carts = carts;
            Order order = new Order()
            {
                Name = acc.Name,
                Email = acc.Email,
            };
           
            return View(order);
        }

        [HttpPost]
        public IActionResult CheckOut(Order model, int[] productIds)
        {
            User acc = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("AppLogin") ?? string.Empty);

            model.UserId = acc.Id;
            _context.Orders.Add(model);
            _context.SaveChanges();
            foreach (int ProductId in productIds)
            {
                Cart _cart = _context.Carts.FirstOrDefault(c => c.UserId == acc.Id && c.ProductId == ProductId);

                OrderDetail detail = new OrderDetail();
                detail.ProductId = ProductId;
                detail.OrderId = model.Id;
                detail.Quantity = _cart.Quantity;
                detail.Price = _cart.Price;
                _context.OrdersDetails.Add(detail);
                _context.SaveChanges();
            }
            TempData["yes"] = "Tạo đơn hàng thành công";
            return RedirectToAction("Index", "Cart");
        }
    }
}
