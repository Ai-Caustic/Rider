using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferLayer.DTOS;

namespace ServiceLayer.ICustomServices
{
    public interface IVehicleService
    {
        Task<List<VehicleDTO>> GetAllVehicles();

        Task<VehicleDTO> GetVehicleById(Guid id);

        Task<VehicleDTO> GetVehicleByPlate(string licensePlate);

        Task<bool> CreateVehicle(VehicleDTO vehicleDTO);

        Task<bool> UpdateVehicle(Guid vehicleId, VehicleDTO updatedVehicle);

        Task<bool> DeleteVehicle(Guid vehicleId);

        Task<List<VehicleDTO>> GetVehicleBySeats(int numberOfSeats);

        Task<DriverDTO> GetVehicleDriver(Guid vehicleId);

        //TODO: Add RideDTO 
        Task<List<RideDTO>> GetVehicleRideHistory(Guid vehicleId);
    }
}