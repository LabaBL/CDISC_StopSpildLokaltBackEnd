using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CDISC_StopSpildLokaltBackEnd {

    //https://docs.microsoft.com/en-us/ef/core/get-started/aspnetcore/new-db?tabs=visual-studio

    public class SSLContext : DbContext {
        public SSLContext(DbContextOptions<SSLContext> options) : base(options) {

        }

        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Identification> Identifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Volunteer>().ToTable("Volunteer");
            modelBuilder.Entity<Team>().ToTable("Team");
            modelBuilder.Entity<Identification>().ToTable("Identification");
        }
    }

}
