using System.Linq.Expressions;
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
    public class RideController : ControllerBase
    {
        private readonly IRideService _rideService;

        private readonly ILogger<RideController> _logger;

        private readonly IUserService _userService;

        private readonly IDriverService _driverService;

        public RideController(IRideService rideService, ILogger<RideController> logger, IUserService userService, IDriverService driverService)
        {
            _rideService = rideService;
            _logger = logger;
            _driverService = driverService;
            _userService = userService;
        }

        [HttpGet(nameof(GetRideById))]
        public async Task<IActionResult> GetRideById(Guid Id)
        {
            try
            {
                var ride = await _rideService.GetRideById(Id);
                if (ride == null)
                {
                    _logger.LogWarning("Ride not found");
                    return NotFound(nameof(ride));
                }
                else
                {
                    _logger.LogInformation("Returned ride");
                    return Ok(ride);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpGet(nameof(GetAllRides))]
        public async Task<IActionResult> GetAllRides()
        {
            try
            {
                var rides = await _rideService.GetAllRides();
                if(rides == null)
                {
                    _logger.LogWarning("Could not find rides");
                    return NotFound(nameof(rides));
                }
                else
                {
                    _logger.LogInformation("Returned ride");
                    return Ok(rides);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpPut(nameof(UpdateRide))]
        public async Task<IActionResult> UpdateRide(Guid Id, RideDTO updatedRide)
        {
            try
            {
                var ride = await _rideService.GetRideById(Id);
                if(ride == null)
                {
                    _logger.LogWarning("Ride not found");
                    return NotFound(nameof(ride));
                }
                else
                {
                    await _rideService.UpdateRide(Id, updatedRide);
                    _logger.LogInformation("Updated ride");
                    return Ok("Updated ride");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpDelete(nameof(DeleteRide))]
        public async Task<IActionResult> DeleteRide(Guid Id)
        {
            try
            {
                var ride = await _rideService.GetRideById(Id);
                if(ride == null)
                {
                    _logger.LogWarning("Ride not found");
                    return NotFound(nameof(ride));
                }
                else
                {
                    await _rideService.DeleteRide(Id);
                    _logger.LogInformation("Ride deleted");
                    return Ok("Deleted ride");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpPost(nameof(CancelRide))]
        public async Task<IActionResult> CancelRide(Guid rideId)
        {
            try
            {
                var ride = await _rideService.GetRideById(rideId);
                if(ride == null)
                {
                    _logger.LogWarning("Ride not found");
                    return NotFound(nameof(ride));
                }
                else
                {
                    await _rideService.CancelRide(rideId);
                    _logger.LogInformation($"Cancelled ride {rideId}");
                    return Ok("Cancelled ride");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpGet(nameof(GetUserRides))]
        public async Task<IActionResult> GetUserRides(Guid userId)
        {
            try
            {
                var user = await _userService.GetUserById(userId);
                if(user == null)
                {
                    _logger.LogWarning($"Returned user {userId} rides");
                    return NotFound(nameof(user));
                }
                else
                {
                    await _rideService.GetRidesByUserId(userId);
                    _logger.LogInformation($"User {userId} cancelled ride");
                    return Ok("Returned user's rides");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpGet(nameof(GetDriverRides))]
        public async Task<IActionResult> GetDriverRides(Guid driverId)
        {
            try
            {
                var driver = await _driverService.GetDriverById(driverId);
                if(driver == null)
                {
                    _logger.LogWarning("Driver not found");
                    return NotFound(nameof(driver));
                }
                else
                {
                    await _rideService.GetRidesByDriverId(driverId);
                    _logger.LogInformation($"Returned driver {driverId} rides");
                    return Ok("Returned driver's rides");
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