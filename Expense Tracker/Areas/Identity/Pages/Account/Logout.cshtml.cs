// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.Threading.Tasks;
using Expense_Tracker.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Expense_Tracker.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            // Sign out the user
            await _signInManager.SignOutAsync();

            // Clear cookies explicitly
            foreach (var cookie in HttpContext.Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            // Log the logout event
            _logger.LogInformation("User logged out.");

            // Add cache control headers to prevent caching issues
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

            // Redirect user after logout
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // Redirect to the login page explicitly
                return RedirectToPage("/Account/Login");
            }
        }
    }
}
