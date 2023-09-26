using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DomainLayer.Enums;

namespace DomainLayer.Models
{
    public class User : BaseEntity
    {
        public required string Email { get; set; }

        public required string UserName { get; set; }

        [RegularExpression(@"^[0-9]{10}$")] // 10-digit phone number validation
        public required int PhoneNumber { get; set; } 

        public required int IdNumber { get; set; }

        public string ? ProfilePhotoUrl { get; set; }

        public DateOnly ? BirthDate { get; set; }

        public string ? Gender { get; set; }

        public required string IdPhotoUrl { get; set; }

        public Roles Role { get; set; } 


        public ICollection<Ride> ? Rides { get; set; }

        public ICollection<Payment> ?  Payments { get; set; }
    
    }
}