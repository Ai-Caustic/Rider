using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using TransferLayer.DTOS;

namespace DomainLayer.IRepository
{
    public interface IRideRepository
    {
        Task<List<Ride>> GetAllRides();

        Task<Ride> GetRideById(Guid Id);

        Task Insert(Ride ride);

        Task Update(Guid rideId, Ride updatedRide);

        Task Remove(Guid rideId);

        Task CancelRide(Guid rideId);

        Ride MapRideDTO(RideDTO rideDTO);

        Task<List<Ride>> GetDriverRides(Guid driverId);

        Task<List<Ride>> GetUserRides(Guid userId); 
    }
}
