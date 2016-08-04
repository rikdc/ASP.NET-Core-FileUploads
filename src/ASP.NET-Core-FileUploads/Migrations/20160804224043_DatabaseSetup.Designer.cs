using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ASP.NET_Core_FileUploads.Models.Context;

namespace ASP.NETCoreFileUploads.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160804224043_DatabaseSetup")]
    partial class DatabaseSetup
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("ASP.NET_Core_FileUploads.Models.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("PathToFile");

                    b.HasKey("Id");

                    b.ToTable("Documents");
                });
        }
    }
}
