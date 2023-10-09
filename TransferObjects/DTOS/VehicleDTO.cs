using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferLayer.DTOS
{
    public class VehicleDTO
    {
        public Guid DriverId { get; set; }

        public Guid RideId { get; set; }

        public required string Model { get; set; }

        public required string Color { get; set; }

        public int NumberOfSeats { get; set; } 

        public string LicensePlate { get; set; }

        public required string VehiclePhotoUrl { get; set; }

        public required string InsurancePhotoUrl { get; set; }

    }
}
