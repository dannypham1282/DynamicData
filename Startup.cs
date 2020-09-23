using DynamicData.Data;
using DynamicData.Interface;
using DynamicData.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DynamicData
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add connection string
            services.AddDbContext<DatabaseContext>(options => options
            .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()))
            .UseSqlServer(Configuration.GetConnectionString("appConn")).EnableSensitiveDataLogging());
            //End

            //add Interface and Repository
            services.AddTransient<IUser, UserRepo>();
            services.AddTransient<ILibrary, LibraryRepo>();
            services.AddTransient<ILibraryType, LibraryTypeRepo>();
            services.AddTransient<IItem, ItemRepo>();
            services.AddTransient<IField, FieldRepo>();
            services.AddTransient<IDefaultField, DefaultFieldRepo>();
            services.AddTransient<IFieldValue, FieldValueRepo>();
            services.AddTransient<ICommon, CommonRepo>();
            services.AddTransient<IUserRoles, UserRoleRepos>();
            //End 

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



            //Add Claim base authentication and authorization. Add JSON Serializer
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                    options =>
                    {
                        options.LoginPath = new PathString("/Account/Login/");
                        options.AccessDeniedPath = new PathString("/Account/AccessDenied/");
                        options.LogoutPath = new PathString("/Account/Login/");
                    });

            services.AddMvc(config =>
             {
                 var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                 config.Filters.Add(new AuthorizeFilter(policy));
             }).AddNewtonsoftJson(o =>
             {
                 o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                 o.SerializerSettings.DateFormatString = "mm/dd/yy, dddd";
             });
            services.AddControllersWithViews();

            //End
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                app.UseEndpoints(endpoints =>
                {
                    //endpoints.MapAreaControllerRoute(name: "default", areaName: "Site", pattern: "{controller=Home}/{action=Index}/{id?}");
                    //endpoints.MapControllerRoute(name: "areaRoute", pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

                    endpoints.MapControllerRoute(
                        name: "default_Guid",
                        pattern: "{controller=Home}/{action=Index}/{guid?}");

                    //                endpoints.MapControllerRoute(
                    //    "Guid",
                    //    "{controller}/{action}/{guid}",
                    //    new { controller = "Home", action = "Index" },
                    //    new { guid = new GuidRouteConstraint() }
                    //);
                });
            });
        }
    }
}
