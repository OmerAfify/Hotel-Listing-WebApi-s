using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace HotelListing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try {

                Log.Information("Starting your app");
                CreateHostBuilder(args).Build().Run(); 
            } catch (Exception ex) {   
                
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
