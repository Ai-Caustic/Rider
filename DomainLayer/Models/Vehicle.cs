using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Vehicle : BaseEntity
    {
    
        public Guid DriverId { get; set; }

        public Guid RideId { get; set; }

        public required string Model { get; set; }

        public required string Color { get; set; }

        public int NumberOfSeats { get; set; } 

        public string LicensePlate { get; set; }

        public required string VehiclePhotoUrl { get; set; }

        public required string InsurancePhotoUrl { get; set; }

        public virtual Driver Driver { get; set; }

        public virtual Ride ? Ride { get; set; }

        public Vehicle() {}

        public static Vehicle Create(Guid driverId, string model, string color, int numberOfSeats, string licensePlate, string vehiclePhotoUrl, string insurancePhotoUrl)
        {
            var vehicle = new Vehicle()
            {
                DriverId = driverId,
                Model = model,
                Color = color,
                NumberOfSeats = numberOfSeats,
                LicensePlate = licensePlate,
                VehiclePhotoUrl = vehiclePhotoUrl,
                InsurancePhotoUrl = insurancePhotoUrl
            };

            vehicle.GenerateNewIdentity();
            
            return vehicle;
        }
    }
}