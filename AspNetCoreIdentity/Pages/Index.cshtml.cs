// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Index.cshtml.cs" company="Sample Company">
//   © 2023 Sample Company.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WebApplication1.Pages
{
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            this._logger = logger;
        }

        public void OnGet()
        {
        }
    }
}