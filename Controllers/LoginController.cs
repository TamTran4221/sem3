using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using sem3.Areas.Admin.Models;
using sem3.Data;
using sem3.Models;
using System.Security.Cryptography;
using System.Text;

namespace sem3.Controllers
{
	public class LoginController : Controller
	{
		private readonly ApplicationDbContext _context;
		public LoginController(ApplicationDbContext context)
		{
			_context = context;
		}
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Login(Login model)
		{
			if (ModelState.IsValid)
			{
				User acc = _context.Users.FirstOrDefault(a => a.Email.Equals(model.Email) && a.Password.Equals(GetMD5(model.Password)));

				if (acc != null)
				{
					HttpContext.Session.SetString("AppLogin", JsonConvert.SerializeObject(acc));
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("Email", "Tài khoản hoặc mật khẩu không chính xác");
				}
			}
			return View(model);
		}
		public IActionResult Register()
		{

			return View();
		}
		[HttpPost]
		public ActionResult Register(User user)
		{
			if (ModelState.IsValid)
			{
				var check = _context.Users.FirstOrDefault(a => a.Email == user.Email);
				if (check == null)
				{
					user.Password = GetMD5(user.Password);
					_context.ChangeTracker.LazyLoadingEnabled = false;
					_context.Users.Add(user);
					_context.SaveChanges();
					TempData["yes"] = "You are register account success!";
					return RedirectToAction("Login", "Login");
				}
				else
				{
					ViewBag.error = "Email already exists";
					return View();
				}


			}
			return View();

		}
		private string GetMD5(string password)
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] fromData = Encoding.UTF8.GetBytes(password);
			byte[] targetData = md5.ComputeHash(fromData);
			string byte2String = null;

			for (int i = 0; i < targetData.Length; i++)
			{
				byte2String += targetData[i].ToString("x2");

			}
			return byte2String;
		}
		public IActionResult Logout()
		{
			HttpContext.Session.Remove("AppLogin");
			return RedirectToAction("Login");
		}
	}
}