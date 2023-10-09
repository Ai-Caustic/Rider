using DataLayer.Data;
using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.ICustomServices;
using TransferLayer.DTOS;

namespace RiderApi.Controllers
{
    [Route("rideshare/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        private readonly ILogger<VehicleController> _logger;

        public VehicleController(IVehicleService vehicleService, ILogger<VehicleController> logger)
        {
            _vehicleService = vehicleService;
            _logger = logger;
        }

        [HttpGet(nameof(GetVehicleById))]
        public async Task<IActionResult> GetVehicleById(Guid Id)
        {
            try
            {
                var vehicle = await _vehicleService.GetVehicleById(Id);
                if(vehicle == null)
                {
                    _logger.LogError("Vehicle not found");
                    return NotFound(nameof(vehicle));
                }
                else
                {
                    _logger.LogInformation("Returned vehicle");
                    return Ok(vehicle);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpGet(nameof(GetAllVehicles))]
        public async Task<IActionResult> GetAllVehicles()
        {
            try
            {
                var vehicles = await _vehicleService.GetAllVehicles();
                if(vehicles == null)
                {
                    _logger.LogError("Could not find vehicles");
                    return NotFound(nameof(vehicles));
                }
                else
                {
                    _logger.LogInformation("All vehicles returned");
                    return Ok(vehicles);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpGet(nameof(GetVehicleByPlate))]
        public async Task<IActionResult> GetVehicleByPlate(string plate)
        {
            try
            {
                var vehicle = await _vehicleService.GetVehicleByPlate(plate);
                if(vehicle == null)
                {
                    _logger.LogError($"Vehicle with plate {plate} not found");
                    return NotFound($"Vehicle with plate {plate} not found");
                }
                else
                {
                    _logger.LogInformation($"Vehicle with plate {plate} returned");
                    return Ok(vehicle);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpGet(nameof(GetVehicleBySeats))]
        public async Task<IActionResult> GetVehicleBySeats(int seats)
        {
            try
            {
                var vehicles = await _vehicleService.GetVehicleBySeats(seats);
                if(vehicles == null)
                {
                    _logger.LogError($"Could not find vehicles with {seats} seats");
                    return NotFound(nameof(vehicles));
                }
                else
                {
                    _logger.LogInformation($"Found vehicles with {seats} seats");
                    return Ok(vehicles);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpPost(nameof(CreateVehicle))]
        public async Task<IActionResult> CreateVehicle(VehicleDTO vehicle)
        {
            try
            {
                if(vehicle == null)
                {
                    _logger.LogError("Invalid vehicle input");
                    return BadRequest();
                }
                else
                {
                    await _vehicleService.CreateVehicle(vehicle);
                    _logger.LogInformation("Created vehicle");
                    return Ok("Created user");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpPut(nameof(UpdateVehicle))]
        public async Task<IActionResult> UpdateVehicle(Guid Id, VehicleDTO updatedVehicle)
        {
            try
            {
                var vehicle = await _vehicleService.GetVehicleById(Id);
                if(vehicle == null)
                {
                    _logger.LogError("Cannot find vehicle");
                    return NotFound(nameof(vehicle));
                }
                else
                {
                    await _vehicleService.UpdateVehicle(Id, updatedVehicle);
                    _logger.LogInformation("Updated vehicle");
                    return Ok("Updated vehicle");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpDelete(nameof(DeleteVehicle))]
        public async Task<IActionResult>  DeleteVehicle(Guid Id)
        {
            try
            {
                var vehicle = await _vehicleService.GetVehicleById(Id);
                if(vehicle == null)
                {
                    _logger.LogError("Vehicle not found");
                    return NotFound(nameof(vehicle));
                }
                else
                {
                    await _vehicleService.DeleteVehicle(Id);
                    _logger.LogInformation("Deleted vehicle");
                    return Ok("Deleted vehicle");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpGet(nameof(GetVehicleDriver))]
        public async Task<IActionResult> GetVehicleDriver(Guid vehicleId)
        {
            try
            {
                var vehicle = await _vehicleService.GetVehicleById(vehicleId);
                if(vehicle == null)
                {
                    _logger.LogError("Vehicle not found");
                    return NotFound(nameof(vehicle));
                }
                else
                {
                    var driver = await _vehicleService.GetVehicleDriver(vehicleId);
                    if(driver == null)
                    {
                        _logger.LogError("Driver not found");
                        return NotFound(nameof(driver));
                    }
                    else
                    {
                        _logger.LogInformation("Returned vehicle driver");
                        return Ok(driver);
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpGet(nameof(GetVehicleRides))]
        public async Task<IActionResult> GetVehicleRides(Guid vehicleId)
        {
            try
            {
                var vehicle = await _vehicleService.GetVehicleById(vehicleId);
                if(vehicle == null)
                {
                    _logger.LogError("Could not find vehicle");
                    return NotFound(nameof(vehicle));
                }
                else
                {
                    var rides = await _vehicleService.GetVehicleRideHistory(vehicleId);
                    if(rides == null)
                    {
                        _logger.LogError("Vehicle rides not found");
                        return NotFound(nameof(rides));
                    }
                    else
                    {
                        _logger.LogInformation("Returned vehicle ride history");
                        return Ok(rides);
                    }
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
