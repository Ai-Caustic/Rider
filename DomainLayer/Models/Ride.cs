using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DomainLayer.Enums;


namespace DomainLayer.Models
{
    public class Ride : BaseEntity
    { 

        public Guid ? DriverId { get; set; }

        public Guid ? UserId { get; set; }

        public Guid ? VehicleId { get; set; }

        public Guid PaymentId { get; set; }

        public string PickupLocation { get; set; }

        public string Destination { get; set; }

        public DateTime StartTime { get; set; } // Timestamp for when the ride stared

        public DateTime EndTime { get; set; } // Timestamp for when the ride Ended

        public RideStatus Status { get; set; } // Status of the ride

        public double RideFare { get; set; }

        public virtual Vehicle ? Vehicle { get; set; }

        public virtual User ? User { get; set; }

        public virtual Driver ? Driver { get; set; }

        public virtual Payment ? Payment { get; set; }


        //empty constructor
        public Ride() {}

        public static Ride Create(Guid driverId, Guid userId, Guid vehicleId, string pickupLocation, string destination, DateTime startTime, DateTime endTime, RideStatus status, double rideFare, bool isActive)
        {
            var ride = new Ride
            {
                DriverId = driverId,
                UserId = userId,
                VehicleId = vehicleId,
                PickupLocation = pickupLocation,
                Destination = destination,
                StartTime = startTime,
                EndTime = endTime,
                Status = RideStatus.Requested,
                RideFare = rideFare,
                IsActive = isActive
            };

            ride.GenerateNewIdentity();
            return ride;
        }
    }
}
