using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ICustomServices
{
    public interface IRideService
    {
        Task GetAllRides();

        Task GetRideById(Guid Id);

        Task CreateRide(Ride ride);

        Task UpdateRide(Ride ride);

        //Task DeleteRide(Ride ride);

        //Task CompleteRide(Guid rideId);

        Task CancelRide(Guid rideId);

        Task GetRidesByUserId(Guid userId);

        Task GetRidesByDriverId(Guid driverId);


        //Task CalculateRideFare(double distance, double fare);


    }
}
