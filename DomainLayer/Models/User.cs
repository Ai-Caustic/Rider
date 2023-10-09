using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using DomainLayer.Enums;

namespace DomainLayer.Models
{
    public class User : BaseEntity
    {
        public string Email { get; set; }

        public string UserName { get; set; }

        [RegularExpression(@"^([\+]?2547[-]?|[0])?[1-9][0-9]{8}$")]
        public string Mobile{ get; set; } 

        public int IdNumber { get; set; }

        public string ? ProfilePhotoUrl { get; set; }

        public DateOnly ? BirthDate { get; set; } //Change this to DateOnlyVO

        public string Gender { get; set; }

        public string IdPhotoUrl { get; set; }

        public Roles Role { get; set; } 


        public ICollection<Ride> Rides { get; set; } = new List<Ride>();

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

        //Empty Constructor
        public User(){}

        //Custom User methods
        public static User Create(string email, string userName, string mobile, int idNumber, string idPhotoUrl, string ? profilePhotoUrl , DateOnly ? birthDate, Roles role, string gender, bool IsActive)
        {
            var user = new User
            {
                Email = email,
                UserName = userName,
                Mobile = mobile,
                IdNumber = idNumber,
                IdPhotoUrl = idPhotoUrl,
                ProfilePhotoUrl = profilePhotoUrl,
                Gender = gender,
                BirthDate = birthDate,
                Role = Roles.Passenger,
                IsActive = true
            };

            user.GenerateNewIdentity();
            return user;
        }

        public void UpdateProfile(string profilePhotoUrl, DateOnly birthDate, string gender)
        {
            ProfilePhotoUrl = profilePhotoUrl;
            BirthDate = birthDate;
            Gender = gender;
        }
    
    }
}