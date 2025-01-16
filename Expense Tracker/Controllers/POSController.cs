using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker.Controllers
{
    public class POSController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PointOfSale()
        {
            return View();
        }


    }
}
