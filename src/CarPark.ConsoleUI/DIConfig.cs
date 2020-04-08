using System.Reflection;
using AutoMapper;
using CarPark.BLL.MappingProfile;
using CarPark.DAL.EF;
using CarPark.DAL.Interfaces;
using CarPark.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CarPark.ConsoleUI
{
    public static class DIConfig
    {
        public static ServiceProvider Configure(IConfigurationRoot config)
        {
            var bllAssembly = Assembly.Load("CarPark.Bll");
            var consoleAssembly = Assembly.Load("CarPark.ConsoleUI");

            return new ServiceCollection()
                .AddDbContext<CarParkContext>(options =>
                    options.UseSqlServer(config.GetConnectionString("CarparkDb")))
                .Scan(scan => scan
                    .FromAssemblies(bllAssembly, consoleAssembly)
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Manager")))
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Validator")))
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("ConsoleService")))
                    .AsSelf()
                    .WithTransientLifetime())
                .AddAutoMapper(typeof(MapperProfile))
                .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddLogging(loggingBuilder => loggingBuilder.AddFile("Logs/StoreApp-{Date}.txt"))
                .BuildServiceProvider();
        }
    }
}