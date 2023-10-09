using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferLayer.DTOS;

namespace DomainLayer.IRepository
{
    public interface IVehicleRepository
    {
        Task<List<Vehicle>> GetAllVehicles();

        Task<Vehicle> GetVehicleById(Guid Id);

        Task Insert(Vehicle vehicle);

        Task Update(Guid vehicleId, Vehicle updatedVehicle);

        Task Remove(Guid vehicleId);

        Vehicle MapVehicleDTO(VehicleDTO vehicleDTO);

        Task<Vehicle> SearchPlate(string plateNo);

        Task<List<Vehicle>> QueryVehicles(string query);

        Task<List<Vehicle>> SearchVehicleBySeats(int seatsNo);

        Task<Driver> GetVehicleDriver(Guid driverId);

        Task<List<Ride>> GetVehicleRides(Guid vehicleId);

    }
}
