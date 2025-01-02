using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
namespace Expense_Tracker.Controllers
{

   
    public class AppRolesController : Controller
    {
      
        private readonly RoleManager<IdentityRole> _roleManager;
        public AppRolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        // List All the roles created by users
        [Authorize(Roles = "Admin,Sales")]
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            if (model.Name == null)
            {
                return View();
            }
            // avoid dublication
            if (!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult()) {
            _roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
            }
            return RedirectToAction("Index");
        }
    }
}
