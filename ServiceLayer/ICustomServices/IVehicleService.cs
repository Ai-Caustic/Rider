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
        Task GetAllVehicles();

        Task GetVehicleById(Guid id);

        Task GetVehicleByPlate(string licensePlate);

        Task CreateVehicle(Vehicle vehicle);

        Task UpdateVehicle(Vehicle vehicle);

        Task DeleteVehicle(Vehicle vehicle);

        Task SearchVehicles(string searchItem);

        Task GetVehicleDriver(Guid vehicleId);

        Task GetVehicleRideHistory(Guid vehicleId);
    }
}