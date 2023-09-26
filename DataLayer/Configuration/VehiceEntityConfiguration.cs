using System.Collections.Immutable;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore; 
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace DataLayer.Configuration
{
    public class VehicleEntityConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("Vehicle");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .HasColumnName("Id");

            builder.HasOne(v => v.Ride)
                .WithOne(r => r.Vehicle)
                .HasForeignKey<Ride>(r => r.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull);


            builder.HasOne(v => v.Driver)
                .WithMany(d => d.Vehicles)
                .HasForeignKey(v => v.DriverId);

            builder.HasAlternateKey( v => new { v.LicensePlate });

            builder.Property(v => v.LicensePlate)
                .IsRequired();

            builder.Property(v => v.NumberOfSeats)
                .IsRequired();
            
            builder.Property(v => v.Model)
                .IsRequired();

            builder.Property(v => v.Color)
                .IsRequired();

            builder.Property(v => v.VehiclePhotoUrl)
                .IsRequired();
            
            builder.Property(v => v.InsurancePhotoUrl)
                .IsRequired();
   
        }
    }
}