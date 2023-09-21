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
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        private readonly ILogger<LocationService> _logger;

        public LocationService(ILocationRepository locationRepository, ILogger<LocationService> logger)
        {
            _locationRepository = locationRepository;
            _logger = logger;
        }

        public async Task GetAllLocations()
        {
            try
            {
                await _locationRepository.GetAllLocations();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
        }

        public async Task GetLocationById(Guid Id)
        {
            try
            {
                await _locationRepository.GetLocationById(Id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
        }

        public async Task CreateLocation(Location location)
        {
            try
            {
                await _locationRepository.Insert(location);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
        }

        public async Task UpdateLocation(Location location)
        {
            try
            {
                await _locationRepository.Update(location);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
        }

        public async Task DeleteLocation(Location location)
        {
            try
            {
                await _locationRepository.Remove(location);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
        }
    }
}

