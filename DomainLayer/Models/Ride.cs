using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;


namespace DomainLayer.Models
{
    public class Ride : BaseEntity
    { 

        public Guid ? DriverId { get; set; }

        public Guid ? UserId { get; set; }

        public Guid ? VehicleId { get; set; }

        public string ? PickupLocation { get; set; }

        public double PickUpLatitude { get; set; }

        public double PickUpLongitude  { get; set; }

        public string ? Destination { get; set; }

        public double DestinationLatitude { get; set; }

        public double DestinationLongitude { get; set; }

        public DateTime StartTime { get; set; } // Timestamp for when the ride stared

        public DateTime EndTime { get; set; } // Timestamp for when the ride Ended

        public required double RideFare { get; set; }

        public virtual Vehicle ? Vehicle { get; set; }

        public virtual User ? User { get; set; }

        public virtual Driver ? Driver { get; set; }

        public virtual Payment ? Payment { get; set; }
    }
}
