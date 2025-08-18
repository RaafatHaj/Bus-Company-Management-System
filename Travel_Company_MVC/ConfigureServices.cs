using CloudinaryDotNet;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Routing;
using Travel_Company_MVC.HangfirTasks;
using Travel_Company_MVC.Helper;
using Travel_Company_MVC.Services.Email;
using Travel_Company_MVC.Services.Images;
using Travel_Company_MVC.Settings;
using TravelCompany.Infrastructure.Persistence;
using TravelCompany.Infrastructure.Persistence.Entities;
using UoN.ExpressiveAnnotations.NetCore.DependencyInjection;

namespace Travel_Company_MVC
{
    public static class ConfigureServices
	{

		public static IServiceCollection RegisterWebServices(this IServiceCollection services, WebApplicationBuilder builder)
		{

			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

			services.AddHangfire(x => x.UseSqlServerStorage(connectionString));
			services.AddHangfireServer();
			services.AddScoped<HangfireTasks>();

            services.AddScoped<IImageService, ImageService>();


            services.AddExpressiveAnnotations();

			// Identity whti Configurations ....

            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddSignInManager<SignInManager<ApplicationUser>>();

            // By Default UnAuthorized User will be redirected to log in page if you scafoled identity Views and use it 
            // However you can redirected page by this configureations 

      
            //  builder.Services.ConfigureApplicationCookie(options =>
            //  {
            //      options.LoginPath = "/Account/Login";
            //      options.AccessDeniedPath = "/Account/AccessDenied";
            //  });
            services.Configure<IdentityOptions>(options =>
			{
                // Default Lockout settings.

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 2;
                options.Lockout.AllowedForNewUsers = true;


            });

            // Register Cloudinary ...

            var cloudinarySettings = builder.Configuration.GetSection(nameof(CloudinarySettings)).Get<CloudinarySettings>();

            var account = new Account()
			{
				Cloud = cloudinarySettings!.Cloud,
				ApiKey = cloudinarySettings.ApiKey,
				ApiSecret = cloudinarySettings.ApiSecret
			};

			var cloudinary = new Cloudinary(account);

			services.AddSingleton(cloudinary);


			// Register My Custom class instead of the default one so that i add my custom class , its implementation in Helper Folder .... 

			services.AddScoped< IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory >();

            // Configure Security Stamp so that the logined user logout when certain updates occures ....

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromMinutes(0); // check every certain time 
            });


            // Mail Configurations ...

            services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));
            services.AddHttpContextAccessor();
            services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IEmailBuilder, EmailBuilder>();
            services.AddScoped<IEmailService, EmailService>();
            // Cookies Configurations ....

            //builder.Services.ConfigureApplicationCookie(options =>
            //{
            //    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            //    options.Cookie.Name = "YourAppCookieName";
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            //    options.LoginPath = "/Identity/Account/Login";
            //    // ReturnUrlParameter requires 
            //    //using Microsoft.AspNetCore.Authentication.Cookies;
            //    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            //    options.SlidingExpiration = true;
            //});

            return services;
		}
	}
}
