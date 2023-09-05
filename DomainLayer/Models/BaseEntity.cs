using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DomainLayer.Enums;

namespace DomainLayer.Models
{
    public class BaseEntity 
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; } 

        public bool IsActive { get; set;}

        public Roles Role { get; set; }

    }
}
        

