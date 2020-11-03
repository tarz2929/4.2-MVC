using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstASP.Models
{
    class PersonContext : DbContext
    {
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<EMailAddress> EMailAddresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connection =
                    "server=localhost;" +
                    "port=3306;" +
                    "user=root;" +
                    "database=mvc_4point2;";

                string version = "10.4.14-MariaDB";

                optionsBuilder.UseMySql(connection, x => x.ServerVersion(version));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.FirstName)
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.LastName)
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

                /*
                entity.HasData(
                    new Person()
                    {
                        ID = -1,
                        Name = "Ford"
                    },
                    new Person()
                    {
                        ID = -2,
                        Name = "Chevrolet"
                    },
                    new Person()
                    {
                        ID = -3,
                        Name = "Dodge"
                    }
                );
                */
            });
            modelBuilder.Entity<EMailAddress>(entity =>
            {
                string keyName = "FK_" + nameof(EMailAddress) +
                    "_" + nameof(Person);

                // These SHOULD be set automatically. If you want to play around with it by removing these and verify this version of EF works that way, feel free. 
                entity.Property(e => e.Address)
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.PersonID)
                .HasName(keyName);

                entity.HasOne(thisEntity => thisEntity.Person)
                .WithMany(parent => parent.EMailAddresses)
                .HasForeignKey(thisEntity => thisEntity.PersonID)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName(keyName);

                /*
                entity.HasData(
                    new EMailAddress()
                    {
                        ID = -1,
                        PersonID = -1,
                        Colour = "Blue",
                        Model = "Fusion",
                        ModelYear = 2010
                    },
                    new EMailAddress()
                    {
                        ID = -2,
                        PersonID = -1,
                        Colour = "Black",
                        Model = "Escape",
                        ModelYear = 2014
                    },
                    new EMailAddress()
                    {
                        ID = -3,
                        PersonID = -2,
                        Colour = "Red",
                        Model = "Cruze",
                        ModelYear = 2012
                    },
                    new EMailAddress()
                    {
                        ID = -4,
                        PersonID = -3,
                        Colour = "Black",
                        Model = "Ram",
                        ModelYear = 2018
                    },
                    new EMailAddress()
                    {
                        ID = -5,
                        PersonID = -3,
                        Colour = "Blue",
                        Model = "Charger",
                        ModelYear = 2016
                    }
                    );
                    */
            });
        }
    }
}