using DomainLayer.Models;
using TransferLayer.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ICustomServices
{
    public interface IDriverService
    {
        Task<List<DriverDTO>> GetAllDrivers();

        Task<DriverDTO> GetDriverById (Guid Id); 
        
        Task<bool> CreateDriver (DriverDTO driverDTO);

        Task<bool> UpdateDriver (Guid driverId, DriverDTO driverDTO);

        Task<bool> DeleteDriver (Guid driverId);

        Task<bool> AssignVehicleToDriver (Guid driverId, Guid vehicleId);

        Task<bool> UnassignVehicleFromDriver (Guid driverId, Guid vehicleId);

        Task <List<DriverDTO>> SearchDrivers (string searchItem); 

        Task<List<VehicleDTO>> GetDriverVehicles (Guid driverId); 

        Task<List<RideDTO>> GetDriverRideHistory (Guid driverId); 

        Task StartRide (Guid driverId, Guid rideId, Guid vehicleId);

        Task EndRide (Guid rideId);
    }
}