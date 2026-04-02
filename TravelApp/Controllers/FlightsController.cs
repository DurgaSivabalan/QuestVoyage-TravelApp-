using Microsoft.AspNetCore.Mvc;

namespace TravelApp.Controllers
{
    public class FlightsController : Controller
    {
        public IActionResult Flights()
        {
            return View();
        }
    }
}
