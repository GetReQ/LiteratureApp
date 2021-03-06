﻿// <auto-generated />
using Literature.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace literature.Migrations
{
    [DbContext(typeof(MyDatabaseContext))]
    [Migration("20190419155005_InitCreate")]
    partial class InitCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("Literature.Models.Publication", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<string>("Language");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.ToTable("Publication");
                });
#pragma warning restore 612, 618
        }
    }
}
