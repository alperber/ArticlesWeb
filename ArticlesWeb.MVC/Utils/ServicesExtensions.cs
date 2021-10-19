using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using ArticlesWeb.MVC.DataAccess;
using ArticlesWeb.MVC.Models.DbEntities;

namespace ArticlesWeb.MVC.Utils
{
    public static class ServicesExtensions
    {
        public static IServiceCollection 
            ConfigureDatabase(this IServiceCollection services,
                                   IConfiguration configuration)
        {
            services.AddDbContext<ArticlesContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DevConnection"));
            });

            return services;
        }

        public static IServiceCollection AddCustomLocalization(this IServiceCollection services)
        {
            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });

            services
                .AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            services.Configure<RequestLocalizationOptions>(opt =>
            {
                var supportedCulture = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("tr-TR")
                };
                opt.DefaultRequestCulture = new RequestCulture("en-US");
                opt.SupportedCultures = supportedCulture;
                opt.SupportedUICultures = supportedCulture;
            });

            return services;
        }

        public static IServiceCollection AddCustomIdentity(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.Configure<IdentityOptions>(configuration.GetSection("IdentityOptions"));
            
            services.AddAuthentication(o =>
            {
                o.DefaultScheme = IdentityConstants.ApplicationScheme;
                o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddIdentityCookies();

            services.AddIdentityCore<User>()
                .AddSignInManager()
                .AddEntityFrameworkStores<ArticlesContext>();


            var hour = configuration["CookieOptions:ExpireHour"].ToInt32();
            var slidingExpiration = bool.Parse(configuration["CookieOptions:SlidingExpiration"]);
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(hour);
                
                options.LoginPath = configuration["CookieOptions:LoginPath"];
                
                options.AccessDeniedPath = configuration["CookieOptions:AccessDeniedPath"];

                options.SlidingExpiration = slidingExpiration;
            });

            return services;
        }
        
        public static int ToInt32(this string @string) => Int32.Parse(@string);
    }
}
