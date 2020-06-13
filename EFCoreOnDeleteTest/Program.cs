using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;

namespace EFCoreOnDeleteTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging((hostingContext,logging) =>
            {

                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                logging.AddEventLog(new EventLogSettings()
                {
                    SourceName = "EFCoreTestLog",
                    LogName = "EFCoreTestLog",
                    Filter = (x, y) => y >= LogLevel.Information
                });
                // clear default logging providers
                logging.ClearProviders();

                // add built-in providers manually, as needed 
                logging.AddConsole();
                logging.AddDebug();
                //logging.AddEventLog();
                logging.AddEventSourceLogger();




            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                ;
    }
}
