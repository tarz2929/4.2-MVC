﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductInformation.Models;

namespace ProductInformation.Migrations
{
    [DbContext(typeof(ProductInfoContext))]
    [Migration("20201104182006_SeedData")]
    partial class SeedData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ProductInformation.Models.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.HasKey("ID");

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            ID = -1,
                            Name = "Kitchen"
                        },
                        new
                        {
                            ID = -2,
                            Name = "Garage"
                        });
                });

            modelBuilder.Entity("ProductInformation.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(10)");

                    b.Property<int>("CategoryID")
                        .HasColumnType("int(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID")
                        .HasName("FK_Product_Category");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            ID = -1,
                            CategoryID = -1,
                            Name = "Mixer"
                        },
                        new
                        {
                            ID = -2,
                            CategoryID = -1,
                            Name = "Rice Cooker"
                        },
                        new
                        {
                            ID = -3,
                            CategoryID = -2,
                            Name = "Wrench Set"
                        },
                        new
                        {
                            ID = -4,
                            CategoryID = -2,
                            Name = "Floor Jack"
                        },
                        new
                        {
                            ID = -5,
                            CategoryID = -2,
                            Name = "Screwdriver Set"
                        });
                });

            modelBuilder.Entity("ProductInformation.Models.Product", b =>
                {
                    b.HasOne("ProductInformation.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryID")
                        .HasConstraintName("FK_Product_Category")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
