// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourceStore.cs" company="Sample Company">
//   © 2023 Sample Company.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace IdentityServer.Services;

using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Stores;

/// <summary>
/// Provides a resource store.
/// </summary>
public class ResourceStore : IResourceStore
{
    /// <summary>
    /// The collection of clients.
    /// </summary>
    private readonly IList<ApiResource> apiResources;

    /// <summary>
    /// The collection of clients.
    /// </summary>
    private readonly IList<ApiScope> apiScopes;

    /// <summary>
    /// The collection of clients.
    /// </summary>
    private readonly IList<IdentityResource> identityResources;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceStore" /> class.
    /// </summary>
    /// <param name="configuration">The application configuration.</param>
    public ResourceStore(IConfiguration configuration)
    {
        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        this.apiScopes = configuration.GetSection("ApiScopes").Get<List<ApiScope>>() ?? new List<ApiScope>();
        this.apiResources = configuration.GetSection("ApiResources").Get<List<ApiResource>>() ?? new List<ApiResource>();
        this.identityResources = configuration.GetSection("IdentityResources").Get<List<IdentityResource>>() ?? new List<IdentityResource>();

        if (this.identityResources.All(r => r.Name != IdentityServerConstants.StandardScopes.OpenId))
        {
            this.identityResources.Add(new IdentityResources.OpenId());
        }

        if (this.identityResources.All(r => r.Name != IdentityServerConstants.StandardScopes.Profile))
        {
            this.identityResources.Add(new IdentityResources.Profile());
        }
    }

    /// <summary>
    /// Gets API resources by API resource name.
    /// </summary>
    /// <param name="apiResourceNames">The names of the resources to return.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
    {
        var resources = this.apiResources.Where(r => apiResourceNames.Contains(r.Name));
        return Task.FromResult(resources);
    }

    /// <summary>
    /// Gets API resources by scope name.
    /// </summary>
    /// <param name="scopeNames">The names of the scopes to return.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
    {
        var resources = this.apiResources.Where(r => r.Scopes.Any(scopeNames.Contains));
        return Task.FromResult(resources);
    }

    /// <summary>
    /// Gets API scopes by scope name.
    /// </summary>
    /// <param name="scopeNames">The names of the scopes to return.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
    {
        var scopes = this.apiScopes.Where(s => scopeNames.Contains(s.Name));
        return Task.FromResult(scopes);
    }

    /// <summary>
    /// Gets identity resources by scope name.
    /// </summary>
    /// <param name="scopeNames">The names of the scopes to return.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
    {
        var resources = this.identityResources.Where(s => scopeNames.Contains(s.Name));
        return Task.FromResult(resources);
    }

    /// <summary>
    /// Gets all resources.
    /// </summary>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
    public Task<Resources> GetAllResourcesAsync()
    {
        var resources = new Resources
        {
            ApiResources = this.apiResources,
            ApiScopes = this.apiScopes,
            IdentityResources = this.identityResources,
        };

        return Task.FromResult(resources);
    }
}