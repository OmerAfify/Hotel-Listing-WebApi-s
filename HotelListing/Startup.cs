using HotelListing.Business_Services;
using HotelListing.Data;
using HotelListing.Helpers;
using HotelListing.Interfaces;
using HotelListing.Interfaces.IServices;
using HotelListing.Models;
using HotelListing.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace HotelListing
{
    public class Startup
    {
        
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
   
        public void ConfigureServices(IServiceCollection services)
        {

            //Entity Framework
            services.AddDbContext<ApplicationDbContext>
                 (options => options.UseSqlServer(Configuration.GetConnectionString("sqlServerConnection")));


            //Identity  
            services.AddIdentity<MyIdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>         // Adding Jwt Bearer config
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true, //
                    ValidateLifetime = true, //
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    ValidAudience = Configuration["JWT:ValidAudience"],
               
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SecretTokenKeyValue")))
                };
            });


            //cors policy
            services.AddCors(o => {
                o.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                    .AllowAnyHeader());
            });


            //AutoMapper
            services.AddAutoMapper(typeof(ApplicationMapper));


            //Dependency Injection config 
            services.AddScoped<ICountryServices, CountrySevices>();
            services.AddScoped<IHotelServices, HotelServices>();
            services.AddScoped<IUserAuthenticationManager, UserAuthenticationManager>();


            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelListing", Version = "v1" });
            });


            //Adding Controllers + configuring refrencial loop handling 
            services.AddControllers().AddNewtonsoftJson(op =>
                op.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        }

          public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => { 
                string swaggerJsonBasepath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasepath}/swagger/v1/swagger.json", "HotelListingAPI");
                
                }) ;
            

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

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


