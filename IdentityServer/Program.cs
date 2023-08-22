// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Sample Company">
//   © 2023 Sample Company.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace IdentityServer
{
    using System.Reflection;
    using IdentityServer.Data;
    using IdentityServer.Services;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

            builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => { options.SignIn.RequireConfirmedAccount = true; })
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

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
                .AddAppAuthRedirectUriValidator()
                .AddSigningCredential(builder.Configuration)
                .AddAspNetIdentity<IdentityUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(
                        builder.Configuration.GetConnectionString("ConfigurationConnection"),
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(
                        builder.Configuration.GetConnectionString("OperationConnection"),
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                    options.EnableTokenCleanup = true;
                });

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