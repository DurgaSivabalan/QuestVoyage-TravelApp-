using Microsoft.AspNetCore.Mvc;
using TravelApp.Models;
using System.Linq;

namespace TravelApp.Controllers
{
    public class PackagesController : Controller
    {
        private readonly AppDbContext _context;

        public PackagesController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Package model, IFormFile imageFile)
        {
            // 🔥 VALIDATION (STEP 3)
            if (string.IsNullOrEmpty(model.Name))
                return Content("Name is required");

            if (string.IsNullOrEmpty(model.Type))
                return Content("Type is required");

            // IMAGE
            if (imageFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    imageFile.CopyTo(ms);
                    model.ImageData = ms.ToArray();
                }
            }

            _context.Packages.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var package = _context.Packages.Find(id);

            if (package == null)
            {
                return Content("NOT FOUND: " + id);
            }

            _context.Packages.Remove(package);
            _context.SaveChanges();

            return Content("DELETED SUCCESS: " + id);
        }
        public IActionResult Index()
        {
            var data = _context.Packages.ToList();
            return View(data);
        }
    }
}
