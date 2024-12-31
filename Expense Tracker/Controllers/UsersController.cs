using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
