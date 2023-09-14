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
                .HasColumnName("RideId");

            builder.HasOne(r => r.Driver)
                   .WithOne(d => d.Ride)
                   .HasForeignKey<Driver>(d => d.RideId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Vehicle)
                   .WithOne(v => v.Ride)
                   .HasForeignKey<Vehicle>(v => v.RideId)
                   .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(r => r.Payment)
                .WithOne(p => p.Ride)
                .HasForeignKey<Payment>(p => p.RideId);    

            builder.Property(r => r.PickUpLatitude)
                .HasColumnType("decimal(8, 6)")
                .IsRequired();

            builder.Property(r => r.PickUpLongitude)
                .HasColumnType("decimal(9, 6)")
                .IsRequired();
            
            builder.Property(r => r.DestinationLatitude)
                .HasColumnType("decimal(8, 6)")
                .IsRequired();

            builder.Property(r => r.DestinationLongitude)
                .HasColumnType("decimal(9, 6)")
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