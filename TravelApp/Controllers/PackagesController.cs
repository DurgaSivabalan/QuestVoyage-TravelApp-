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
            //Validation (Step 3)
            if (string.IsNullOrEmpty(model.Name))
                return Content("Name is required");

            if (string.IsNullOrEmpty(model.Type))
                return Content("Type is required");

            // Image
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

        public IActionResult Index(int page = 1)
        {
            int pageSize = 7; // show 4 cards per page

            var totalItems = _context.Packages.Count();

            var data = _context.Packages
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return View(data);
        }
    }
}
