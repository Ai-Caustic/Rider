using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferLayer.DTOS
{
    public class UserDTO : BaseDTO
    {
        public required string Email { get; set; }

        public required string UserName { get; set; }

        [RegularExpression(@"^[0-9]{10}$")] // 10-digit phone number validation
        public required int PhoneNumber { get; set; }

        public required int IdNumber { get; set; }

        public string? ProfilePhotoUrl { get; set; }

        public DateOnly? BirthDate { get; set; }

        public string? Gender { get; set; }

        public required string IdPhotoUrl { get; set; }
    }
}
