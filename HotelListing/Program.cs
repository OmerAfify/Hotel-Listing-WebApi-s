using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace HotelListing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().WriteTo.File(
                path: "D:\\6-Programming and courses\\New Projects\\Asp.net Core\\HotelListing\\HotelListing\\Serilogs\\log-.txt",
                rollingInterval: RollingInterval.Day,
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message}{NewLine}{Exception}",
                restrictedToMinimumLevel: LogEventLevel.Information
                ).CreateLogger();
           
 
            try {
                Log.Information("Starting your app");
                CreateHostBuilder(args).Build().Run(); 
                } 
            catch (Exception ex) {   
                
                Log.Fatal(ex, "app failed to start");
            
            } finally {

                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
