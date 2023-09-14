using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DomainLayer.IRepository;

namespace DomainLayer.Models
{
    public class Location : BaseEntity
    {
        public required string Name { get; set; }

        public required double Latitude { get; set; }

        public required double Longitude { get; set; }

    }
}