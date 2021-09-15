﻿// <auto-generated />
using System;
using AdvertTrackerAPI.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AdvertTrackerAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210914220708_AddedHangfireConnection")]
    partial class AddedHangfireConnection
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AdvertTrackerAPI.Models.Vendor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("LastVisted")
                        .HasColumnType("datetime2");

                    b.Property<string>("VendorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VisitorCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MyProperty");
                });
#pragma warning restore 612, 618
        }
    }
}
