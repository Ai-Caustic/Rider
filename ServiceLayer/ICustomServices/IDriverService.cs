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
        Task GetAllDrivers();

        Task GetDriverById (Guid Id); 
        
        Task CreateDriver (Driver driver);

        Task UpdateDriver (Driver driver);

        Task DeleteDriver (Driver driver);

        Task AssignVehicleToDriver (Guid driverId, Guid vehicleId);

        Task UnassignVehicleFromDriver (Guid driverId, Guid vehicleId);

        Task SearchDriver (string searchItem); 

        Task GetDriverVehicles (Guid driverId); 

        Task GetDriverRideHistory (Guid driverId); 

        Task StartRide (Guid driverId, Guid rideId, Guid vehicleId);

        Task EndRide (Guid rideId);
    }
}