using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelListing.Business_Services;
using HotelListing.Helpers;
using HotelListing.Interfaces;
using HotelListing.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;

namespace HotelListing
{
    public class Startup
    {
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

           Log.Logger = new LoggerConfiguration()
          .ReadFrom.Configuration(configuration)
          .CreateLogger();
        }

    
        public void ConfigureServices(IServiceCollection services)
        {

            // Db conn string + DI
            services.AddDbContext<ApplicationDbContext>
              (options => options.UseSqlServer(Configuration.GetConnectionString("sqlServerConnection")));


            services.AddScoped<ICountryServices, CountrySevices>();
            services.AddScoped<IHotelServices, HotelServices>();



            services.AddAutoMapper(typeof(ApplicationMapper));


            services.AddControllers().AddNewtonsoftJson(opt=>
                  opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
      
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelListing", Version = "v1" });
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelListing v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
