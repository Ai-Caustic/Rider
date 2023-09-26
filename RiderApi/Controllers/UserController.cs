﻿using DataLayer.Data;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }   

        [HttpGet(nameof(GetUserById))]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            try
            {
                var user = await _userService.GetUserById(userId);
                if (user != null)
                {
                    _logger.LogInformation("Returned User");
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }
        
        [HttpGet(nameof(GetAllUsers))]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                if (users == null)
                {
                    _logger.LogError("Could not find users");
                   return NotFound("Users");
                }   
                _logger.LogInformation("All users returned");
                return Ok(users);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpPost(nameof(CreateUser))]
        public IActionResult CreateUser(UserDTO user)
        {
            try
            {
                if(user == null)
                {
                    _logger.LogError("Invalid user input");
                    return BadRequest();
                }
                _userService.CreateUser(user);
                _logger.LogInformation("Created user");
                return Ok("Created user");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpPut(nameof(UpdateUser))]
        public async Task<IActionResult> UpdateUser(Guid userId)
        {
            try
            {
               var user = await _userService.GetUserById(userId);
               if(user == null)
               {
                  _logger.LogError($"Cannot find user {userId}");
                  return NotFound("User not found");
               }
               await _userService.UpdateUser(userId);
                _logger.LogInformation("Updated user");
                return Ok("Updated user");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpDelete(nameof(DeleteUser))]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            try
            {
                var user = await _userService.GetUserById(userId);
                if(user == null)
                {
                    _logger.LogError($"Could not find user {userId}");
                    return NotFound("User not found");
                }
                _logger.LogInformation("Deleted user");
                return Ok("Deleted user");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpGet(nameof(SearchUsers))]
        public async Task<IActionResult> SearchUsers(string query)
        {
            try
            {
                var result = await _userService.SearchUsers(query);
                _logger.LogInformation($"Searched for {query}");
                return Ok(result);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpGet(nameof(UserPayments))]
        public IActionResult UserPayments(Guid userId)
        {
            try
            {
                _userService.GetUserPayments(userId);
                _logger.LogInformation("Returned user payments");
                return Ok("User Payments");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpGet(nameof(UserRides))]
        public IActionResult UserRides(Guid userId)
        {
            try
            {
                _userService.GetUserRides(userId);
                _logger.LogInformation("Returned user rides");
                return Ok("User rides");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpPost(nameof(BookRide))]
        public IActionResult BookRide(Guid userId, Ride ride)
        {
            try
            {
                _userService.BookRide(userId, ride);
                _logger.LogInformation("User Booked Ride");
                return Ok("Booked ride");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpPost(nameof(CancelRide))]
        public IActionResult CancelRide(Guid rideId)
        {
            try
            {
                _userService.CancelRide(rideId);
                _logger.LogInformation("Ride cancelled");
                return Ok("Ride cancelled");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }


    }
}


