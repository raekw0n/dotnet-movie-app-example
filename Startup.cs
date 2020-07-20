using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcMovie.Data;
using Microsoft.EntityFrameworkCore;

namespace MvcMovie
{
    /**
     * ASP.NET Core apps use a Startup class which is named Startup by convention. The Startup class:
     *
     * - Optionally includes a ConfigureServices method to configure the app's services. A service is a reusable
     *   component that provides app functionality. Services are registered in ConfigureServices and consumed across
     *   the app via DI or ApplicationServices.
     * 
     * - Includes a Configure method to create the app's requess processing pipeline.
     *
     * ConfigureServices and Configure are called by the ASP.NET Core runtime when the app starts:
     */
    public class Startup
    {
        // Only the following service types can be injected into the Startup constructor when using the GenericHost:
        // - IWebHostConfiguration
        // - IHostConfiguration
        // - IConfiguration
        public Startup(IConfiguration configuration)
        {
            // Configuration providers, see notes in Program.cs for more details
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<MvcMovieContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("MvcMovieContext")));
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // ASP.NET Core MVC invokes controller classes and action method within them depending on the incoming
                // URL. The default URL routing logic used by MVC uses a format like this to determine what code to
                // invoke:
                //
                // - /[Controller]/[ActionName]/[Parameters]
                //
                // As you can see below, the app will default to /Movies/Index if a user browses to the app and does not
                // supply any URL segments.
                //
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Movies}/{action=Index}/{id?}");
            });
        }
    }
}
