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
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Identity;
using System.Linq;
using System;

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
            
           
            services.AddTransient<ICarRepository, CarRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IOrdersRepository, OrdersRepository>();
            // services.AddMvc(optins => optins.EnableEndpointRouting =false);
            services.AddRazorPages();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => ShopCart.GetCart(sp));
            services.AddSession();
            services.AddMvc((optins => optins.EnableEndpointRouting = false));
            services.AddMemoryCache();
            
            
            //services.AddSession();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            
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
            

        }
    }
}
