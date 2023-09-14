using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ICustomServices
{
    public interface IDriverService
    {
        Task<List<Driver>> GetAllDrivers();

        Task<Driver> GetDriverByID (Guid Id);
        
        Task CreateDriver (Driver driver);

        Task UpdateDriver (Driver driver);

        Task DeleteDriver (Driver driver);

        Task AssignVehicleToDriver (Guid driverId, Guid vehicleId);

        Task RemoveVehicleFromDriver (Guid driverId, Guid vehicleId);

        Task<List<Driver>> SearchDriver (string searchItem);

        Task<List<Vehicle>> GetDriverVehicles (Guid driverId);

        Task<List<Ride>> GetDriverRideHistory (Guid driverId);
    }
}