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

        [RegularExpression(@"^([\+]?2547[-]?|[0])?[1-9][0-9]{8}$")]
        public string Mobile { get; set; }

        public Guid RideId { get; set; }

        public Roles Roles { get; set; }
        [Required]
        public  string DriverPhotoUrl { get; set; }

        public required string LicensePhotoUrl { get; set; }

        public required string VerificationBadgeUrl { get; set; }  // NTSA Verification Badge photo

        public virtual ICollection<Vehicle> ? Vehicles { get; set; }

        public virtual ICollection<Ride> ? Rides { get; set;}

        //Empty Constructor
        public Driver() {}

        //Custom driver methods
        public static Driver Create(string firstName, string lastName, string email, string mobile, Roles role, string driverPhotoUrl, string licensePhotoUrl, string verificationBadgeUrl, bool IsActive)
        {
            var driver = new Driver
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Mobile = mobile,
                Roles = Roles.Driver,
                DriverPhotoUrl = driverPhotoUrl,
                LicensePhotoUrl = licensePhotoUrl,
                VerificationBadgeUrl = verificationBadgeUrl,
                IsActive = true
            };

            driver.GenerateNewIdentity();

            return driver;
        }

    }
}