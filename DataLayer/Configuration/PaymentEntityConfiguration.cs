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
    public class PaymentEntityConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payment");

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.User)
                   .WithMany(u => u.Payments)
                   .HasForeignKey(p => p.UserId);

            builder.Property(p => p.Id)
                .HasColumnName("Id");

            builder.Property(p => p.PaymentMethod)
                .IsRequired();

            builder.Property(p => p.PaymentAmount)
                .IsRequired()
                .HasColumnType("smallmoney"); 

            builder.Property(p => p.TimeStamp)
                .IsRequired()
                .HasColumnType("datetime");
        }
    }
}