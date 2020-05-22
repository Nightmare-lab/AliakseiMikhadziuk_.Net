using System.Reflection;
using AutoMapper;
using CarPark.BLL.MappingProfiles;
using CarPark.DAL.EF;
using CarPark.DAL.Interfaces;
using CarPark.DAL.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            var bllAssembly = Assembly.Load("CarPark.BLL");

            services.AddDbContext<CarParkContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("CarparkDb")))
                .Scan(scan => scan
                    .FromAssemblies(bllAssembly)
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                   // .AsImplementedInterfaces()
                    .AsSelf()
                    .WithTransientLifetime())
                .AddAutoMapper(typeof(MapperProfile))
                .AddScoped(typeof(IRepository<>), typeof(Repository<>));
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
