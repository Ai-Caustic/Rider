using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferLayer.DTOS;

namespace ServiceLayer.ICustomServices
{
    public interface IRideService
    {
        Task<List<RideDTO>> GetAllRides();

        Task<RideDTO> GetRideById(Guid Id);

        Task<bool> CreateRide(RideDTO rideDTO);

        Task<bool> UpdateRide(Guid rideId, RideDTO rideDTO);

        Task<bool> DeleteRide(Guid rideId);

        //Task CompleteRide(Guid rideId);

        Task<bool> CancelRide(Guid rideId);

        Task<List<RideDTO>> GetRidesByUserId(Guid userId);

        Task<List<RideDTO>> GetRidesByDriverId(Guid driverId);


        //Task CalculateRideFare(double distance, double fare);


    }
}
