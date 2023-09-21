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
using Microsoft.Extensions.Logging;

namespace ServiceLayer.CustomServices
{
    public class RideService : IRideService
    {
        private readonly IRideRepository _rideRepository; //TODO: All Services inherit the repository interface not class

        private readonly ILogger<RideService> _logger;
        public RideService(IRideRepository rideRepository, ILogger<RideService> logger)
        {
            _rideRepository = rideRepository;
            _logger = logger;
        }

        public async Task GetAllRides()
        {
            try
            {
                await _rideRepository.GetAllRides();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task GetRideById(Guid Id)
        {
            try
            {
                await _rideRepository.GetRideById(Id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task CreateRide(Ride ride)
        {
            try
            {
                await _rideRepository.Insert(ride);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task UpdateRide(Ride ride)
        {
            try
            {
                await _rideRepository.Update(ride);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task CancelRide(Guid rideId)
        {
            try
            {
                await _rideRepository.CancelRide(rideId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }

        }

        public async Task GetRidesByUserId(Guid userId)
        {   
            try
            {
                await _rideRepository.GetUserRides(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }

        }

        public async Task GetRidesByDriverId(Guid driverId)
        {
            try
            {
                await _rideRepository.GetDriverRides(driverId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }

        }

    }
}