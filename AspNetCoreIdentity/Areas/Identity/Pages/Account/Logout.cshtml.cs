// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Logout.cshtml.cs" company="Sample Company">
//   © 2023 Sample Company.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#nullable disable

namespace WebApplication1.Areas.Identity.Pages.Account
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class LogoutModel : PageModel
    {
        private readonly ILogger<LogoutModel> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;

        public LogoutModel(SignInManager<IdentityUser> signInManager, ILogger<LogoutModel> logger)
        {
            this._signInManager = signInManager;
            this._logger = logger;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await this._signInManager.SignOutAsync();
            this._logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return this.LocalRedirect(returnUrl);
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
                return this.RedirectToPage();
            }
        }
    }
}