// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Privacy.cshtml.cs" company="Sample Company">
//   © 2023 Sample Company.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WebApplication1.Pages
{
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            this._logger = logger;
        }

        public void OnGet()
        {
        }
    }
}