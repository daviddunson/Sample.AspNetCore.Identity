// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Sample Company">
//   © 2023 Sample Company.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace IdentityServer
{
    using IdentityServer.Data;
    using IdentityServer.Services;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var config = builder.Configuration;
            var connectionString = config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => { options.SignIn.RequireConfirmedAccount = true; })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddAuthentication()
                .AddGoogle(options =>
                {
                    var google = config.GetSection("Authentication:Google");
                    options.ClientId = google["ClientId"];
                    options.ClientSecret = google["ClientSecret"];
                })
                .AddFacebook(options =>
                {
                    var facebook = config.GetSection("Authentication:Facebook");
                    options.ClientId = facebook["ClientId"];
                    options.ClientSecret = facebook["ClientSecret"];
                })
                .AddMicrosoftAccount(options =>
                {
                    var microsoft = config.GetSection("Authentication:Microsoft");
                    options.ClientId = microsoft["ClientId"];
                    options.ClientSecret = microsoft["ClientSecret"];
                })
                .AddTwitter(options =>
                {
                    var twitter = config.GetSection("Authentication:Twitter");
                    options.ConsumerKey = twitter["ConsumerKey"];
                    options.ConsumerSecret = twitter["ConsumerSecret"];
                    options.RetrieveUserDetails = true;
                });

            builder.Services.AddIdentityServer(options =>
                {
                    options.UserInteraction.LoginUrl = "/Identity/Account/Login";
                    options.UserInteraction.LogoutUrl = "/Identity/Account/Logout";
                })
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