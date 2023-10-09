using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferLayer.Enums;

namespace TransferLayer.DTOS
{
    public class UserDTO
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        [RegularExpression(@"^([\+]?2547[-]?|[0])?[1-9][0-9]{8}$")]
        public string Mobile { get; set; }

        public int IdNumber { get; set; }

        public string ? ProfilePhotoUrl { get; set; }

        public DateOnly ? BirthDate { get; set; }
        
        public string Gender { get; set; }

        public string IdPhotoUrl { get; set; }


        // Include DTOs or Id's for related entity 
        public List<Guid> RideIds { get; set; } //TODO: Change this to a DTO

        public List<Guid> PaymentIds { get; set; }
    }
}
