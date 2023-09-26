using DataLayer.Data;
using DomainLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.ICustomServices;

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
        public IActionResult GetDriverById(Guid Id)
        {
            try
            {
                var driver = _driverService.GetDriverById(Id);
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
        public IActionResult GetAllDrivers()
        {
            try
            {
                var drivers = _driverService.GetAllDrivers();
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
        public IActionResult CreateDriver(Driver driver)
        {
            try
            {
                if(driver != null)
                {
                    _driverService.CreateDriver(driver);
                    _logger.LogInformation("Created driver");
                    return Ok("Created Driver");
                }
                else
                {
                    _logger.LogWarning("Driver is null");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpPut(nameof(UpdateDriver))]
        public IActionResult UpdateDriver(Driver driver)
        {
            try
            {
                if(driver != null)
                {
                    _driverService.UpdateDriver(driver);
                    _logger.LogError("Driver updated successfully");
                    return Ok();
                }
                else
                {
                    _logger.LogWarning("Driver not null");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpDelete(nameof(DeleteDriver))]
        public IActionResult DeleteDriver(Driver driver)
        {
            try
            {
                if(driver != null)
                {
                    _driverService.DeleteDriver(driver);
                    _logger.LogInformation("Driver deleted successfully");
                    return Ok("Deleted driver");
                }
                else
                {
                    _logger.LogWarning("Could not delete driver");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpPost(nameof(AssignVehicleToDriver))]
        public IActionResult AssignVehicleToDriver(Guid driverId, Guid vehicleId)
        {
            try
            {
                var driver = _driverService.GetDriverById(driverId);
                var vehicle = _vehicleService.GetVehicleById(vehicleId);

                if (driver != null && vehicle != null)
                {
                    _driverService.AssignVehicleToDriver(driverId, vehicleId);
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
        public IActionResult UnnassignVehicleFromDriver(Guid driverId, Guid vehicleId)
        {
            try
            {
                var driver = _driverService.GetDriverById(driverId);
                var vehicle = _vehicleService.GetVehicleById(vehicleId);

                if (driver != null && vehicle != null)
                {
                    _driverService.UnassignVehicleFromDriver(driverId, vehicleId);
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
        public IActionResult SearchDriver(string query)
        {
            try
            {
                if (query != null)
                {
                    _driverService.SearchDriver(query);
                    _logger.LogInformation($"Searched for {query}");
                    return Ok("Searched for query");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpGet(nameof(GetDriverVehicles))]
        public IActionResult GetDriverVehicles(Guid driverId)
        {
            try
            {
                var driver = _driverService.GetDriverById(driverId);

                if (driver != null)
                {
                    _driverService.GetDriverVehicles(driverId);
                    _logger.LogInformation($"Returned driver {driverId} vehicles");
                    return Ok("Found driver's vehicles");
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
        public IActionResult GetDriverRideHistory(Guid driverId)
        {
            try
            {
                var driver = _driverService.GetDriverById(driverId);

                if (driver != null)
                {
                    _driverService.GetDriverRideHistory(driverId);
                    _logger.LogInformation($"Returned driver {driverId} ride history");
                    return Ok("Fetched driver's ride history");
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
        public IActionResult StartRide(Guid driverId, Guid rideId, Guid vehicleId)
        {
            try
            {
                var driver = _driverService.GetDriverById(driverId);
                var ride = _rideService.GetRideById(rideId);
                var vehicle = _vehicleService.GetVehicleById(vehicleId);

                if (driver != null && ride != null &&vehicle != null)
                {
                    _driverService.StartRide(driverId, rideId, vehicleId);
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
        public IActionResult EndRide(Guid rideId)
        {
            try
            {
                var ride = _rideService.GetRideById(rideId);
                
                if(ride != null)
                {
                    _driverService.EndRide(rideId);
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
