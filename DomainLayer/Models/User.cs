using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class User : BaseEntity
    {
        public required string Email { get; set; }

        public required int PhoneNumber { get; set; }

        public required int IdNumber { get; set; }

        public byte[] ? ProfilePhoto { get; set; }

        public DateTime ? BirthDate { get; set; }

        public string ? Gender { get; set; }

        public required byte[] IdPhoto { get; set; }
    

    }
}