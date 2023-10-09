using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferLayer.DTOS
{
    public class DriverDTO 
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }  

        public string Email { get; set; }

        [RegularExpression(@"^([\+]?2547[-]?|[0])?[1-9][0-9]{8}$")]
        public string Mobile { get; set; }
        
        public string DriverPhotoUrl { get; set; }

        public string LicensePhotoUrl { get; set; }

        public string VerificationBadgeUrl { get; set; }

         // Include DTOs or Id's for related entity 
        public List<Guid> VehicleIds { get; set; } //TODO: Change this to a DTO

        public List<Guid> RideIds { get; set; }
    }
}
