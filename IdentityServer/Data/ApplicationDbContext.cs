// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationDbContext.cs" company="Sample Company">
//   © 2023 Sample Company.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace IdentityServer.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationIdentityDbContext : IdentityDbContext
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
            : base(options)
        {
        }
    }
}