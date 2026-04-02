using Microsoft.AspNetCore.Mvc;
using TravelApp.Models;

namespace TravelApp.Controllers
{
    public class SubscriberController : Controller
    {
        private readonly AppDbContext _context;

        public SubscriberController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Subscribe([FromBody] Subscriber model)
        {
            Console.WriteLine("Submit form hit hard.");

            if (model == null || string.IsNullOrWhiteSpace(model.Email))
            {
                return Json(new { success = false, message = "Email required" });
            }

            var exists = _context.Subscribers.Any(x => x.Email == model.Email);

            if (exists)
            {
                return Json(new { success = false, message = "Already subscribed" });
            }

            model.SubscribedOn = DateTime.Now;

            _context.Subscribers.Add(model);
            _context.SaveChanges();

            Console.WriteLine("Subscribe UI");
            return Json(new { success = true, message = "Subscribed successfully ✅" });
        }



        // ADMIN VIEW
        public IActionResult List()
        {
            var data = _context.Subscribers
                .OrderByDescending(x => x.SubscribedOn)
                .ToList();

            return View(data);
        }
    }
}
