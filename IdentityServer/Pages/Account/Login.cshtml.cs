// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Login.cshtml.cs" company="GSD Logic">
//   Copyright © 2006 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace IdentityServer.Pages.Account;

using System.Diagnostics.CodeAnalysis;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

/// <summary>
/// Contains interaction logic for <c>Login.cshtml</c>.
/// </summary>
[AllowAnonymous]
public class LoginModel : PageModel
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
    /// Initializes a new instance of the <see cref="LoginModel" /> class.
    /// </summary>
    /// <param name="interaction">The interaction service.</param>
    /// <param name="events">The event service.</param>
    public LoginModel(IIdentityServerInteractionService interaction, IEventService events)
    {
        this.interaction = interaction ?? throw new ArgumentNullException(nameof(interaction));
        this.events = events ?? throw new ArgumentNullException(nameof(events));
    }

    /// <summary>
    /// Gets or sets the return URL.
    /// </summary>
    [BindProperty(SupportsGet = true)]
    public string ReturnUrl { get; set; }

    /// <summary>
    /// Handles an HTTP POST request for the current page.
    /// </summary>
    /// <returns>A <see cref="Task" /> representing any asynchronous operation.</returns>
    public async Task<IActionResult> OnPostAsync()
    {
        var context = await this.interaction.GetAuthorizationContextAsync(this.ReturnUrl).ConfigureAwait(false);

        var user = new IdentityServerUser("A56270F6F5014A959674B2C2EE672FDC")
        {
            DisplayName = "Guest",
            IdentityProvider = "Sample",
        };

        var authenticationProperties = new AuthenticationProperties();

        await this.events.RaiseAsync(new UserLoginSuccessEvent(user.DisplayName, user.SubjectId, user.DisplayName, clientId: context?.Client?.ClientId)).ConfigureAwait(false);
        await this.HttpContext.SignInAsync(user, authenticationProperties).ConfigureAwait(false);

        if (string.IsNullOrEmpty(this.ReturnUrl))
        {
            this.ReturnUrl = "~/";
        }

        return this.Redirect(this.ReturnUrl);
    }
}