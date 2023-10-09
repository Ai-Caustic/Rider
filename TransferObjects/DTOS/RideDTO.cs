using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferLayer.Enums;

namespace TransferLayer.DTOS
{
    public class RideDTO 
    {
        public Guid ? DriverId { get; set; }

        public Guid ? UserId { get; set; }

        public Guid ? VehicleId { get; set; }

        public Guid ? RideId { get; set; }

        public string ? PickupLocation { get; set; }

        public double PickUpLatitude { get; set; }

        public double PickUpLongitude  { get; set; }

        public string ? Destination { get; set; }

        public double DestinationLatitude { get; set; }

        public double DestinationLongitude { get; set; }

        public DateTime StartTime { get; set; } // Timestamp for when the ride stared

        public DateTime EndTime { get; set; } // Timestamp for when the ride Ended

        public RideStatus Status { get; set; } // Status of the ride

        public double RideFare { get; set; }

        public virtual VehicleDTO ? VehicleDTO { get; set; }

        public virtual UserDTO ? UserDTO { get; set; }

        public virtual DriverDTO ? DriverDTO { get; set; }

        public virtual PaymentDTO ? PaymentDTO { get; set; }
    }
}