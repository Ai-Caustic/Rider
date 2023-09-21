using DomainLayer.Models;
using ServiceLayer.ICustomServices;
using DomainLayer.Enums;
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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task GetAllUsers()
        {
            try
            {
                await _userRepository.GetAllUsers();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task GetUserById(Guid Id)
        {
            try
            {
                await _userRepository.GetUserById(Id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task CreateUser(User user)
        {
            try
            {
                await _userRepository.Insert(user);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task UpdateUser(User user)
        {
            try
            {
                await _userRepository.Update(user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task DeleteUser(User user)
        {
            try
            {
                await _userRepository.Remove(user);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task SearchUser(string searchItem)
        {
            try
            {
                await _userRepository.Search(searchItem);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task GetUserPayments(Guid userId)
        {
            try
            {
                await _userRepository.GetUserPayments(userId);                                       
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task GetUserRides(Guid userId)
        {
            try
            {
                await _userRepository.GetUserRides(userId);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task BookRide(Guid userId, Ride ride)
        {   
            try
            {
                await _userRepository.BookRide(userId, ride);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }

        public async Task CancelRide(Guid rideId)
        {
            try
            {
                await _userRepository.CancelRide(rideId);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}, Exception: {ex.InnerException}");
            }
        }
    }
}
