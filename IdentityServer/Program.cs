// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Sample Company">
//   © 2023 Sample Company.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace IdentityServer
{
    using IdentityServer.Services;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddIdentityServer()
                .AddSigningCredential(builder.Configuration)
                .AddClientStore<ClientStore>()
                .AddResourceStore<ResourceStore>()
                .AddProfileService<ProfileService>()
                .AddAppAuthRedirectUriValidator();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseIdentityServer();

            app.MapRazorPages();
            app.MapGet("/", () => "Identity Server");

            app.Run();
        }
    }
}