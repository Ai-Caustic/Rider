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
using AutoMapper;
using TransferLayer.DTOS;

namespace ServiceLayer.CustomServices
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;

        private readonly ILogger<DriverService> _logger;

        private readonly IMapper _mapper;


        public DriverService(IDriverRepository driverRepository, ILogger<DriverService> logger, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _logger = logger;
            _mapper = mapper;

        }

        public async Task<List<DriverDTO>> GetAllDrivers()
        {
            try
            {
                var drivers = await _driverRepository.GetAllDrivers();

                var result = _mapper.Map<List<DriverDTO>>(drivers);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
            return null;
        }

        public async Task<DriverDTO> GetDriverById(Guid Id)
        {
            try
            {
                var driver = await _driverRepository.GetDriverById(Id);
                if (driver == null)
                {
                    _logger.LogError("Could not find driver");
                }
                else
                {
                    var result = _mapper.Map<DriverDTO>(driver);
                    return result; 
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
            return null;
        }

        public async Task<bool> CreateDriver(DriverDTO driverDTO)
        {
            try
            {
                if(driverDTO == null)
                {
                    _logger.LogError("Driver cannot be null");
                    return false;
                }
                else
                {
                    var driver = _driverRepository.MapDriverDTO(driverDTO);
                    await _driverRepository.Insert(driver);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
                return false;
            }
        }

        public async Task<bool> UpdateDriver(Guid driverId, DriverDTO driverDTO)
        {
            try
            {
                var driver = await _driverRepository.GetDriverById(driverId);
                if (driver == null)
                {
                    _logger.LogError("Driver not found");
                    return false;
                }
                else
                {
                    var mappedDriver = _driverRepository.MapDriverDTO(driverDTO);
                    await _driverRepository.Update(driverId, mappedDriver);
                    _logger.LogInformation("Updated driver");
                    return true;
                }
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
                return false;
            }
        }

        public async Task<bool> DeleteDriver(Guid driverId)
        {
            try
            {
                var driver = await _driverRepository.GetDriverById(driverId);
                if (driver == null)
                {
                    _logger.LogError("Driver not found");
                    return false;
                }
                else
                {
                    await _driverRepository.Remove(driverId);
                    _logger.LogInformation("Deleted location");
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
                return false;
            }
        }

        public async Task<bool> AssignVehicleToDriver(Guid driverId, Guid vehicleId)
        {
            try
            {
                await _driverRepository.AssignVehicle(driverId, vehicleId);
                _logger.LogInformation($"Assigned vehicle {vehicleId} to driver {driverId}"); 
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
                return false;
            }
        }

        public async Task<bool> UnassignVehicleFromDriver(Guid driverId, Guid vehicleId)
        {
            try
            {
                await _driverRepository.UnassignVehicle(driverId, vehicleId);
                _logger.LogInformation($"Unassigned vehicle {vehicleId} from driver {driverId}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
                return false;
            }
        }

        public async Task<List<DriverDTO>> SearchDrivers(string searchItem)
        {
            try
            {
                var results = await _driverRepository.Search(searchItem);
                if(results == null)
                {
                    _logger.LogError($"Could not find results with {searchItem}");
                }
                else
                {
                    var result = _mapper.Map<List<DriverDTO>>(results);
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
            return null;
        }

        public async Task<List<VehicleDTO>> GetDriverVehicles(Guid driverId)
        {
            try
            {
                var vehicles = await _driverRepository.GetDriverVehicles(driverId);
                if(vehicles == null)
                {
                    _logger.LogError($"No vehicles found for driver {driverId}");
                }
                else
                {
                    var mappedVehicles = _mapper.Map<List<VehicleDTO>>(vehicles);

                    return mappedVehicles;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}"); 
            }
            return null;
        }
    
        public async Task<List<RideDTO>> GetDriverRideHistory(Guid driverId)
        {
            try
            {
                var rides = await _driverRepository.GetDriverRides(driverId);
                if (rides == null)
                {
                    _logger.LogError($"No rides found for driver {driverId}");
                }
                else
                {
                    var mappedRides = _mapper.Map<List<RideDTO>>(rides);

                    return mappedRides;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");  
            }
            return null;
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