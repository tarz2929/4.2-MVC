using Microsoft.EntityFrameworkCore;
using ProductInformation.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductInformation.Models
{
    public class ProductInfoContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connection =
                    "server=localhost;" +
                    "port=3306;" +
                    "user=root;" +
                    "database=mvc_productinfo;";

                string version = "10.4.14-MariaDB";

                optionsBuilder.UseMySql(connection, x => x.ServerVersion(version));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

                entity.HasData(
                    new Category()
                    {
                        ID = -1,
                        Name = "Kitchen"
                    },
                    new Category()
                    {
                        ID = -2,
                        Name = "Garage"
                    }
                );
            });
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Name)
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

                string keyToCategory = "FK_" + nameof(Product) +
                    "_" + nameof(Category);
                // FK_Product_Category

                
                entity.HasIndex(e => e.CategoryID)
                .HasName(keyToCategory);

                entity.HasOne(thisEntity => thisEntity.Category)
                .WithMany(parent => parent.Products)
                .HasForeignKey(thisEntity => thisEntity.CategoryID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName(keyToCategory);

                entity.HasData(
                    new Product()
                    {
                        ID = -1,
                        CategoryID = -1,
                        Name = "Mixer"
                    },
                    new Product()
                    {
                        ID = -2,
                        CategoryID = -1,
                        Name = "Rice Cooker"
                    },
                    new Product()
                    {
                        ID = -3,
                        CategoryID = -2,
                        Name = "Wrench Set"
                    },
                    new Product()
                    {
                        ID = -4,
                        CategoryID = -2,
                        Name = "Floor Jack"
                    },
                    new Product()
                    {
                        ID = -5,
                        CategoryID = -2,
                        Name = "Screwdriver Set"
                    }
                );
            });
        }
    }
}