using HR_System.BLL.Common;
using HR_System.BLL.Helper;
using HR_System.BLL.Service.Abstraction;
using HR_System.BLL.Service.Implementation;
using HR_System.DAL.Common;
using HR_System.DAL.Database;
using HR_System.DAL.Entity;
using HR_System.DAL.Repo.Abstraction;
using HR_System.DAL.Repo.Implementation;
using HR_System.PL.Language;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace HR_System.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(SharedResource));

                    }
                );

            var conectionString = builder.Configuration.GetConnectionString("HR");

            builder.Services.AddDbContext<HrDbContext>(options =>
              options.UseSqlServer(
                  builder.Configuration.GetConnectionString("HR")
              ));



            //Dependancy Injection
            //Repo
            builder.Services.AddBussinesInDAL();
            //builder.Services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            //Services
            builder.Services.AddBussinesInBLL();
            //builder.Services.AddScoped<IEmployeeService, EmployeeService>();

           

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Customer/Account/AccessDenied"; // صفحة خاصة عند منع الوصول
                options.LoginPath = "/Identity/Account/Login"; // صفحة تسجيل الدخول
            });

            builder.Services .AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Allow spaces and some extra chars in username
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._ @+";

                // Password policy 
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                options.SignIn.RequireConfirmedEmail = true;
            })
     .AddRoles<IdentityRole>()
     .AddEntityFrameworkStores<HrDbContext>()
     .AddDefaultTokenProviders();


            var app = builder.Build();

            var supportedCultures = new[]
                {
                new CultureInfo("ar-EG"),
                new CultureInfo("en-US")
            };

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // ✅ مهم جدًا
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                RequestCultureProviders = new List<IRequestCultureProvider>
    {
        new CookieRequestCultureProvider()
    }
            });
            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
