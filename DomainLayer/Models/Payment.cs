using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Payment : BaseEntity
    {
        public Guid ? UserId { get; set; }

        public Guid ? RideId { get; set; }

        public required string PaymentMethod { get; set; }

        public required double PaymentAmount { get; set; }

        public required DateTime TimeStamp { get; set; }

        public virtual User ? User { get; set; }

        public virtual Ride ? Ride { get; set; }
    }
}