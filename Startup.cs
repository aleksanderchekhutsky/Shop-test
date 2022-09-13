using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Data.Interfaces;
using Shop.Data.Mocks;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Data.Repository;
using Microsoft.AspNetCore.Http;
using Shop.Data.Models;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;

using Microsoft.AspNetCore.Identity;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Shop.Areas.Identity.Data;
using Microsoft.Extensions.Logging;
using AspNetCore.Unobtrusive.Ajax;

namespace Shop
{ 
    public class Startup
    {
        private IConfigurationRoot _confstring;

        [System.Obsolete]
        public Startup(IHostingEnvironment hostEnv)
        {
            _confstring = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDBContent>(options => options.UseSqlServer(_confstring.GetConnectionString("DefaultConnection")));
            services.AddTransient<ICreditCardRepository, DapperCreditCardRepository>();

            services.AddTransient<IPayRepository, DapperPayRepository>();

            services.AddTransient<IWalletTransactionRepository, DapperWalletTransactionRepository>();

           // services.AddTransient<ICarRepository, CarRepository>(); Entity
            services.AddTransient<ICarRepository, DapperCarRepository>();   //dapper
            //services.AddTransient<ICategoryRepository, CategoryRepository>();  entity
            services.AddTransient<ICategoryRepository, DapperCategoryRepository>();
            //services.AddTransient<IOrdersRepository, OrdersRepository>();         //<-- Entity
            services.AddTransient<IOrdersRepository, DapperOrdersRepository>();     //<-- Dapper
            services.AddScoped<IWalletRepository, DapperWalletRepository>();
            // services.AddMvc(optins => optins.EnableEndpointRouting =false);
            services.AddRazorPages();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => ShopCart.GetCart(sp));
            services.AddSession();
            services.AddMvc((optins => optins.EnableEndpointRouting = false));
            services.AddMemoryCache();
            services.AddUnobtrusiveAjax();
            //services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            //services.AddSingleton<ILogger>(_ => new ("shop"));
            //services.AddIdentity<ShopUser, IdentityRole>()
            //.AddEntityFrameworkStores<AppDBContent>();


            //services.AddSession();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication(); 
            app.UseAuthorization();

            //app.UseIdentity();
            ///app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();
            //app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(name: "categoryFilter", template: "Car/{action}/{category?}", defaults: new { Controller = "Car", action = "List" });
            });
            //app.MapControllerRoute(
            //    name: "default",
            //    pattern:"{controller=Home}/{action=Index}/{id?}"
            //    );
            //DBObjects.Initial(app);
            using (var scope = app.ApplicationServices.CreateScope())
            {
                AppDBContent content = scope.ServiceProvider.GetRequiredService<AppDBContent>();
                DBObjects.Initial(content);
            }
            
            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);


        }
    }
}
