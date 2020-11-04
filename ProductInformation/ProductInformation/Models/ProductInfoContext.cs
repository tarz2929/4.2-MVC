using Microsoft.EntityFrameworkCore;
using ProductInformation.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstASP.Models
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
                /*
                string keyName = "FK_" + nameof(EMailAddress) +
                    "_" + nameof(Person);
                */

                entity.Property(e => e.Name)
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

                /*
                entity.HasIndex(e => e.PersonID)
                .HasName(keyName);

                entity.HasOne(thisEntity => thisEntity.Person)
                .WithMany(parent => parent.EMailAddresses)
                .HasForeignKey(thisEntity => thisEntity.PersonID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName(keyName);
                */
            });
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Name)
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");
            });
        }
    }
}