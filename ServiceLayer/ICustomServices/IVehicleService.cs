using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ICustomServices
{
    public interface IVehicleService
    {
        Task<List<Vehicle>> GetAllVehicles();

        Task<Vehicle> GetVehicleById(Guid id);

        Task<Vehicle> GetVehicleByPlate(string licensePlate);

        Task CreateVehicle(Vehicle vehicle);

        Task UpdateVehicle(Vehicle vehicle);

        Task DeleteVehicle(Vehicle vehicle);

        Task<List<Vehicle>> SearchVehicles(string searchItem);

        Task<List<Driver>> GetVehicleDriver(Guid vehicleId);

        Task<List<Ride>> GetVehicleRideHistory(Guid vehicleId);
    }
}