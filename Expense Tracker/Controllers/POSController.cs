using Expense_Tracker.Models;
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
    }
}
