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

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => { options.SignIn.RequireConfirmedAccount = true; })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddAuthentication()
                .AddGoogle(options =>
                {
                    var google = builder.Configuration.GetSection("Authentication:Google");
                    options.ClientId = google["ClientId"];
                    options.ClientSecret = google["ClientSecret"];
                })
                .AddFacebook(options =>
                {
                    var facebook = builder.Configuration.GetSection("Authentication:Facebook");
                    options.ClientId = facebook["ClientId"];
                    options.ClientSecret = facebook["ClientSecret"];
                })
                .AddMicrosoftAccount(options =>
                {
                    var microsoft = builder.Configuration.GetSection("Authentication:Microsoft");
                    options.ClientId = microsoft["ClientId"];
                    options.ClientSecret = microsoft["ClientSecret"];
                })
                .AddTwitter(options =>
                {
                    var twitter = builder.Configuration.GetSection("Authentication:Twitter");
                    options.ConsumerKey = twitter["ConsumerKey"];
                    options.ConsumerSecret = twitter["ConsumerSecret"];
                    options.RetrieveUserDetails = true;
                });

            builder.Services.AddIdentityServer()
                .AddSigningCredential(builder.Configuration)
                .AddClientStore<ClientStore>()
                .AddResourceStore<ResourceStore>()
                .AddAspNetIdentity<IdentityUser>()
                .AddAppAuthRedirectUriValidator();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseIdentityServer();
            app.MapRazorPages();

            app.Run();
        }
    }
}