// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="United States Government">
//   © 2023 United States Government, as represented by the Secretary of the Army.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WebApplication1
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.EntityFrameworkCore;
    using WebApplication1.Data;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;
            var connectionString = config.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            }

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

            builder.Services.AddRazorPages();
            builder.Services.AddTransient<IEmailSender, EmailSender>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapRazorPages();
            app.Run();
        }
    }
}