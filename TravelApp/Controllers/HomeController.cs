using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TravelApp.Models;

namespace TravelApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var email = HttpContext.Session.GetString("User");

            if (email == null)
                return RedirectToAction("Login", "Login");

            var bookings = _context.Bookings
                .Where(x => x.UserEmail == email)
                .OrderByDescending(x => x.Id)
                .Take(3)
                .ToList();

            ViewBag.UserEmail = email;
            ViewBag.Bookings = bookings;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
