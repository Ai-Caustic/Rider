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
        public async Task<IActionResult> GetUserById(Guid Id)
        {
            try
            {
                var user = await _userService.GetUserById(Id);
                if(user == null)
                {
                    _logger.LogWarning("User not found");
                    return NotFound(nameof(User));
                }
                else
                {
                    _logger.LogInformation("Returned User");
                    return Ok(user);
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
                    _logger.LogWarning("Could not find users");
                   return NotFound(nameof(users));
                }  
                else
                { 
                    _logger.LogInformation("All users returned");
                    return Ok(users);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpPost(nameof(CreateUser))]
        public async Task<IActionResult> CreateUser(UserDTO user)
        {
            try
            {
                if(user == null)
                {
                    _logger.LogWarning("Invalid user input");
                    return BadRequest();
                }
                else
                {
                    await _userService.CreateUser(user);
                    _logger.LogInformation("Created user");
                    return Ok("Created user");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpPut(nameof(UpdateUser))]
        public async Task<IActionResult> UpdateUser(Guid Id, UserDTO updatedUser)
        {
            try
            {
               var user = await _userService.GetUserById(Id);
                if (user == null)
                {
                    _logger.LogWarning($"Cannot find user {Id}");
                    return NotFound("User not found");
                }
                else
                {
                    await _userService.UpdateUser(Id, updatedUser);
                    _logger.LogInformation("Updated user");
                    return Ok("Updated user");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        [HttpDelete(nameof(DeleteUser))]
        public async Task<IActionResult> DeleteUser(Guid Id)
        {
            try
            {
                var user = await _userService.GetUserById(Id);
                if (user == null)
                {
                    _logger.LogError($"Could not find user {Id}");
                    return NotFound("User not found");
                }
                else
                {
                    await _userService.DeleteUser(Id);
                    _logger.LogInformation("Deleted user");
                    return Ok("Deleted user");
                }
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
        public IActionResult UserPayments(Guid Id)
        {
            try
            {
                _userService.GetUserPayments(Id);
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
        public IActionResult UserRides(Guid Id)
        {
            try
            {
                _userService.GetUserRides(Id);
                _logger.LogInformation("Returned user rides");
                return Ok("User rides");
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
            }
            return BadRequest();
        }

        //[HttpPost(nameof(BookRide))]
        //public IActionResult BookRide(Guid Id, Ride ride)
        //{
        //    try
        //    {
        //        _userService.BookRide(Id, ride);
        //        _logger.LogInformation("User Booked Ride");
        //        return Ok("Booked ride");
        //    }
        //    catch(Exception ex)
        //    {
        //        _logger.LogError($"Error: {ex.Message} Exception: {ex.InnerException}");
        //    }
        //    return BadRequest();
        //}

        [HttpPost(nameof(CancelRide))]
        public IActionResult CancelRide(Guid Id)
        {
            try
            {
                _userService.CancelRide(Id);
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


