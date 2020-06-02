using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AutoMapper;
using CarPark.BLL.MappingProfiles;
using CarPark.DAL.EF;
using CarPark.DAL.Interfaces;
using CarPark.DAL.Repository;
using CarPark.WebAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace CarPark.WebAPI
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
            var bllAssembly = Assembly.Load("CarPark.BLL");
            services.AddDbContext<CarParkContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CarparkDb")))
                .Scan(scan => scan
                    .FromAssemblies(bllAssembly)
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                    .AsSelf()
                    .WithTransientLifetime())
                .AddAutoMapper(typeof(MapperProfile), typeof(WebUI.MappingProfiles.MapperProfile))
                .AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddControllers();
            services.AddAuthentication()
                .AddJwtBearer(configureOptions => configureOptions.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidIssuer = JwtInformation.Issuer,
                        ValidAudience = JwtInformation.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtInformation.Key))
                    });

            services.AddSwaggerGen(swaggerOptions =>
            {
                swaggerOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "CarParkApi" });

                swaggerOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"

                });

                swaggerOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name =  "Bearer",
                            In = ParameterLocation.Header
                        } ,
                        new List<string>()
                    }
                });
            });

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<CarParkContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(swaggerOptions =>
                {
                    swaggerOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "CarParkApi");
                    swaggerOptions.RoutePrefix = string.Empty;
                }
            );
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
