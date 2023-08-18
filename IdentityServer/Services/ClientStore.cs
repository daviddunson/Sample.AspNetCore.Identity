// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClientStore.cs" company="Sample Company">
//   © 2023 Sample Company.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace IdentityServer.Services;

using IdentityServer4.Models;
using IdentityServer4.Stores;

/// <summary>
/// Provides a client store.
/// </summary>
public class ClientStore : IClientStore
{
    /// <summary>
    /// The collection of clients.
    /// </summary>
    private readonly IList<Client> clients;

    /// <summary>
    /// Initializes a new instance of the <see cref="ClientStore" /> class.
    /// </summary>
    /// <param name="configuration">The application configuration.</param>
    public ClientStore(IConfiguration configuration)
    {
        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(configuration));
        }

        this.clients = configuration.GetSection("Clients").Get<List<Client>>();
    }

    /// <summary>
    /// Gets a client with the specified client identifier.
    /// </summary>
    /// <param name="clientId">The client identifier.</param>
    /// <returns>A <see cref="Task" /> representing the asynchronous operation whose result contains the client with the specified client identifier.</returns>
    public Task<Client> FindClientByIdAsync(string clientId)
    {
        var client = this.clients.FirstOrDefault(c => c.ClientId == clientId);
        return Task.FromResult(client);
    }
}