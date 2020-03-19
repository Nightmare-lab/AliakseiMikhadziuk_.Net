using System;
using System.IO;
using BusinessLogic.Map;
using BusinessLogic.Services;
using CommandLine;
using CsvHelper.Configuration;
using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FileConverter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true);

            var config = configBuilder.Build();

            var services = new ServiceCollection()
                .AddSingleton(typeof(IRepository<Students>), typeof(CsvRepository<Students>))
                .AddSingleton(typeof(ClassMap<Students>), typeof(Mapping))
                .AddSingleton<IConfiguration>(config)
                .AddSingleton<ReportCreatorServices>()
                .AddTransient<ReportServices>()
                .AddLogging(log =>
                {
                    log.AddFile("Logs/log-{Date}.txt");
                    log.AddConsole();
                })
                .BuildServiceProvider();



            var logger = services.GetService<ILoggerFactory>().CreateLogger<Program>();
            
            logger.LogInformation("Starting application");

            AppStart(services.GetService<ReportServices>(), args);
            
            logger.LogInformation("All done!");
        }

        private static void AppStart(ReportServices service, string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    service.ChooseReportType(Enum.Parse<ReportType>(o.ReportType));
                });
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        private class Options
        {
            [Option('t', "report type", Required = true, HelpText = "Set output for report type. 1 is json, 2 is excel")]
            public string ReportType { get; set; }
        }
    }

}
