using Expense_Tracker.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker.Controllers
{

    [Authorize(Roles="Admin,Sales")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

     public    UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

       

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            ViewData["UserID"]=    _userManager.GetUserId(this.User);

            return View(users);
        }
    }
}
