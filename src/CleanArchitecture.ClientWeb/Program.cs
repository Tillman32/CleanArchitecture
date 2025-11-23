using CleanArchitecture.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CleanArchitecture.ClientWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Setup logging first to catch all errors
            NLog.LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration("nlog.config");
            var logger = NLog.LogManager.GetCurrentClassLogger();

            try
            {
                logger.Debug("Starting up...");
                IHostBuilder builder = CreateHostBuilder(args);
                IHost host = builder.Build();

                // Seed the Database
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    try
                    {
                        var context = services.GetRequiredService<ApplicationDbContext>();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                        ApplicationDbInitializer.Initialize(context);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "An error occurred while seeding the database.");
                    }
                }

                host.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Program could not start due to error.");
                throw;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
