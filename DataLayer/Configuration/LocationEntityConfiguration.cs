using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore; 
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace DataLayer.Configuration
{
    public class LocationEntityConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Location");

            builder.HasKey(l => l.Id);

            builder.Property(p => p.Id)
                .HasColumnName("LocationId");

            builder.HasAlternateKey(p => new { p.Name });    

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(p => p.Latitude)
                .HasColumnType("decimal(8, 6)")
                .IsRequired();

            builder.Property(p => p.Longitude)
                .HasColumnType("decimal(9, 6)")
                .IsRequired();
        }
    }
}