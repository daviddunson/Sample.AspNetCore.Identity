// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfirmEmail.cshtml.cs" company="Sample Company">
//   © 2023 Sample Company.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#nullable disable

namespace WebApplication1.Areas.Identity.Pages.Account
{
    using System.Text;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;

    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ConfirmEmailModel(UserManager<IdentityUser> userManager)
        {
            this._userManager = userManager;
        }

        /// <summary>
        /// This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        /// directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if ((userId == null) || (code == null))
            {
                return this.RedirectToPage("/Index");
            }

            var user = await this._userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await this._userManager.ConfirmEmailAsync(user, code);
            this.StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
            return this.Page();
        }
    }
}