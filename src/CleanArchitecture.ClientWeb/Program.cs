using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Infrastructure.Database;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.ClientWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Setup logging first to catch all errors
            var logger =
                NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            IWebHost host = null;

            try
            {
                logger.Debug("Starting up...");
                host = BuildWebHost(args);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Program could not start due to error.");
                throw;
            }

            // Seed the Database
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    ApplicationDbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
