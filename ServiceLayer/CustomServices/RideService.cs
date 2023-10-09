using DomainLayer.Models;
using ServiceLayer.ICustomServices;
using DomainLayer.Enums;
using DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.IRepository;
using TransferLayer.DTOS;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace ServiceLayer.CustomServices
{
    public class RideService : IRideService
    {
        private readonly IRideRepository _rideRepository;

        private readonly ILogger<RideService> _logger;

        private readonly IMapper _mapper;
        public RideService(IRideRepository rideRepository, ILogger<RideService> logger, IMapper mapper)
        {
            _rideRepository = rideRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<RideDTO>> GetAllRides()
        {
            try
            {
                var rides = await _rideRepository.GetAllRides();
                if(rides == null)
                {
                    _logger.LogError($"Could not get rides");
                }
                else
                {
                    var result = _mapper.Map<List<RideDTO>>(rides);
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
            return null;
        }

        public async Task<RideDTO> GetRideById(Guid Id)
        {
            try
            {
                var ride = await _rideRepository.GetRideById(Id);
                if (ride == null)
                {
                    _logger.LogError("Could not find ride");
                } 
                else
                {
                    var result = _mapper.Map<RideDTO>(ride);
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
            return null;
        }

        public async Task<bool> CreateRide(RideDTO rideDTO)
        {
            try
            {
                if(rideDTO == null)
                {
                    _logger.LogError("Ride cannot be null");
                    return false;
                }
                else
                {
                    var ride = _rideRepository.MapRideDTO(rideDTO);
                    await _rideRepository.Insert(ride);
                    return true;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
                return false;
            }
        }

        public async Task<bool> UpdateRide(Guid rideId, RideDTO rideDTO)
        {
            try
            {
                var ride = await _rideRepository.GetRideById(rideId);
                if(ride == null)
                {
                    _logger.LogError("Ride not found");
                    return false;
                }
                else
                {
                    var mappedRide = _rideRepository.MapRideDTO(rideDTO);
                    await _rideRepository.Update(rideId, mappedRide);
                    _logger.LogInformation("Updated ride");
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
                return false;
            }
        }

        public async Task<bool> DeleteRide(Guid rideId)
        {
            try
            {
                var ride = await _rideRepository.GetRideById(rideId);
                if(ride == null)
                {
                    _logger.LogError("User not found");
                    return false;
                }
                else
                {
                    await _rideRepository.Remove(rideId);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
                return false;
            }
        }

        public async Task<bool> CancelRide(Guid rideId)
        {
            try
            {
                var ride = await _rideRepository.GetRideById(rideId);
                if (ride == null)
                {
                    _logger.LogError("Ride not found");
                    return false;
                }
                else
                {
                    await _rideRepository.CancelRide(rideId);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
                return false;
            }

        }

        public async Task<List<RideDTO>> GetRidesByUserId(Guid userId)
        {   
            try
            {
                var rides = await _rideRepository.GetUserRides(userId);
                if(rides == null)
                {
                    _logger.LogError($"Could not find rides for user {userId}");
                    return null;
                }
                else
                {
                    var result = _mapper.Map<List<RideDTO>>(rides);
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
            return null;
        }

        public async Task<List<RideDTO>> GetRidesByDriverId(Guid driverId)
        {
            try
            {
                var rides = await _rideRepository.GetDriverRides(driverId);
                if(rides == null)
                {
                    _logger.LogError($"Could not find rides for driver{driverId}");
                    return null;
                }
                else
                {
                    var result = _mapper.Map<List<RideDTO>>(rides);
                    return result;
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
            return null;
        }
    }
}