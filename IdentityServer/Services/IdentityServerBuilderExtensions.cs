// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IdentityServerBuilderExtensions.cs" company="Sample Company">
//   © 2023 Sample Company.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace IdentityServer.Services;

using System.Security.Cryptography;
using IdentityServer4;
using Microsoft.IdentityModel.Tokens;

/// <summary>
/// Provides extension methods for the <see cref="IIdentityServerBuilder" /> interface.
/// </summary>
public static class IdentityServerBuilderExtensions
{
    /// <summary>
    /// Sets the signing credential.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static IIdentityServerBuilder AddSigningCredential(this IIdentityServerBuilder builder, IConfiguration configuration)
    {
        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        using var rsa = RSA.Create(2048);

        var signingKey = configuration["SigningKey"];

        if (signingKey != null)
        {
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(signingKey), out _);
        }

        var parameters = rsa.ExportParameters(true);
        var securityKey = new RsaSecurityKey(parameters);
        builder.AddSigningCredential(securityKey, IdentityServerConstants.RsaSigningAlgorithm.RS256);

        return builder;
    }
}