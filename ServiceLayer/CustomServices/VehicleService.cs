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
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        private readonly ILogger<VehicleService> _logger;

        private readonly IMapper _mapper;

        public VehicleService(IVehicleRepository vehicleRepository, ILogger<VehicleService> logger, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<VehicleDTO>> GetAllVehicles()
        {
            try
            {
               var vehicles = await _vehicleRepository.GetAllVehicles();

               if(vehicles == null)
               {
                    _logger.LogError("Could not get vehicles");
               }
               else
               {
                    var result = _mapper.Map<List<VehicleDTO>>(vehicles);

                    return result;
               }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
            return null;
        }

        public async Task<VehicleDTO> GetVehicleById(Guid Id)
        {
            try
            {
                var driver = await _vehicleRepository.GetVehicleById(Id);
                if(driver == null)
                {
                    _logger.LogError($"Cannot find driver {Id}");
                }
                else
                {
                    var result = _mapper.Map<VehicleDTO>(driver);
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
            return null;
        }

        public async Task<VehicleDTO> GetVehicleByPlate(string licensePlate)
        {
            try
            {
                var vehicle = await _vehicleRepository.SearchPlate(licensePlate);
                if(vehicle == null)
                {
                    _logger.LogError($"Could not find vehicle with license {licensePlate}");
                }
                else
                {
                    var result = _mapper.Map<VehicleDTO>(vehicle);
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
            return null;
        }

        public async Task<bool> CreateVehicle(VehicleDTO vehicleDTO)
        {
            try
            {
                if (vehicleDTO == null)
                {
                    _logger.LogError("Vehicle cannot be null");
                    return false;
                }
                else
                {
                    var vehicle = _vehicleRepository.MapVehicleDTO(vehicleDTO);
                    await _vehicleRepository.Insert(vehicle);
                    _logger.LogInformation("Vehicle added successfully");
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
                return false;
            }
        }

        public async Task<bool> UpdateVehicle(Guid vehicleId, VehicleDTO updatedVehicle)
        {
            try
            {
                var vehicle = await _vehicleRepository.GetVehicleById(vehicleId);
                if(vehicle == null)
                {
                    _logger.LogError($"Cannot find vehicle {vehicleId}");
                    return false;
                }
                else
                {
                    var mappedVehicle = _vehicleRepository.MapVehicleDTO(updatedVehicle);
                    await _vehicleRepository.Update(vehicleId, mappedVehicle);
                    _logger.LogInformation($"Updated vehicle {vehicleId}");
                    return true;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
                return false;
            }
        }
        public async Task<bool> DeleteVehicle(Guid vehicleId)
        {
            try
            {
                var vehicle = await _vehicleRepository.GetVehicleById(vehicleId);
                if (vehicle == null)
                {
                    _logger.LogError("Vehicle not found");
                    return false;
                }
                else
                {
                    await _vehicleRepository.Remove(vehicleId);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
                return false;
            }
        }

        public async Task<List<VehicleDTO>> GetVehicleBySeats(int numberOfSeats)
        {
            try
            {
                var results = await _vehicleRepository.SearchVehicleBySeats(numberOfSeats);
                if(results == null)
                {
                    _logger.LogError($"Could not find vehicles with {numberOfSeats} seats");
                }
                else
                {
                    var result = _mapper.Map<List<VehicleDTO>>(results);
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
            return null;
        }


        public async Task<DriverDTO> GetVehicleDriver(Guid vehicleId)
        {
            try
            {
                var vehicle = await _vehicleRepository.GetVehicleById(vehicleId);
                if(vehicle == null)
                {
                    _logger.LogError($"Cannot find vehicle {vehicleId}");
                }
                else
                {
                    var driver = await _vehicleRepository.GetVehicleDriver(vehicleId);
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

        //TODO : Add Ride DTO
        public async Task<List<RideDTO>> GetVehicleRideHistory(Guid vehicleId)
        {
            try
            {
                var rides = await _vehicleRepository.GetVehicleRides(vehicleId);

                var mappedRides = _mapper.Map<List<RideDTO>>(rides);
                return mappedRides;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
            return null;
        }
    }
}