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
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("Id");

            builder.HasMany(u => u.Rides)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);

            builder.HasMany(u => u.Payments)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            builder.HasAlternateKey(u => new { u.Email, u.IdNumber, u.PhoneNumber });

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.UserName)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(u => u.IdNumber)
                .IsRequired()
                .HasMaxLength(8);

            builder.Property(u => u.PhoneNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(u => u.IdPhotoUrl)
                .IsRequired();
            
            var converter = new ValueConverter<DateOnly, DateTime>(
                u => u.ToDateTime(default), // Convert DateOnly to DateTime
                u => DateOnly.FromDateTime(u) // Convert DateTime to DateOnly
            );

            builder.Property(u => u.BirthDate)
                .HasConversion(converter); // Make sure the column is a date
        }
    }
}