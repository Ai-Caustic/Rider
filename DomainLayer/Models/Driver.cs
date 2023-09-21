using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DomainLayer.Enums;

namespace DomainLayer.Models
{
    public class Driver : BaseEntity
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }  

        public required string Email { get; set; }

        [RegularExpression(@"^[0-9]{10}$")] // 10-digit phone number validation
        //[Required]
        public int PhoneNumber { get; set; }

        public Guid RideId { get; set; }

        public Roles Roles { get; set; }
        [Required]
        public  string DriverPhotoUrl { get; set; }

        public required string LicensePhotoUrl { get; set; }

        public required string VerificationBadgeUrl { get; set; }  // NTSA Verification Badge photo

        public virtual ICollection<Vehicle> ? Vehicles { get; set; }

        public virtual ICollection<Ride> ? Rides { get; set;}

    }
}