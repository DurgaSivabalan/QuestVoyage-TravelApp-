using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TravelApp.Models;

public class TestimonialController : Controller
{
    private readonly AppDbContext _context;

    public TestimonialController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Testimonial()
    {
        var reviews = _context.Reviews.ToList();
        Console.WriteLine("Count: " + reviews.Count);
      
        return View(reviews);
    }
    [HttpPost]
    public IActionResult MarkHelpful(int id)
    {
        var review = _context.Reviews.FirstOrDefault(r => r.Id == id);
        if (review == null) return NotFound();

        review.HelpfulCount++;
        _context.SaveChanges();

        return RedirectToAction("Testimonial");
    }
    [HttpPost]
    public IActionResult AddReview(Review review)
    {
        if (!ModelState.IsValid)
        {
            var reviews = _context.Reviews
                .OrderByDescending(r => r.Id)
                .ToList();

            return View("Testimonial", reviews);
        }

        _context.Reviews.Add(review);

        var result = _context.SaveChanges();

        Console.WriteLine("Saved rows: " + result);
        return RedirectToAction("Testimonial");
    }

}