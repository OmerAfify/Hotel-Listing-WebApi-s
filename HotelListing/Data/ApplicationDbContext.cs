using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelListing.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Models
{
    public class ApplicationDbContext : IdentityDbContext<MyIdentityUser>
    {
        public DbSet<Country> countries { get; set; }
        public DbSet<Hotel> hotels { get; set; }


        public ApplicationDbContext()
        {

        }
      
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }




        }
    }
