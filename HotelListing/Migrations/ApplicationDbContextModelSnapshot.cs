﻿// <auto-generated />
using HotelListing.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelListing.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("HotelListing.Models.Country", b =>
                {
                    b.Property<int>("countryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("countryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("countryShortName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("countryId");

                    b.ToTable("countries");
                });

            modelBuilder.Entity("HotelListing.Models.Hotel", b =>
                {
                    b.Property<int>("hotelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("countryId")
                        .HasColumnType("int");

                    b.Property<string>("hotelAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("hotelName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("rating")
                        .HasColumnType("float");

                    b.HasKey("hotelId");

                    b.HasIndex("countryId");

                    b.ToTable("hotels");
                });

            modelBuilder.Entity("HotelListing.Models.Hotel", b =>
                {
                    b.HasOne("HotelListing.Models.Country", "country")
                        .WithMany()
                        .HasForeignKey("countryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("country");
                });
#pragma warning restore 612, 618
        }
    }
}