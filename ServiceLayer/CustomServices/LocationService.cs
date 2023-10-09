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
using TransferLayer.DTOS;
using AutoMapper;

namespace ServiceLayer.CustomServices
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        private readonly ILogger<LocationService> _logger;

        private readonly IMapper _mapper;

        public LocationService(ILocationRepository locationRepository, ILogger<LocationService> logger, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<LocationDTO>> GetAllLocations()
        {
            try
            {
                var locations = await _locationRepository.GetAllLocations();
                
                var result = _mapper.Map<List<LocationDTO>>(locations);

                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return null;
        }

        public async Task<LocationDTO> GetLocationById(Guid Id)
        {
            try
            {
                var location = await _locationRepository.GetLocationById(Id);
                if(location == null)
                {
                    _logger.LogError("Could not find location");
                }
                else
                {
                    var result = _mapper.Map<LocationDTO>(location);

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return null;
        }

        public async Task<bool> CreateLocation(LocationDTO locationDTO)
        {
            try
            {
               if(locationDTO == null)
               {
                    _logger.LogError("Location cannot be null");
                    return false;
               }
               else
               {
                    var location = _locationRepository.MapLocationDTO(locationDTO);
                    await _locationRepository.Insert(location);
                    return true;
               }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
                return false;
            }
        }

        public async Task<bool> UpdateLocation(Guid locationId, LocationDTO Updatedocation)
        {
            try
            {
               var location = await _locationRepository.GetLocationById(locationId);
               if (location == null)
               {
                    _logger.LogError("Location not found");
                    return false;
               }
               else
               {
                    var mappedLocation = _locationRepository.MapLocationDTO(Updatedocation);
                    await _locationRepository.Update(locationId, mappedLocation);
                    _logger.LogInformation("Updated location");
                    return true;
               }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
                return false;
            }
        }

        public async Task<bool> DeleteLocation(Guid locationId)
        {
            try
            {
                var location = await _locationRepository.GetLocationById(locationId);
                if(location == null)
                {
                    _logger.LogError("Location not found");
                    return false;
                }
                else
                {
                    await _locationRepository.Remove(locationId);
                    _logger.LogInformation("Deleted location");
                    return true;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
                return false;
            }
        }
    }
}

