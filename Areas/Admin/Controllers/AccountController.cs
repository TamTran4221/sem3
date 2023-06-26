using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using sem3.Areas.Admin.Models;
using sem3.Data;
using sem3.Models;
using System;
using System.Security.Principal;

namespace sem3.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AccountController : Controller
	{
		private readonly ApplicationDbContext _context;
		public AccountController(ApplicationDbContext context)
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
				User acc = _context.Users.FirstOrDefault(a => a.Email.Equals(model.Email) && a.Password.Equals(model.Password));

				if (acc != null)
				{
					HttpContext.Session.SetString("AdminLogin", JsonConvert.SerializeObject(acc));
					return RedirectToAction("Index", "Dashboard");
				}
				else
				{
					ModelState.AddModelError("Email", "Tài khoản hoặc mật khẩu không chính xác");
				}
			}
			return View(model);

		}
	}
}
