using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferLayer.Enums;

namespace TransferLayer.DTOS
{
    public class PaymentDTO
    {
        public Guid UserId { get; set; }

        public Guid RideId { get; set; }

        public required string PaymentMethod { get; set; }

        public required double PaymentAmount { get; set; }

        public required DateTime TimeStamp { get; set; }
        
    }
}
