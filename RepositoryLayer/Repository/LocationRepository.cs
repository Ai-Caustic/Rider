using DataLayer.Data;
using DomainLayer.IRepository;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using TransferLayer.DTOS;

namespace RepositoryLayer.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;

        private readonly ILogger<LocationRepository> _logger;

        public LocationRepository(ApplicationDbContext context, IMapper mapper, ILogger<LocationRepository> logger)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<Location>> GetAllLocations()
        {
            return await _context.Locations.AsNoTracking().ToListAsync();
        }

        public async Task<Location> GetLocationById(Guid Id)
        {
            return await _context.Locations.SingleOrDefaultAsync(l => l.Id == Id);
        }

        public async Task Insert(Location location)
        {
            try
            {
                if(location != null)
                {
                    bool locationExists = await _context.Locations.AnyAsync(l => l.Id == location.Id);
                    if(locationExists)
                    {
                        _logger.LogError("Location already exists");
                        return;
                    }
                    else
                    {
                        Location newLocation = Location.Create(location.Name, location.Latitude, location.Longitude, DateTime.Now, true);
                        await _context.Locations.AddAsync(newLocation);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    _logger.LogError("Location is null");
                }
            } 
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
        }

        public async Task Update(Guid locationId, Location updatedLocation)
        {
            try
            {
                var location = await _context.Locations.SingleOrDefaultAsync(l => l.Id == locationId);
                if(location != null)
                {
                    location.Id = location.Id;
                    location.Name = updatedLocation.Name;
                    location.Latitude = updatedLocation.Latitude;
                    location.Longitude = updatedLocation.Longitude;
                    location.IsActive = updatedLocation.IsActive;
                    location.UpdatedAt = DateTime.Now;

                    await _context.SaveChangesAsync();
                }
                else
                {
                    _logger.LogError("Location not found");
                }

            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
        }

        public async Task Remove(Guid locationId)
        {
            try
            {
                var location = await _context.Locations.SingleOrDefaultAsync(l => l.Id == locationId);
                if(location != null)
                {
                    _context.Locations.Remove(location);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _logger.LogError("Location not found");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
        }

        public Location MapLocationDTO(LocationDTO locationDTO)
        {
            return _mapper.Map<Location>(locationDTO);
        }
    }
}
