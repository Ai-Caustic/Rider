using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DomainLayer.Models;
using DataLayer.Configuration;


namespace DataLayer.Data
{
    
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        public required DbSet<User> Users { get; set; }

        public required DbSet<Driver> Drivers { get; set; }

        public required DbSet<Ride> Rides { get; set; }

        public required DbSet<Vehicle> Vehicles { get; set; }

        public required DbSet<Location> Locations { get; set; }

        public required DbSet<Payment> Payments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());

            modelBuilder.ApplyConfiguration(new DriverEntityConfiguration());

            modelBuilder.ApplyConfiguration(new RideEntityConfiguration());

            modelBuilder.ApplyConfiguration(new VehicleEntityConfiguration());

            modelBuilder.ApplyConfiguration(new LocationEntityConfiguration());

            modelBuilder.ApplyConfiguration(new PaymentEntityConfiguration());
        }
    
    }
}