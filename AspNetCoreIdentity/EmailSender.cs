// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmailSender.cs" company="Sample Company">
//   © 2023 Sample Company.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WebApplication1
{
    using Microsoft.AspNetCore.Identity.UI.Services;

    public class EmailSender : IEmailSender
    {
        /// <summary>
        /// This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        /// directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}