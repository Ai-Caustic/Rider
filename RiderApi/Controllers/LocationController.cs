using DataLayer.Data;
using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.ICustomServices;
using TransferLayer.DTOS;

namespace RiderApi.AddControllers
{
    [Route("rideshare/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        private readonly ILogger<LocationController> _logger;

        public LocationController(ILocationService locationService, ILogger<LocationController> logger)
        {
            _locationService = locationService;
            _logger = logger;
        }

        [HttpGet(nameof(GetAllLocations))]
        public async Task<IActionResult> GetAllLocations()
        {
            try
            {
                var locations = await _locationService.GetAllLocations();
                if(locations != null)
                {
                    _logger.LogInformation("Returned all locations");
                    return Ok(locations);
                }
                else
                {
                    return NotFound("Locations");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpGet(nameof(GetLocationById))]
        public async Task<IActionResult> GetLocationById(Guid Id)
        {
            try
            {
                var location = await _locationService.GetLocationById(Id);
                if(location != null)
                {
                    _logger.LogInformation("Returned location");
                    return Ok(location);
                }
                else
                {
                    return NotFound(location);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpPost(nameof(CreateLocation))]
        public async Task<IActionResult> CreateLocation(LocationDTO location)
        {
            try
            {
                if(location == null)
                {
                    _logger.LogError("Invalid location input");
                    return BadRequest();
                }
                else
                {
                    await _locationService.CreateLocation(location);
                    _logger.LogInformation("Created location");
                    return Ok("Created location");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpPut(nameof(UpdateLocation))]
        public async Task<IActionResult> UpdateLocation(Guid Id, LocationDTO updatedLocation)
        {
            try
            {
                var location = await _locationService.GetLocationById(Id);
                if(location == null)
                {
                    _logger.LogError("Location not found");
                    return NotFound("Loction");
                }
                else
                {
                    await _locationService.UpdateLocation(Id, updatedLocation);
                    _logger.LogInformation("Updated location");
                    return Ok("Updated location");
                }
            }
            catch(DbUpdateException ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpDelete(nameof(DeleteLocation))]
        public async Task<IActionResult> DeleteLocation(Guid Id)
        {
            try
            {
                var location = await _locationService.GetLocationById(Id);
                if(location == null)
                {
                    _logger.LogError("Could not find location");
                    return NotFound("Locatiom");
                }
                else
                {
                    await _locationService.DeleteLocation(Id);
                    _logger.LogInformation("Deleted location");
                    return Ok("Deleted location");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }
}
}