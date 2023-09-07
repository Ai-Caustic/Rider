using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Vehicle : BaseEntity
    {

        public Guid ? DriverId { get; set; }

        public required string Model { get; set; }

        public required string Color { get; set; }

        public required int NumberOfSeats { get; set; }  

        public required string LicensePlate { get; set; }

        public required string VehiclePhotoUrl { get; set; }

        public required string InsurancePhotoUrl { get; set; }


        public virtual Driver ? Driver { get; set; }

        public virtual Ride ? Ride { get; set; }


    }
}