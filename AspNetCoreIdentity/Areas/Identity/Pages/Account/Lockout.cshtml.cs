// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Lockout.cshtml.cs" company="Sample Company">
//   © 2023 Sample Company.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#nullable disable

namespace WebApplication1.Areas.Identity.Pages.Account
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    /// <summary>
    /// This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    /// directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [AllowAnonymous]
    public class LockoutModel : PageModel
    {
        /// <summary>
        /// This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        /// directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public void OnGet()
        {
        }
    }
}