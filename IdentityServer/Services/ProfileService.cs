// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProfileService.cs" company="Sample Company">
//   © 2023 Sample Company.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace IdentityServer.Services;

using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;

/// <summary>
/// Provides profile information from the user store.
/// </summary>
public class ProfileService : IProfileService
{
    /// <summary>
    /// This method is called whenever claims about the user are requested (e.g. during token creation or via the userinfo endpoint)
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        if (context.RequestedClaimTypes.Contains(JwtClaimTypes.Name))
        {
            context.IssuedClaims.Add(new Claim(JwtClaimTypes.Name, "Guest"));
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// This method gets called whenever identity server needs to determine if the user is valid or active (e.g. if the user's account has been deactivated since they logged in).
    /// (e.g. during token issuance or validation).
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public Task IsActiveAsync(IsActiveContext context)
    {
        return Task.CompletedTask;
    }
}