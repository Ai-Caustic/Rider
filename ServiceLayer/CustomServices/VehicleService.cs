using DomainLayer.Models;
using ServiceLayer.ICustomServices;
using DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ServiceLayer.CustomServices
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        private readonly ILogger<VehicleService> _logger;

        public VehicleService(IVehicleRepository vehicleRepository, ILogger<VehicleService> logger)
        {
            _vehicleRepository = vehicleRepository;
            _logger = logger;
        }

        public async Task GetAllVehicles()
        {
            try
            {
                await _vehicleRepository.GetAllVehicles();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task GetVehicleById(Guid Id)
        {
            try
            {
                await _vehicleRepository.GetVehicleById(Id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task GetVehicleByPlate(string licensePlate)
        {
            try
            {
                await _vehicleRepository.SearchPlate(licensePlate);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task CreateVehicle(Vehicle vehicle)
        {
            try
            {
                await _vehicleRepository.Insert(vehicle);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task UpdateVehicle(Vehicle vehicle)
        {
            try
            {
                await _vehicleRepository.Update(vehicle);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task DeleteVehicle(Vehicle vehicle)
        {
            try
            {
                await _vehicleRepository.Remove(vehicle);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task SearchVehicles(string searchItem)
        {
            try
            {
                await _vehicleRepository.QueryVehicles(searchItem);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task GetVehicleDriver(Guid vehicleId)
        {
            try
            {
                await _vehicleRepository.GetVehicleDriver(vehicleId);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task GetVehicleRideHistory(Guid vehicleId)
        {
            try
            {
                await _vehicleRepository.GetVehicleRides(vehicleId);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }


    }
}