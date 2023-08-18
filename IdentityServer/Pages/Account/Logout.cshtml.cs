// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Logout.cshtml.cs" company="Sample Company">
//   © 2023 Sample Company.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace IdentityServer.Pages.Account;

using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

/// <summary>
/// Contains interaction logic for <c>Login.cshtml</c>.
/// </summary>
[AllowAnonymous]
public class LogoutModel : PageModel
{
    /// <summary>
    /// The event service.
    /// </summary>
    private readonly IEventService events;

    /// <summary>
    /// The interaction service.
    /// </summary>
    private readonly IIdentityServerInteractionService interaction;

    /// <summary>
    /// Initializes a new instance of the <see cref="LogoutModel" /> class.
    /// </summary>
    /// <param name="events">The event service.</param>
    /// <param name="interaction">The interaction service.</param>
    public LogoutModel(IEventService events, IIdentityServerInteractionService interaction)
    {
        this.events = events ?? throw new ArgumentNullException(nameof(events));
        this.interaction = interaction ?? throw new ArgumentNullException(nameof(interaction));
    }

    /// <summary>
    /// Gets or sets the return URL.
    /// </summary>
    [BindProperty(SupportsGet = true)]
    public string ReturnUrl { get; set; }

    /// <summary>
    /// Occurs during an HTTP GET request.
    /// </summary>
    /// <param name="logoutId">The logout identifier.</param>
    /// <returns>A <see cref="Task" /> that represents the asynchronous operation.</returns>
    public async Task<IActionResult> OnGetAsync(string logoutId)
    {
        if (this.User.IsAuthenticated())
        {
            await this.HttpContext.SignOutAsync().ConfigureAwait(false);
            await this.events.RaiseAsync(new UserLogoutSuccessEvent(this.User.GetSubjectId(), this.User.GetDisplayName())).ConfigureAwait(false);

            var provider = this.User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value ?? string.Empty;

            if (await this.HttpContext.GetSchemeSupportsSignOutAsync(provider).ConfigureAwait(false))
            {
                return this.SignOut(new AuthenticationProperties { RedirectUri = this.ReturnUrl }, provider);
            }
        }

        var logout = await this.interaction.GetLogoutContextAsync(logoutId).ConfigureAwait(false);

        if (logout?.PostLogoutRedirectUri != null)
        {
            return this.Redirect(logout.PostLogoutRedirectUri);
        }

        if (!string.IsNullOrEmpty(this.ReturnUrl))
        {
            return this.Redirect(this.ReturnUrl);
        }

        return this.Page();
    }
}