using Microsoft.AspNetCore.Mvc;

namespace TravelApp.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult About()
        {
            return View();
        }
    }
}
