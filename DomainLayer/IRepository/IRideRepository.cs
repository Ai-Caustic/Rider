using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DomainLayer.IRepository
{
    public interface IRideRepository
    {
        Task<List<Ride>> GetAllRides();

        Task<Ride> GetRideById(Guid Id);

        Task Insert(Ride ride);

        Task Update(Ride ride);

        Task Remove(Ride ride);

        Task CancelRide(Guid rideId);

        Task<List<Ride>> GetDriverRides(Guid driverId);

        Task<List<Ride>> GetUserRides(Guid userId); 
    }
}
