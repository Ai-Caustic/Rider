using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore; 
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace DataLayer.Configuration
{
    public class DriverEntityConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.ToTable("Driver");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                .HasColumnName("DriverId");

            builder.HasOne(d => d.Ride)
                .WithOne(r => r.Driver)
                .HasForeignKey<Ride>(r => r.DriverId);                

            builder.HasAlternateKey(d => new { d.Email, d.PhoneNumber });

            builder.Property(d => d.FirstName)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(d => d.LastName)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(d => d.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(d => d.PhoneNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(d => d.DriverPhotoUrl)
                .IsRequired();

            builder.Property(d => d.LicensePhotoUrl)
                .IsRequired();

            builder.Property(d => d.VerificationBadgeUrl)
                .IsRequired();

        }
    }
}