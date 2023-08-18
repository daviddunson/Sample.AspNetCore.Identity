// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PersonalData.cshtml.cs" company="Sample Company">
//   © 2023 Sample Company.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WebApplication1.Areas.Identity.Pages.Account.Manage
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class PersonalDataModel : PageModel
    {
        private readonly ILogger<PersonalDataModel> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public PersonalDataModel(
            UserManager<IdentityUser> userManager,
            ILogger<PersonalDataModel> logger)
        {
            this._userManager = userManager;
            this._logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await this._userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this._userManager.GetUserId(this.User)}'.");
            }

            return this.Page();
        }
    }
}