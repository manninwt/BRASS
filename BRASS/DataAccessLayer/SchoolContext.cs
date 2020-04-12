using BRASS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace BRASS.DataAccessLayer
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }
        public DbSet<Buses> Buses { get; set; }
        public DbSet<Drivers> Drivers { get; set; }
        public DbSet<RoutePoints> RoutePoints { get; set; }
        public DbSet<Routes> Routes { get; set; }
        public DbSet<RouteStops> RouteStops { get; set; }
        public DbSet<School> School { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<StudentsOnBus> StudentsOnBus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Buses>().ToTable("Bus");
            modelBuilder.Entity<Drivers>().ToTable("Driver");
            modelBuilder.Entity<RoutePoints>().ToTable("RoutePoints");
            modelBuilder.Entity<Routes>().ToTable("Route");
            modelBuilder.Entity<RouteStops>().ToTable("RouteStops");
            modelBuilder.Entity<School>().ToTable("School");
            modelBuilder.Entity<Students>().ToTable("Student");
            modelBuilder.Entity<StudentsOnBus>().ToTable("StudentsOnBus");
        }
    }
}
