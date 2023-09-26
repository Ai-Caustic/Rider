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
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;

        private readonly ILogger<DriverService> _logger;


        public DriverService(IDriverRepository driverRepository, ILogger<DriverService> logger)
        {
            _driverRepository = driverRepository;
            _logger = logger;

        }

        public async Task GetAllDrivers()
        {
            try
            {
                await _driverRepository.GetAllDrivers();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task GetDriverById(Guid Id)
        {
            try
            {
                await _driverRepository.GetDriverById(Id);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task CreateDriver(Driver driver)
        {
            try
            {
                await _driverRepository.Insert(driver);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task UpdateDriver(Driver driver)
        {
            try
            {
                if (driver != null)
                {
                    await _driverRepository.Update(driver);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task DeleteDriver(Driver driver)
        {
            try
            {
                if (driver != null)
                {
                    await _driverRepository.Remove(driver);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }


        //start here
        public async Task AssignVehicleToDriver(Guid driverId, Guid vehicleId)
        {
            try
            {
                await _driverRepository.AssignVehicle(driverId, vehicleId); 
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task UnassignVehicleFromDriver(Guid driverId, Guid vehicleId)
        {
            try
            {
                await _driverRepository.UnassignVehicle(driverId, vehicleId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task SearchDriver(string searchItem)
        {
            try
            {
                await _driverRepository.Search(searchItem);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task GetDriverVehicles(Guid driverId)
        {
            try
            {
                await _driverRepository.GetDriverVehicles(driverId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}"); 
            }
        }

        public async Task GetDriverRideHistory(Guid driverId)
        {
            try
            {
                await _driverRepository.GetDriverRides(driverId);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");  
            }
        }

        public async Task StartRide(Guid driverId, Guid rideId, Guid vehicleId)
        {
            try
            {
                await _driverRepository.StartRide(driverId, rideId, vehicleId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task EndRide(Guid rideId)
        {
            try
            {
                await _driverRepository.EndRide(rideId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

    }
}