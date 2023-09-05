using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DomainLayer.Models;


namespace DataLayer.Data
{
    
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuider)
        {
            modelBuider.Entity<User>(entity => 
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);
                
                entity.HasIndex(e => e.Email)
                .IsUnique();

                entity.Property(e => e.PhoneNumber)
                .IsRequired();
                
                entity.HasIndex(e => e.PhoneNumber)
                .IsUnique();

                entity.Property(e => e.IdNumber)
                .IsRequired();

                entity.HasIndex(e => e.IdNumber)
                .IsUnique();


                //Check Constraint for if User is active
                entity.HasCheckConstraint("CK_USER_IsActive", "[IsActive] IN (0, 1)");


            });
        }
    
    }
}