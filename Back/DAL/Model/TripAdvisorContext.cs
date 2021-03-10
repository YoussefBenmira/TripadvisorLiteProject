using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Model
{
    public class TripAdvisorContext : DbContext
    {
        public TripAdvisorContext()
        {

        }
        public TripAdvisorContext(DbContextOptions<TripAdvisorContext> options) : base(options)
        {
        }

        public  DbSet<Location> Locations { get; set; }
        public  DbSet<Opinion> Opinions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>().ToTable("Location");
            modelBuilder.Entity<Opinion>().ToTable("Opinion");
        }


    }
}
