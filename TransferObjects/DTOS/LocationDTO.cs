using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferLayer.Enums;

namespace TransferLayer.DTOS
{
    public class LocationDTO
    {
        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

    }
}
