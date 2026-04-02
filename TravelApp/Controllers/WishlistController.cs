using Microsoft.AspNetCore.Mvc;
using TravelApp.Models;

namespace TravelApp.Controllers
{
    public class WishlistController : Controller
    {
        private readonly AppDbContext _context;

        public WishlistController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Add(int id)
        {
            var email = HttpContext.Session.GetString("User");

            if (email == null)
                return Unauthorized();

            var exists = _context.Wishlists
                .Any(x => x.PackageId == id && x.UserEmail == email); // ✅ FIX

            if (exists)
                return BadRequest();

            _context.Wishlists.Add(new Wishlist
            {
                PackageId = id,
                UserEmail = email
            });

            _context.SaveChanges();

            return Ok();
        }

        public IActionResult Wishlist()
        {
            var email = HttpContext.Session.GetString("User");

            if (email == null)
                return RedirectToAction("Login", "Login");

            var packageIds = _context.Wishlists
                .Where(x => x.UserEmail == email)
                .Select(x => x.PackageId) // ✅ IMPORTANT FIX
                .ToList();

            var packages = _context.Packages
                .Where(p => packageIds.Contains(p.Id))
                .ToList();

            return View(packages);
        }

        public IActionResult Remove(int id)
        {
            var email = HttpContext.Session.GetString("User");

            var item = _context.Wishlists
                .FirstOrDefault(x => x.PackageId == id && x.UserEmail == email);
            if (item != null)
            {
                _context.Wishlists.Remove(item);
                _context.SaveChanges();
            }

            return RedirectToAction("Wishlist");
        }
    }
}