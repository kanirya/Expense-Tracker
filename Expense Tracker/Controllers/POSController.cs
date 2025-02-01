using Expense_Tracker.Models;
using Expense_Tracker.Models.DTOs;
using Expense_Tracker.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.Controllers
{
    public class POSController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Single constructor accepting ApplicationDbContext
        public POSController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PointOfSale()
        {
            var products = await _context.Product.ToListAsync();
            return View(products);
        }

        [HttpPost]
        public IActionResult PointOfSale([FromBody] List<Product> cartItems)
        {
            if (cartItems == null || cartItems.Count == 0)
            {
                return BadRequest("Cart is empty.");
            }

            // Save cart data to session
            HttpContext.Session.SetObjectAsJson("Cart", cartItems);

            return Ok();
        }

        public IActionResult OrderSummary()
        {
            // Retrieve cart data from session
            var cart = HttpContext.Session.GetObjectFromJson<List<ProductVM>>("Cart");

            if (cart == null)
            {
                return RedirectToAction("Index");
            }

            return View(cart);
        }


    }
}
