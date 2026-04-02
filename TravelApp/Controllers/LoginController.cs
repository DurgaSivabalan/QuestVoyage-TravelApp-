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
        // LOGIN PAGE
        public IActionResult Login()
        {
            return View();
        }
        // LOGIN POST
        [HttpPost]
        public IActionResult Login(User model)
        {
            // 🔴 ADMIN LOGIN
            if (model.Email == "admin@questvoyage.com" && model.Password == "Admin@123")
            {
                HttpContext.Session.SetString("Admin", "true");
                Console.WriteLine("Admin Session Set");
                return RedirectToAction("Admin", "Admin");
            }

            // 🟢 USER LOGIN
            var user = _context.Users
                .FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);
            if (user != null)
            {
                HttpContext.Session.SetString("User", user.Email);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid Email or Password ❌";
            return View();
        }
        // REGISTER PAGE
        public IActionResult Register()
        {
            return View();
        }
        // REGISTER POST
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