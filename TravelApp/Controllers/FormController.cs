using Microsoft.AspNetCore.Mvc;

namespace TravelApp.Controllers
{
    public class FormController : Controller
    {
        public IActionResult Form()
        {
            return View();
        }
    }
}
