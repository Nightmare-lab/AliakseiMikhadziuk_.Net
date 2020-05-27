using System.Reflection;
using AutoMapper;
using CarPark.BLL.MappingProfiles;
using CarPark.DAL.EF;
using CarPark.DAL.Interfaces;
using CarPark.DAL.Models;
using CarPark.DAL.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace CarPark.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

      
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();

            var bllAssembly = Assembly.Load("CarPark.BLL");

            services.AddDbContext<CarParkContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("CarparkDb")))
                .Scan(scan => scan
                    .FromAssemblies(bllAssembly)
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                    .AsSelf()
                    .WithTransientLifetime())
                .AddAutoMapper(typeof(MapperProfile),typeof(WebUI.MappingProfiles.MapperProfile))
                .AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                    {
                        options.Password.RequireNonAlphanumeric = false;
                    })
                .AddEntityFrameworkStores<CarParkContext>();
        }

      
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
               
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }

    }
}
