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
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;

        private readonly IVehicleService _vehicleService;

        private readonly IRideService _rideService;

        private readonly ILogger<DriverController> _logger;

        public DriverController(IDriverService driverService, ILogger<DriverController> logger, IVehicleService vehicleService, IRideService rideService)
        {
            _driverService = driverService;
            _logger = logger;
            _vehicleService = vehicleService;
            _rideService = rideService;
        }

        [HttpGet(nameof(GetDriverById))]
        public async Task<IActionResult> GetDriverById(Guid Id)
        {
            try
            {
                var driver = await _driverService.GetDriverById(Id);
                if (driver != null)
                {
                    _logger.LogInformation("Returned driver");
                    return Ok(driver);
                }
                else
                {
                    return NotFound();
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpGet(nameof(GetAllDrivers))]
        public async Task<IActionResult> GetAllDrivers()
        {
            try
            {
                var drivers = await _driverService.GetAllDrivers();
                if (drivers != null)
                {
                    _logger.LogInformation("Returned all drivers");
                    return Ok(drivers);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpPost(nameof(CreateDriver))]
        public async Task<IActionResult> CreateDriver(DriverDTO driver)
        {
            try
            {
                if(driver == null)
                {
                    _logger.LogError("Invalid driver input");
                    return BadRequest();
                }
                else
                {
                    await _driverService.CreateDriver(driver);
                    _logger.LogInformation("Created driver");
                    return Ok("Created driver");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpPut(nameof(UpdateDriver))]
        public async Task<IActionResult> UpdateDriver(Guid Id, DriverDTO updatedDriver)
        {
            try
            {
                var driver = await _driverService.GetDriverById(Id);
                if (driver == null)
                {
                    _logger.LogError($"Could not find driver {Id}");
                    return NotFound("Driver not found");
                }
                else
                {
                    await _driverService.UpdateDriver(Id, updatedDriver);
                    _logger.LogInformation($"Updated driver {Id}");
                    return Ok("Updated driver");
                }   
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpDelete(nameof(DeleteDriver))]
        public async Task<IActionResult> DeleteDriver(Guid Id)
        {
            try
            {
                var driver = await _driverService.GetDriverById(Id);
                if(driver == null)
                {
                    _logger.LogError($"Could not find driver {Id}");
                    return NotFound("Driver not found");
                }
                else
                {
                    await _driverService.DeleteDriver(Id);
                    _logger.LogInformation($"Deleted driver {Id}");
                    return Ok("Deleted driver");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpPost(nameof(AssignVehicleToDriver))]
        public async Task<IActionResult> AssignVehicleToDriver(Guid driverId, Guid vehicleId)
        {
            try
            {
                var driver = await _driverService.GetDriverById(driverId);
                var vehicle = await _vehicleService.GetVehicleById(vehicleId);

                if (driver != null && vehicle != null)
                {
                    await _driverService.AssignVehicleToDriver(driverId, vehicleId);
                    _logger.LogInformation($"Assigning vehicle {vehicleId} to driver {driverId}");
                    return Ok("Assigned vehicle to driver");
                }
                else
                {
                    _logger.LogWarning("Could not find vehicle or driver");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpPost(nameof(UnnassignVehicleFromDriver))]
        public async Task<IActionResult> UnnassignVehicleFromDriver(Guid driverId, Guid vehicleId)
        {
            try
            {
                var driver = await _driverService.GetDriverById(driverId);
                var vehicle = await _vehicleService.GetVehicleById(vehicleId);

                if (driver != null && vehicle != null)
                {
                    await _driverService.UnassignVehicleFromDriver(driverId, vehicleId);
                    _logger.LogInformation($"Unnaseigned vehicle {vehicleId} from driver {driverId}");
                    return Ok("Unnaseigned vehicle from driver");
                }
                else
                {
                    _logger.LogWarning("Could not find vehicle or driver");
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpGet(nameof(SearchDriver))]
        public async Task<IActionResult> SearchDriver(string query)
        {
            try
            {
                var result = await _driverService.SearchDrivers(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpGet(nameof(GetDriverVehicles))]
        public async Task<IActionResult> GetDriverVehicles(Guid driverId)
        {
            try
            {
                var driver = await _driverService.GetDriverById(driverId);

                if (driver != null)
                {
                    var vehicles = await _driverService.GetDriverVehicles(driverId);
                    _logger.LogInformation($"Returned driver {driverId} vehicles");
                    return Ok(vehicles);
                }
                else
                {
                    _logger.LogWarning("Could not find driver");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpGet(nameof(GetDriverRideHistory))]
        public async Task<IActionResult> GetDriverRideHistory(Guid driverId)
        {
            try
            {
                var driver = await _driverService.GetDriverById(driverId);

                if (driver != null)
                {
                    var rides = await _driverService.GetDriverRideHistory(driverId);
                    _logger.LogInformation($"Returned driver {driverId} ride history");
                    return Ok(rides);
                }
                else
                {
                    _logger.LogWarning("Could not find driver");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpPost(nameof(StartRide))]
        public async Task<IActionResult> StartRide(Guid driverId, Guid rideId, Guid vehicleId)
        {
            try
            {
                var driver = await _driverService.GetDriverById(driverId);
                var ride = _rideService.GetRideById(rideId);
                var vehicle = _vehicleService.GetVehicleById(vehicleId);

                if (driver != null && ride != null &&vehicle != null)
                {
                    await _driverService.StartRide(driverId, rideId, vehicleId);
                    _logger.LogInformation($"Started ride {rideId}");
                    return Ok("Started ride");
                }
                else
                {
                    _logger.LogWarning("Could not find driver, ride or vehicle");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpPost(nameof(EndRide))]
        public async Task<IActionResult> EndRide(Guid rideId)
        {
            try
            {
                var ride = _rideService.GetRideById(rideId);
                
                if(ride != null)
                {
                    await _driverService.EndRide(rideId);
                    _logger.LogInformation($"Ended ride {rideId}");
                    return Ok("Ended ride");
                }
                else
                {
                    _logger.LogWarning($"Could not find ride {rideId}");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }
    }
}
