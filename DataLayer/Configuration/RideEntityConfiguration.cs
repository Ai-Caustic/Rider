using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore; 
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace DataLayer.Configuration
{
    public class RideEntityConfiguration : IEntityTypeConfiguration<Ride>
    {
        public void Configure(EntityTypeBuilder<Ride> builder)
        {
            builder.ToTable("Ride");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id )
                .HasColumnName("Id");

            builder.HasOne(r => r.Driver)
                   .WithMany(d => d.Rides)
                   .HasForeignKey(r => r.DriverId);

            builder.HasOne(r => r.Vehicle)
                   .WithOne(v => v.Ride)
                   .HasForeignKey<Vehicle>(v => v.RideId)
                   .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(r => r.Payment)
                .WithOne(p => p.Ride)
                .HasForeignKey<Payment>(p => p.RideId); 

            builder.HasOne(r => r.User)
                   .WithMany(u => u.Rides)
                   .HasForeignKey(r => r.UserId); 

            builder.Property(r => r.PickupLocation)
                .IsRequired();

            builder.Property(r => r.Destination)
                .IsRequired();
            
            builder.Property(r => r.StartTime)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(r => r.EndTime)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(r => r.RideFare)
                .IsRequired()
                .HasColumnType("smallmoney");
        }
    }
}