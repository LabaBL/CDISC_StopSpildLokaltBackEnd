using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CDISC_StopSpildLokaltBackEnd {

    //https://docs.microsoft.com/en-us/ef/core/get-started/aspnetcore/new-db?tabs=visual-studio

    public class OrganizationalDBContext : DbContext {
        public OrganizationalDBContext(DbContextOptions<OrganizationalDBContext> options) : base(options) {

        }

        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Identification> Identifications { get; set; }
        public DbSet<Organization> Organizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Volunteer>().ToTable("Volunteer");
            modelBuilder.Entity<Team>().ToTable("Team");
            modelBuilder.Entity<Identification>().ToTable("Identification");
            modelBuilder.Entity<Organization>().ToTable("Organization");
            //modelBuilder.Entity<Volunteer>()
            //    .HasOne(v => v.Identification)
            //    .WithOne(i => i.Volunteer)
            //    .HasForeignKey<Identification>(i => i.Volunteer);
        }
    }

}
