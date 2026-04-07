using Microsoft.AspNetCore.Mvc;
using TravelApp.Models;
using System.Linq;

namespace TravelApp.Controllers
{
public class LoginController : Controller
    {
        private readonly AppDbContext _context;
        public LoginController(AppDbContext context)
        {
            _context = context;
        }
        // login page
        public IActionResult Login()
        {
            return View();
        }
        //login post
        [HttpPost]
        public IActionResult Login(User model)
        {
            //admin login
            if (model.Email == "admin@questvoyage.com" && model.Password == "Admin@123")
            {
                HttpContext.Session.SetString("Admin", "true");
                Console.WriteLine("Admin Session Set");
                return RedirectToAction("Admin", "Admin");
            }
            //user login
            var user = _context.Users
        .FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password"); // ✅ THIS IS KEY
                return View(model); // return same page
            }

            HttpContext.Session.SetString("User", user.Email);

            return RedirectToAction("Index", "Home");
        }
        // register page
        public IActionResult Register()
        {
            return View();
        }
        // register post
        [HttpPost]
        public IActionResult Register(User model, string confirmPassword)
        {
            if (model.Password != confirmPassword)
            {
                ViewBag.Error = "Passwords do not match ❌";
                return View();
            }

            var exists = _context.Users.Any(x => x.Email == model.Email);

            if (exists) 
            {
                ViewBag.Error = "User already exists ❌";
                return View();
            }

            _context.Users.Add(model);
            _context.SaveChanges();

            ViewBag.Success = "Registered Successfully ✅";
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login");
        }
    }
}